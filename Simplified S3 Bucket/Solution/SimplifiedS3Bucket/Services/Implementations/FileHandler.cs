using SimplifiedS3Bucket.Services.Interfaces;

namespace SimplifiedS3Bucket.Services.Implementations;

public class FileHandler : IFileHandler
{
    private readonly IFileRepository _fileRepository;
    public FileHandler(IFileRepository fileRepository)
    {
        _fileRepository = fileRepository;
    }
    public bool UploadFile(List<IFormFile> postedFiles, string filePath, out string processedfile)
    {
        bool successStatus = false;
        processedfile = string.Empty;

        foreach (IFormFile postedFile in postedFiles)
        {
            string fileName = Path.GetFileName(postedFile.FileName);
            string fileWithPath = Path.Combine(filePath, fileName);
            processedfile = fileName;
            var ext = Path.GetExtension(fileWithPath).ToLowerInvariant();
            //Validate Only text file of size upto 1 kb
            if (ext == ".txt" && postedFile.Length <= 1000)
            {
                string tmpFileName = fileName;
                List<string> lstFileCopies = new List<string>();
                //To store 3 versions of file, keeping in dictionary by orginal file name as key
                //And colon Seperated file Copies by appending ###_number to form unique file name
                //If file already uploaded then to take files copies name from dictionary
                string? colonSeperatedfileCopies = FileAlreadyUploaded(fileName);
                bool fileAlreadyUploaded = false;
                if (!string.IsNullOrEmpty(colonSeperatedfileCopies))
                    fileAlreadyUploaded = true;

                for (int i = 0; i <= 2; i++)
                {
                    if (i > 0)//To store file copies by skiping original file name
                    {
                        if (fileAlreadyUploaded)
                        {
                            tmpFileName = colonSeperatedfileCopies.Split(":")[i - 1];
                        }
                        else
                        {
                            tmpFileName = fileName.Replace(".txt", $"###_{i}" + ".txt");
                            lstFileCopies.Add(tmpFileName);
                        }

                        fileWithPath = Path.Combine(filePath, tmpFileName);
                    }
                    using (FileStream stream = new FileStream(fileWithPath, FileMode.Create))
                    {
                        postedFile.CopyTo(stream);
                        successStatus = true;
                    }
                }
                if (!fileAlreadyUploaded)
                    _fileRepository.AddFile(fileName, string.Join(":", lstFileCopies));
            }

        }
        return successStatus;
    }

    public async Task<MemoryStream> DownloadFile(string fileWithPath)
    {
        var memory = new MemoryStream();
        using (var stream = new FileStream(fileWithPath, FileMode.Open))
        {
            await stream.CopyToAsync(memory);
        }
        memory.Position = 0;
        return memory;
    }

    public string? FileAlreadyUploaded(string fileName)
    {
        return _fileRepository.GetFileCopiesByFileName(fileName);

    }
    // Get content type
    public string GetContentType(string path)
    {
        var types = GetMimeTypes();
        var ext = Path.GetExtension(path).ToLowerInvariant();
        return types[ext];
    }

    // Get mime types
    public Dictionary<string, string> GetMimeTypes()
    {
        return new Dictionary<string, string>
                {
                    {".txt", "text/plain"},
                    {".pdf", "application/pdf"},
                    {".doc", "application/vnd.ms-word"},
                    {".docx", "application/vnd.ms-word"},
                    {".xls", "application/vnd.ms-excel"},
                    {".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"},
                    {".png", "image/png"},
                    {".jpg", "image/jpeg"},
                    {".jpeg", "image/jpeg"},
                    {".gif", "image/gif"},
                    {".csv", "text/csv"}
                };
    }
}
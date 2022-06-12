namespace SimplifiedS3Bucket.Services.Interfaces
{
    public interface IFileHandler
    {
        bool UploadFile(List<IFormFile> postedFiles, string filePath, out string processedfile);
        Task<MemoryStream> DownloadFile(string fileWithPath);
        string? FileAlreadyUploaded(string fileName);
        string GetContentType(string path);
        Dictionary<string, string> GetMimeTypes();
    }
}
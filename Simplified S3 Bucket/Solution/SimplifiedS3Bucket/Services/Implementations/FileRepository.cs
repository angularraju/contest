using SimplifiedS3Bucket.Services.Interfaces;

namespace SimplifiedS3Bucket.Services.Implementations
{
    public class FileRepository : IFileRepository
    {
        private Dictionary<string, string> _dictFileNames = new Dictionary<string, string>();
        public void AddFile(string fileName, string colonSeperatedfileCopies)
        {
            if (!_dictFileNames.ContainsKey(fileName))
                _dictFileNames.Add(fileName, colonSeperatedfileCopies);
        }
        public Dictionary<string, string> GetAllFiles()
        {
            return _dictFileNames;
        }
        public string? GetFileCopiesByFileName(string fileName)
        {
            _dictFileNames.TryGetValue(fileName, out string? colonSeperatedfileCopies);
            return colonSeperatedfileCopies;
        }

    }
}
namespace SimplifiedS3Bucket.Services.Interfaces
{
    public interface IFileRepository
    {
        void AddFile(string fileName, string colonSeperatedfileCopies);
        Dictionary<string, string> GetAllFiles();
        string? GetFileCopiesByFileName(string fileName);
    }
}
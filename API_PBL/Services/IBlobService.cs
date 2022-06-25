namespace API_PBL.Services
{
    public interface IBlobService
    {
        string GetBlob(string name, string containerName);
        Task<IEnumerable<string>> GetAllBlobsAsync(string containerName);
        Task<bool> UploadBlobAsync(string name, IFormFile file, string containerName);
        Task<bool> DeleteBlobAsync(string name, string containerName);
    }
}

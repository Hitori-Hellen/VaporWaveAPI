using Azure.Storage.Blobs;

namespace API_PBL.Services
{
    public class BlobService : IBlobService
    {
        private readonly BlobServiceClient _blobServiceClient;
        public BlobService(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
        }

        public async Task<bool> DeleteBlobAsync(string name, string containerName)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<string>> GetAllBlobsAsync(string containerName)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetBlob(string name, string containerName)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UploadBlobAsync(string name, IFormFile file, string containerName)
        {
            throw new NotImplementedException();
        }
    }
}

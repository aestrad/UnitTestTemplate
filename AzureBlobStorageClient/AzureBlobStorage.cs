using Azure.Storage.Blobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureBlobStorageClient
{
    internal class AzureBlobStorage : IAzureBlobStorage
    {
        private BlobContainerClient _blobContainerClient;

        public AzureBlobStorage(string connectionString, string container)
        {

            _blobContainerClient = new BlobContainerClient(connectionString, container);
        }

        public Task CreateBlob(string name, byte[] data)
        {
            throw new NotImplementedException();
        }

        public async Task<string> GetBlobKey(string blobKey)
        {
            var blob = _blobContainerClient.GetBlobClient(blobKey);

            if (!await _blobContainerClient.ExistsAsync()) return string.Empty;

            var reading = await blob.DownloadStreamingAsync();

            StreamReader reader = new(reading.Value.Content);

            return await reader.ReadToEndAsync();
        }

        public int NumberOfBlobs()
        {
            return _blobContainerClient.GetBlobs().Count();
        }
    }
}

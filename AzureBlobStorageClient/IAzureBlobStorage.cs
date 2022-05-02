namespace AzureBlobStorageClient
{
    public interface IAzureBlobStorage
    {
        public Task CreateBlob(string name, byte[] data);
        public Task<string> GetBlobKey(string blobKey);
        public int NumberOfBlobs();
    }
}

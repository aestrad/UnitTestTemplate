using Xunit;
using Moq;
using System.Threading.Tasks;
using System.Text;

namespace AzureBlobStorageClient.Tests
{
    public class AzureBlobStorageTest
    {

        private const string Content = "Some data";
        /// <summary>
        /// Follow the Arrange, Act, Assert 
        ///
        /// WIP
        /// 
        /// No calls to third party APIs, original test takes 91 ms to run.
        /// </summary>
        [Fact]
        public async Task BlobClient_CreateAndRetrieveBlobs()
        {

            /// Arrange
            var mock = new Mock<IAzureBlobStorage>();
            mock.Setup(azureBlobStorage => azureBlobStorage.CreateBlob(It.IsAny<string>(), It.IsAny<byte[]>()))
                 .Callback(() =>
                 {
                     mock.Setup(azureBlobStorage => azureBlobStorage.GetBlobKey(It.IsAny<string>()))
                         .Returns(async (object content) => await Task.FromResult(Content));

                     mock.Setup(azureBlobStorage => azureBlobStorage.NumberOfBlobs())
                         .Returns(1);
                 });

            /// Act
            await mock.Object.CreateBlob("file.txt", Encoding.UTF8.GetBytes(Content));
            var readTextFile = await mock.Object.GetBlobKey("file.txt");
            var filesCount = mock.Object.NumberOfBlobs();


            /// Assert
            Assert.Equal(1, filesCount);
            Assert.Equal(Content, readTextFile);

        }
    }
}

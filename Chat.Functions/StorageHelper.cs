using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Functions
{
    public static class StorageHelper
    {
        public static CloudBlobContainer GetContainer()
        {
            string StorageConnection = Environment.GetEnvironmentVariable(nameof(StorageConnection));

            var storageAccount = CloudStorageAccount.Parse(StorageConnection);

            var blobClient = storageAccount.CreateCloudBlobClient();

            var container = blobClient.GetContainerReference("chatimages");

            return container;
        }

        public static async Task<string> Upload(byte[] bytes, string fileEnding)
        {
            var container = GetContainer();
            var blob = container.GetBlockBlobReference($"{Guid.NewGuid().ToString()}.{fileEnding}");
            
            var stream = new MemoryStream(bytes);
            await blob.UploadFromStreamAsync(stream);

            return blob.Uri.AbsoluteUri;
        }

        public static async Task Clear()
        { 
            var container = GetContainer();
            var blobList = await container.ListBlobsSegmentedAsync(string.Empty, false, BlobListingDetails.None, int.MaxValue, null, null, null);
            foreach (var blob in blobList.Results.OfType<CloudBlob>())
            { 
                if (blob.Properties.Created.Value.AddHours(1) < DateTime.Now)
                { 
                    await blob.DeleteAsync();
                }
            }
        }
    }
}

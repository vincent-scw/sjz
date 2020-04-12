using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SJZ.Images.Repository
{
    public class ImageRepository : IImageRepository
    {
        public const string IMAGE_CONTAINER = "pictures";

        private readonly StorageAccountSettings _settings;

        public ImageRepository(StorageAccountSettings settings)
        {
            _settings = settings;
        }

        public async Task<List<Uri>> GetImagesByUserAsnyc(string user, int year, int month)
        {
            var list = new List<Uri>();
            var container = await GetBlobContainerAsync(IMAGE_CONTAINER);
            BlobContinuationToken token = null;
            do
            {
                var results = await container.ListBlobsSegmentedAsync($"{user}/{year}/{month}/_thumbnail", true,
                    BlobListingDetails.Metadata, null, token, null, null);
                list.AddRange(results.Results.Select(x => x.Uri));
                token = results.ContinuationToken;
            } while (token != null);

            return list;
        }

        public async Task<Uri> UploadImageAsync(string user, int year, int month, string name, Stream stream)
        {
            stream.Position = 0;
            var container = await GetBlobContainerAsync(IMAGE_CONTAINER);

            var cloudBlockBlob = container.GetBlockBlobReference($"{user}/{year}/{month}/{name}");
            await cloudBlockBlob.UploadFromStreamAsync(stream);

            return cloudBlockBlob.Uri;
        }

        private CloudStorageAccount GetAccount()
        {
            CloudStorageAccount.TryParse(_settings.ConnectionString, out CloudStorageAccount storageAccount);
            return storageAccount;
        }

        private async Task<CloudBlobContainer> GetBlobContainerAsync(string name)
        {
            var account = GetAccount();
            var client = account.CreateCloudBlobClient();

            var container = client.GetContainerReference(name);
            await container.CreateIfNotExistsAsync();

            BlobContainerPermissions permissions = await container.GetPermissionsAsync();
            if (permissions.PublicAccess != BlobContainerPublicAccessType.Blob)
            {
                permissions.PublicAccess = BlobContainerPublicAccessType.Blob;
                await container.SetPermissionsAsync(permissions);
            }
            return container;
        }
    }
}

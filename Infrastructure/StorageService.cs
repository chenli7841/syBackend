using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Azure.Identity;
using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using Common;
using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Infrastructure
{
    internal sealed class StorageService : IStorageService
    {
        private const string S3BucketName = "eplus-ex";
        private const string AwsAccessKey = "AKIAJEPDGNAK3LSG4D7Q";
        private const string AwsSecretKey = "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";
        private const string AzureStorageAccountUrl = "https://eplusfile.blob.core.windows.net";
        private const string AzureStorageAccountName = "eplusfile";
        private const string AzureStorageAccountAccessKey = "NUw8FH4OJd2HxNunxmpzrxkpT/6rciwhlXgiHg1DHk/fnU4zWgUIOkt2Mg1GqtZ9HF8EP0FtNj9++AStbBxhfQ==";
        private const string ContainerName = "shuyu";

        public async Task<string> UploadAsync(string rawData, string filename)
        {
            var data = Regex.Match(rawData, @"data:image/(?<type>.+?),(?<data>.+)").Groups["data"].Value;
            var bytes = Convert.FromBase64String(data);
            await using var stream = new MemoryStream(bytes);
            BlobServiceClient client = new(
                new Uri(AzureStorageAccountUrl),
                new StorageSharedKeyCredential(AzureStorageAccountName, AzureStorageAccountAccessKey));
            var containerClient = client.GetBlobContainerClient(ContainerName);
            await containerClient.UploadBlobAsync(filename, stream);
            return Path.Combine(AzureStorageAccountUrl, filename);
        }

        public async Task<string> UploadToAzureAsync(string rawData, string folderPath, string fileName)
        {
            var data = Regex.Match(rawData, @"data:image/(?<type>.+?),(?<data>.+)").Groups["data"].Value;
            var bytes = Convert.FromBase64String(data);
            await using var stream = new MemoryStream(bytes);
            BlobServiceClient client = new(
                new Uri(AzureStorageAccountUrl),
                new StorageSharedKeyCredential(AzureStorageAccountName, AzureStorageAccountAccessKey));
            var containerClient = client.GetBlobContainerClient(ContainerName);
            var filePath = Path.Combine(folderPath, fileName);
            await containerClient.UploadBlobAsync(filePath, stream);
            return Path.Combine(AzureStorageAccountUrl, filePath);
        }

        public string GetAzureUploadUrl(string folderPath, string fileName)
        {
            BlobServiceClient client = new(
                new Uri(AzureStorageAccountUrl),
                new StorageSharedKeyCredential(AzureStorageAccountName, AzureStorageAccountAccessKey));
            var containerClient = client.GetBlobContainerClient(ContainerName);
            var filePath = Path.Combine(folderPath, fileName);
            BlobClient blobClient = containerClient.GetBlobClient(filePath);
            BlobSasBuilder sasBuilder = new BlobSasBuilder()
            {
                BlobContainerName = ContainerName,
                BlobName = filePath,
                Resource = "b" // "b" for blob resource
            };
            sasBuilder.SetPermissions(BlobSasPermissions.Write);
            sasBuilder.ExpiresOn = DateTimeOffset.UtcNow.AddMinutes(5);
            Uri sasUri = blobClient.GenerateSasUri(sasBuilder);
            return sasUri.AbsoluteUri;
        }

        public string GetFileUrl(string folderPath, string fileName)
        {
            return $"{AzureStorageAccountUrl}/{ContainerName}/{folderPath}/{fileName}";
        }

        public async Task DeleteAsync(string filename)
        {
            using var client = new AmazonS3Client(AwsAccessKey, AwsSecretKey, RegionEndpoint.USEast1);
            var reponse = await client.DeleteObjectAsync(new DeleteObjectRequest()
            {
                BucketName = S3BucketName,
                Key = filename,
            });
        }
    }
}

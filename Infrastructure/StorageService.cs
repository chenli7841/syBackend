using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Common;

namespace Infrastructure
{
    internal sealed class StorageService : IStorageService
    {
        private const string S3BucketName = "eplus-ex";
        private const string AwsAccessKey = "AKIAJEPDGNAK3LSG4D7Q";
        private const string AwsSecretKey = "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";

        public async Task<string> UploadAsync(string rawData, string filename)
        {
            var data = Regex.Match(rawData, @"data:image/(?<type>.+?),(?<data>.+)").Groups["data"].Value;
            var bytes = Convert.FromBase64String(data);
            using var client = new AmazonS3Client(AwsAccessKey, AwsSecretKey, RegionEndpoint.USEast1);
            await using var stream = new MemoryStream(bytes);
            var uploadRequest = new TransferUtilityUploadRequest
            {
                InputStream = stream,
                Key = filename,
                BucketName = S3BucketName,
                CannedACL = S3CannedACL.PublicRead
            };

            var fileTransferUtility = new TransferUtility(client);
            await fileTransferUtility.UploadAsync(uploadRequest);
            return @"https://s3.amazonaws.com/eplus-ex/" + filename;
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

using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Amazon.S3.Model;
using Volo.Abp.BlobStoring;
using Volo.Abp.BlobStoring.Aws;

namespace AbpvNext.BlobStoring.Extensions.Aws
{
    public class AwsBlobProviderExt<TContainer> : AwsBlobProvider, IBlobProviderExt<TContainer> where TContainer : class
    {
        protected IBlobContainerConfigurationProvider ConfigurationProvider { get; }
        public AwsBlobProviderExt(IAwsBlobNameCalculator awsBlobNameCalculator, 
            IAmazonS3ClientFactory amazonS3ClientFactory,
            IBlobNormalizeNamingService blobNormalizeNamingService,
            IBlobContainerConfigurationProvider configurationProvider
            ) : base(awsBlobNameCalculator, amazonS3ClientFactory, blobNormalizeNamingService)
        {
            ConfigurationProvider = configurationProvider;
        }

        public async Task<string> GetUrlOrNullAsync(string name, TimeSpan? expiration = null, CancellationToken cancellationToken = default)
        {
            var typedContainerName = BlobContainerNameAttribute.GetContainerName<TContainer>();
            // The name of the container
            var configuration = ConfigurationProvider.Get(typedContainerName);

            var args = new BlobProviderGetArgs(typedContainerName, configuration, name, cancellationToken);

            var blobName = AwsBlobNameCalculator.Calculate(args);
            var containerName = GetContainerName(args);

            expiration ??= BlobContainerUrlAttribute.GetContainerUrlExpiration<TContainer>();
            var expirationTime = DateTime.Now.Add(expiration.Value);

            using (var amazonS3Client = await GetAmazonS3Client(args))
            {
                if (!await BlobExistsAsync(amazonS3Client, containerName, blobName))
                {
                    return null;
                }
                var preSignedUrlRequest = new GetPreSignedUrlRequest()
                {
                    BucketName = containerName,
                    Key = blobName,
                    Expires = expirationTime
                };
                
                var url =  amazonS3Client.GetPreSignedURL(preSignedUrlRequest);
                return url;
            }
        }
    }
}
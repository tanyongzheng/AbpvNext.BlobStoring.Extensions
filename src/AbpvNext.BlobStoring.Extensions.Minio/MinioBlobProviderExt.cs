using System;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.BlobStoring;
using Volo.Abp.BlobStoring.Minio;

namespace AbpvNext.BlobStoring.Extensions.Minio
{
    public class MinioBlobProviderExt<TContainer> : MinioBlobProvider, IBlobProviderExt<TContainer> where TContainer : class
    {
        protected IBlobContainerConfigurationProvider ConfigurationProvider { get; }
        public MinioBlobProviderExt(IMinioBlobNameCalculator minioBlobNameCalculator, 
            IBlobNormalizeNamingService blobNormalizeNamingService,
            IBlobContainerConfigurationProvider configurationProvider) 
            : base(minioBlobNameCalculator, blobNormalizeNamingService)
        {
            ConfigurationProvider = configurationProvider;
        }

        public async Task<string> GetUrlOrNullAsync(string name, TimeSpan? expiration = null, CancellationToken cancellationToken = default)
        {
            var typedContainerName = BlobContainerNameAttribute.GetContainerName<TContainer>();
            // The name of the container
            var configuration = ConfigurationProvider.Get(typedContainerName);

            var args=new BlobProviderGetArgs(typedContainerName, configuration,name,cancellationToken);

            var blobName = MinioBlobNameCalculator.Calculate(args);

            var client = GetMinioClient(args);
            var containerName = GetContainerName(args);

            if (!await BlobExistsAsync(client, containerName, blobName))
            {
                return null;
            }

            expiration ??= BlobContainerUrlAttribute.GetContainerUrlExpiration<TContainer>();

            var expiresInt = (int)expiration.Value.TotalSeconds;
            return await client.PresignedGetObjectAsync(containerName, blobName,expiresInt);
        }
    }
}
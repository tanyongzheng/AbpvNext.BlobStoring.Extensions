using System;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.BlobStoring;
using Volo.Abp.BlobStoring.Azure;

namespace AbpvNext.BlobStoring.Extensions.Azure
{
    public class AzureBlobProviderExt<TContainer> : AzureBlobProvider, IBlobProviderExt<TContainer> where TContainer : class
    {
        protected IBlobContainerConfigurationProvider ConfigurationProvider { get; }
        public AzureBlobProviderExt(IAzureBlobNameCalculator azureBlobNameCalculator, 
            IBlobNormalizeNamingService blobNormalizeNamingService,
            IBlobContainerConfigurationProvider configurationProvider) : 
            base(azureBlobNameCalculator, blobNormalizeNamingService)
        {
            ConfigurationProvider = configurationProvider;
        }

        public async Task<string> GetUrlOrNullAsync(string name, TimeSpan? expiration = null, CancellationToken cancellationToken = default)
        {
            var typedContainerName = BlobContainerNameAttribute.GetContainerName<TContainer>();
            // The name of the container
            var configuration = ConfigurationProvider.Get(typedContainerName);

            var args = new BlobProviderGetArgs(typedContainerName, configuration, name, cancellationToken);
            var blobName = AzureBlobNameCalculator.Calculate(args);

            if (!await BlobExistsAsync(args, blobName))
            {
                return null;
            }

            expiration ??= BlobContainerUrlAttribute.GetContainerUrlExpiration<TContainer>();
            var expirationTime = DateTime.Now.Add(expiration.Value);

            var blobClient = GetBlobClient(args, blobName);
            //var download = await blobClient.DownloadAsync();
            //var memoryStream = new MemoryStream();
            //await download.Value.Content.CopyToAsync(memoryStream);
            return blobClient.Uri.AbsoluteUri;
        }
    }
}
using System;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.BlobStoring;
using Volo.Abp.BlobStoring.Aliyun;

namespace AbpvNext.BlobStoring.Extensions.Aliyun
{
    public class AliyunBlobProviderExt<TContainer> :AliyunBlobProvider,  IBlobProviderExt<TContainer> where TContainer : class
    {
        protected IBlobContainerConfigurationProvider ConfigurationProvider { get; }

        public AliyunBlobProviderExt(IOssClientFactory ossClientFactory, 
            IAliyunBlobNameCalculator aliyunBlobNameCalculator, 
            IBlobNormalizeNamingService blobNormalizeNamingService,
            IBlobContainerConfigurationProvider configurationProvider) 
            : base(ossClientFactory, aliyunBlobNameCalculator, blobNormalizeNamingService)
        {
            ConfigurationProvider = configurationProvider;
        }

        public async Task<string> GetUrlOrNullAsync(string name,TimeSpan? expiration = null, CancellationToken cancellationToken = default)
        {
            var typedContainerName = BlobContainerNameAttribute.GetContainerName<TContainer>();
            // The name of the container
            var configuration = ConfigurationProvider.Get(typedContainerName);

            // var blobNormalizeNaming = BlobNormalizeNamingService.NormalizeNaming(Configuration, containerName, name);

            var args=new BlobProviderGetArgs(typedContainerName, configuration,name,cancellationToken);
            var containerName = GetContainerName(args);
            var blobName = AliyunBlobNameCalculator.Calculate(args);
            var ossClient = GetOssClient(configuration);
            if (!BlobExists(ossClient, containerName, blobName))
            {
                return null;
            }
            //var result = ossClient.GetObject(containerName, blobName);

            expiration ??= BlobContainerUrlAttribute.GetContainerUrlExpiration<TContainer>();
            var expirationTime = DateTime.Now.Add(expiration.Value);
            var uri = ossClient.GeneratePresignedUri(containerName, blobName,expirationTime);
            var url = uri.ToString();
            return url;
        }

    }
}
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Volo.Abp.BlobStoring;
using Volo.Abp.BlobStoring.FileSystem;
using Volo.Abp.DependencyInjection;

namespace AbpvNext.BlobStoring.Extensions.FileSystem
{
    public class FileSystemBlobProviderExt<TContainer> :FileSystemBlobProvider, ITransientDependency, IBlobProviderExt<TContainer> where TContainer : class
    {

        protected BlobContainerConfiguration Configuration { get; }

        private readonly FileSystemBlobExtensionOptions _fileSystemBlobExtensionOptions;
        protected IBlobContainerConfigurationProvider ConfigurationProvider { get; }

        public FileSystemBlobProviderExt(IBlobFilePathCalculator filePathCalculator,
            IBlobContainerConfigurationProvider configurationProvider,
            IOptions<FileSystemBlobExtensionOptions> fileSystemBlobExtensionOptions
        ) : base(filePathCalculator)
        {
            ConfigurationProvider = configurationProvider;
            _fileSystemBlobExtensionOptions = fileSystemBlobExtensionOptions.Value;
        }

        public async Task<string> GetUrlOrNullAsync(string name, TimeSpan? expiration = null, CancellationToken cancellationToken = default)
        {
            var typedContainerName = BlobContainerNameAttribute.GetContainerName<TContainer>();

            // The name of the container
            var configuration = ConfigurationProvider.Get(typedContainerName);

            var args = new BlobProviderGetArgs(typedContainerName, configuration, name, cancellationToken);

            var filePath = FilePathCalculator.Calculate(args);

            if (!File.Exists(filePath))
            {
                return null;
            }

            expiration ??= BlobContainerUrlAttribute.GetContainerUrlExpiration<TContainer>();
            var expirationTime = DateTime.Now.Add(expiration.Value);

            // 本地地址映射
            if (!string.IsNullOrEmpty(_fileSystemBlobExtensionOptions.BasePathMappingAddress))
            {
                var fileSystemConfiguration = args.Configuration.GetFileSystemConfiguration();
                filePath = filePath.Replace(fileSystemConfiguration.BasePath,
                    _fileSystemBlobExtensionOptions.BasePathMappingAddress).Replace("\\","/");
            }
            return filePath;
        }
    }
}
using System;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.BlobStoring.FileSystem;
using Volo.Abp.Modularity;

namespace AbpvNext.BlobStoring.Extensions.FileSystem
{
    [DependsOn(typeof(AbpBlobStoringFileSystemModule))]
    public class AbpVnextBlobStoringExtensionsFileSystemModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddTransient(
                typeof(IBlobProviderExt<>),
                typeof(FileSystemBlobProviderExt<>)
            );
        }
    }
}

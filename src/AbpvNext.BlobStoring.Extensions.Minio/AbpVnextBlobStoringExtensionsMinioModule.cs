using System;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.BlobStoring.Minio;
using Volo.Abp.Modularity;

namespace AbpvNext.BlobStoring.Extensions.Minio
{
    [DependsOn(typeof(AbpBlobStoringMinioModule))]
    public class AbpVnextBlobStoringExtensionsMinioModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddTransient(
                typeof(IBlobProviderExt<>),
                typeof(MinioBlobProviderExt<>)
            );
        }
    }
}

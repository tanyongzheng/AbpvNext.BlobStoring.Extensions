using System;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.BlobStoring.Azure;
using Volo.Abp.Modularity;

namespace AbpvNext.BlobStoring.Extensions.Azure
{
    [DependsOn(typeof(AbpBlobStoringAzureModule))]
    public class AbpVnextBlobStoringExtensionsAzureModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddTransient(
                typeof(IBlobProviderExt<>),
                typeof(AzureBlobProviderExt<>)
            );
        }
    }
}

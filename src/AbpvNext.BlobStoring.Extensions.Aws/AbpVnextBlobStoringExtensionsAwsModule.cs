using System;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.BlobStoring.Aws;
using Volo.Abp.Modularity;

namespace AbpvNext.BlobStoring.Extensions.Aws
{
    [DependsOn(typeof(AbpBlobStoringAwsModule))]
    public class AbpVnextBlobStoringExtensionsAwsModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddTransient(
                typeof(IBlobProviderExt<>),
                typeof(AwsBlobProviderExt<>)
            );
        }
    }
}

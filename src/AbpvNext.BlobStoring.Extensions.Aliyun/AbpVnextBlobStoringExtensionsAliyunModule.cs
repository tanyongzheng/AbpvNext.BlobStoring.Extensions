using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.BlobStoring;
using Volo.Abp.BlobStoring.Aliyun;
using Volo.Abp.Modularity;

namespace AbpvNext.BlobStoring.Extensions.Aliyun
{
    [DependsOn(typeof(AbpBlobStoringAliyunModule))]
    public class AbpVnextBlobStoringExtensionsAliyunModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddTransient(
                typeof(IBlobProviderExt<>),
                typeof(AliyunBlobProviderExt<>)
            );
        }
    }
}
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.BlobStoring;
using Volo.Abp.Modularity;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Threading;

namespace AbpvNext.BlobStoring.Extensions
{
    [DependsOn(typeof(AbpBlobStoringModule))]
    public class AbpVnextBlobStoringExtensionsModule : AbpModule
    {

    }
}
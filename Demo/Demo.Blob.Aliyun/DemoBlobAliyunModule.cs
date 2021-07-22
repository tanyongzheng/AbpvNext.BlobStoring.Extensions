using AbpvNext.BlobStoring.Extensions;
using AbpvNext.BlobStoring.Extensions.Aliyun;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using Volo.Abp;
using Volo.Abp.Autofac;
using Volo.Abp.BlobStoring;
using Volo.Abp.BlobStoring.Aliyun;
using Volo.Abp.Collections;
using Volo.Abp.Modularity;

namespace Demo.Blob.Aliyun
{
    [DependsOn(
        //typeof(AbpBlobStoringModule),
        //typeof(AbpBlobStoringAliyunModule),
        typeof(AbpVnextBlobStoringExtensionsAliyunModule),
        typeof(AbpAutofacModule))]
    public class DemoBlobAliyunModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();
            var hostEnvironment = context.Services.GetSingletonInstance<IHostEnvironment>();

            context.Services.AddHttpClient();

            context.Services.AddHostedService<DemoBlobAliyunHostedService>();

            #region Blob配置


            Configure<AbpBlobStoringOptions>(options =>
            {
                
                options.Containers.Configure<ProfilePictureContainer>(c =>
                {
                    c.IsMultiTenant = false;
                });
                options.Containers.ConfigureDefault(c =>
                {
                    c.IsMultiTenant = false;
                });
                
                options.Containers.ConfigureAll((containerName, containerConfiguration) =>
                {
                    
                    containerConfiguration.UseAliyun(aliyun =>
                    {
                        aliyun.AccessKeyId = "";
                        aliyun.AccessKeySecret = "";
                        aliyun.Endpoint = "";
                        //STS
                        aliyun.UseSecurityTokenService = true;
                        aliyun.RegionId = "";
                        aliyun.RoleArn = "";
                        aliyun.RoleSessionName = Guid.NewGuid().ToString("N");
                        aliyun.DurationSeconds = 900;
                        aliyun.Policy = string.Empty;
                        //Other
                        aliyun.CreateContainerIfNotExists = true;
                        aliyun.ContainerName = "";
                        aliyun.TemporaryCredentialsCacheKey = "";
                    });
                    
                });
            });
            #endregion
            
        }

        /*
        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {

        }
        */
    }
}
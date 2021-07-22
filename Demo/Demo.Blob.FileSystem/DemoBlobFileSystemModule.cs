using AbpvNext.BlobStoring.Extensions;
using AbpvNext.BlobStoring.Extensions.FileSystem;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Volo.Abp.Autofac;
using Volo.Abp.BlobStoring;
using Volo.Abp.BlobStoring.FileSystem;
using Volo.Abp.Modularity;

namespace Demo.Blob.FileSystem
{
    [DependsOn(
        //typeof(AbpBlobStoringModule),
        typeof(AbpBlobStoringFileSystemModule),
        typeof(AbpVnextBlobStoringExtensionsFileSystemModule),
        typeof(AbpAutofacModule))]
    public class DemoBlobFileSystemModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();
            var hostEnvironment = context.Services.GetSingletonInstance<IHostEnvironment>();

            context.Services.AddHttpClient();

            context.Services.AddHostedService<DemoBlobFileSystemHostedService>();


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
                    containerConfiguration.UseFileSystem(fileSystem =>
                    {
                        fileSystem.BasePath = @"C:\localfile";
                    });
                });

            });

            Configure<FileSystemBlobExtensionOptions>(options =>
            {
                options.BasePathMappingAddress = "http://127.0.0.1:8080";
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
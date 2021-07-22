using AbpvNext.BlobStoring.Extensions;
using AbpvNext.BlobStoring.Extensions.FileSystem;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Serilog;
using System;
using System.Linq;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.Autofac;
using Volo.Abp.BlobStoring;
using Volo.Abp.BlobStoring.FileSystem;
using Volo.Abp.Modularity;

namespace Demo.Blob.WebApi
{
    [DependsOn(
        typeof(AbpBlobStoringModule),
        typeof(AbpBlobStoringFileSystemModule),
        typeof(AbpVnextBlobStoringExtensionsFileSystemModule),
        typeof(AbpAutofacModule),
        typeof(AbpAspNetCoreMvcModule),
        typeof(AbpAspNetCoreSerilogModule))]
    public class DemoBlobWebApiModule : AbpModule
    {
        private const string DefaultCorsPolicyName = "Default";
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();
            var hostEnvironment = context.Services.GetSingletonInstance<IHostEnvironment>();

            ConfigureCors(context, configuration);
            ConfigureConventionalControllers();
            ConfigureSwaggerServices(context, configuration);
            context.Services.AddHttpClient();

            #region Blob配置

            Configure<AbpBlobStoringOptions>(options =>
            {
                /*
                options.Containers
                    .ConfigureDefault(container =>
                    {
                        container.SetConfiguration("TestConfigDefault", "TestValueDefault");
                        container.ProviderType = typeof(FakeBlobProvider1);
                    })
                    .Configure<TestContainer1>(container =>
                    {
                        container.SetConfiguration("TestConfig1", "TestValue1");
                        container.ProviderType = typeof(FakeBlobProvider1);
                    })
                    .Configure<TestContainer2>(container =>
                    {
                        container.SetConfiguration("TestConfig2", "TestValue2");
                        container.ProviderType = typeof(FakeBlobProvider2);
                    });
                */
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

            /*
            Configure<AbpBlobStoringOptions>(options =>
            {
                options.Containers.ConfigureDefault(container =>
                {
                    container.IsMultiTenant = false;
                });

                options.Containers.Configure<ProfilePictureContainer>(container =>
                {
                    container.IsMultiTenant = false;
                });
            });
            */

            
            Configure<FileSystemBlobExtensionOptions>(options =>
            {
                options.BasePathMappingAddress = "http://127.0.0.1:8080";
            });
            
            #endregion
        }


        private static void ConfigureSwaggerServices(ServiceConfigurationContext context, IConfiguration configuration)
        {
            context.Services.AddSwaggerGen(
                options =>
                {
                    options.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Version = "v1",
                        Title = "Order Api",
                        Description = "订单服务api"
                    });
                    options.DocInclusionPredicate((docName, description) => true);
                });
        }


        private void ConfigureConventionalControllers()
        {
            Configure<AbpAspNetCoreMvcOptions>(options =>
            {
                options.ConventionalControllers.Create(typeof(DemoBlobWebApiModule).Assembly);
            });
        }

        private void ConfigureCors(ServiceConfigurationContext context, IConfiguration configuration)
        {
            context.Services.AddCors(options =>
            {
                options.AddPolicy(DefaultCorsPolicyName, builder =>
                {
                    builder
                        .WithOrigins(
                            configuration["App:CorsOrigins"]
                                .Split(",", StringSplitOptions.RemoveEmptyEntries)
                                .Select(o => o.RemovePostFix("/"))
                                .ToArray()
                        )
                        .WithAbpExposedHeaders()
                        .SetIsOriginAllowedToAllowWildcardSubdomains()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();
            var env = context.GetEnvironment();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseSerilogRequestLogging();
            app.UseRouting();
            app.UseAbpSerilogEnrichers();
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Blob API");
            });
            app.UseConfiguredEndpoints();
        }
    }
}
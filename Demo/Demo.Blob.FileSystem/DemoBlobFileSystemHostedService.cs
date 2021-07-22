using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Volo.Abp;

namespace Demo.Blob.FileSystem
{
    public class DemoBlobFileSystemHostedService : IHostedService
    {
        private readonly IAbpApplicationWithExternalServiceProvider _application;
        private readonly IServiceProvider _serviceProvider;

        private readonly BlobManagerAppService _blobManagerAppService;

        public DemoBlobFileSystemHostedService(
            IAbpApplicationWithExternalServiceProvider application,
            IServiceProvider serviceProvider,
            BlobManagerAppService blobManagerAppService)
        {
            _application = application;
            _serviceProvider = serviceProvider;
            _blobManagerAppService = blobManagerAppService;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _application.Initialize(_serviceProvider);
            await _blobManagerAppService.SaveAsync();
            await _blobManagerAppService.GetUrlAsync();
            //return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _application.Shutdown();

            return Task.CompletedTask;
        }

    }
}
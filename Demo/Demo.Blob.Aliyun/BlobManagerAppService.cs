using System;
using System.Net.Http;
using System.Threading.Tasks;
using AbpvNext.BlobStoring.Extensions;
using Volo.Abp.BlobStoring;
using Volo.Abp.DependencyInjection;

namespace Demo.Blob.Aliyun
{
    public class BlobManagerAppService : ITransientDependency
    {
        private readonly IBlobContainer<ProfilePictureContainer> _blobContainer;
        private readonly IBlobProviderExt<ProfilePictureContainer> _blobProviderExt;
        private readonly IHttpClientFactory _httpClientFactory;

        private string blobName = "aliyun_blob_test_001.png";

        public BlobManagerAppService(
            IBlobContainer<ProfilePictureContainer> blobContainer,
            IBlobProviderExt<ProfilePictureContainer> blobProviderExt,
            IHttpClientFactory httpClientFactory)
        {
            _blobContainer = blobContainer;
           _blobProviderExt = blobProviderExt;
            _httpClientFactory = httpClientFactory;
        }

        public async Task SaveAsync()
        {
            var client = _httpClientFactory.CreateClient();
            byte[] bytes =await client.GetByteArrayAsync("https://www.baidu.com/img/540x258_2179d1243e6c5320a8dcbecd834a025d.png");
            var overrideExisting = true;
            await _blobContainer.SaveAsync(blobName, bytes, overrideExisting);
            Console.WriteLine("已保存文件");
        }

        public async Task GetUrlAsync()
        {
            var url = await _blobProviderExt.GetUrlOrNullAsync(blobName, TimeSpan.FromDays(10));
            Console.WriteLine("获取到url:"+url);
        }

    }
}
using System;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.BlobStoring;

namespace AbpvNext.BlobStoring.Extensions
{
    public interface IBlobProviderExt<TContainer> where TContainer : class
    {
        /// <summary>
        /// 获取二进制大对象的Url
        /// </summary>
        /// <param name="name">Blob Name</param>
        /// <param name="expiration">过期时间</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns></returns>
        Task<string> GetUrlOrNullAsync(string name, TimeSpan? expiration = null, CancellationToken cancellationToken = default);
    }
}
using System;
using System.Reflection;
using JetBrains.Annotations;
using Volo.Abp;

namespace AbpvNext.BlobStoring.Extensions
{
    public class BlobContainerUrlAttribute : Attribute
    {

        [NotNull]
        public TimeSpan UrlExpiration { get; }

        public BlobContainerUrlAttribute([NotNull] long urlExpirationSeconds)
        {
            //Check.NotNullOrWhiteSpace(urlExpiration, nameof(urlExpiration));
            UrlExpiration = TimeSpan.FromSeconds(urlExpirationSeconds);
        }

        public virtual TimeSpan GetUrlExpiration(Type type)
        {
            return UrlExpiration;
        }

        public static TimeSpan GetContainerUrlExpiration<T>()
        {
            return GetContainerUrlExpiration(typeof(T));
        }

        public static TimeSpan GetContainerUrlExpiration(Type type)
        {
            var urlAttribute = type.GetCustomAttribute<BlobContainerUrlAttribute>();

            if (urlAttribute == null)
            {
                // 默认1年有效
                return TimeSpan.FromDays(365);
            }

            return urlAttribute.GetUrlExpiration(type);
        }
    }
}
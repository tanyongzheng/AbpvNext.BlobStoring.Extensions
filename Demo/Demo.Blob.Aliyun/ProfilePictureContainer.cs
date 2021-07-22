using System;
using AbpvNext.BlobStoring.Extensions;
using Volo.Abp.BlobStoring;

namespace Demo.Blob.Aliyun
{
    [BlobContainerName(Name)]
    [BlobContainerUrl(60*60*24*365)]
    public class ProfilePictureContainer
    {
        public const string Name = "profile-pictures";
    }

    /*
    public class Consts
    {
        public static readonly TimeSpan UrlExpiration = TimeSpan.FromDays(365);
    }
    */
}
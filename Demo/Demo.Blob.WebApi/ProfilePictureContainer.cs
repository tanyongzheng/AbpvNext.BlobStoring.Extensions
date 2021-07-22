//using AbpvNext.BlobStoring.Extensions;
using Volo.Abp.BlobStoring;

namespace Demo.Blob.WebApi
{
    [BlobContainerName(Name)]
    //[BlobContainerUrl(60 * 60 * 24 * 365)]
    public class ProfilePictureContainer
    {
        public const string Name = "profile-pictures-test";
    }
}
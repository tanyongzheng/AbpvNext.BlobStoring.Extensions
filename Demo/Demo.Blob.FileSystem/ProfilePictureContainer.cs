using AbpvNext.BlobStoring.Extensions;
using Volo.Abp.BlobStoring;

namespace Demo.Blob.FileSystem
{
    [BlobContainerName(Name)]
    [BlobContainerUrl(60 * 60 * 24 * 180)]
    public class ProfilePictureContainer
    {
        public const string Name = "profile-pictures";
    }
}
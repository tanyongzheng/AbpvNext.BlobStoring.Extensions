namespace AbpvNext.BlobStoring.Extensions.FileSystem
{
    public class FileSystemBlobExtensionOptions
    {
        /// <summary>
        /// 基本路径映射地址
        /// 譬如本地路径映射为域名或者IP地址
        /// </summary>
        public string BasePathMappingAddress { get; set; }
    }
}
$full = $args[0]

# 公共路径 
# 获取根目录完整路径
$rootFolder = (Get-Item -Path "./" -Verbose).FullName

# 开发模式下的解决方案列表，主要包含类库项目和部分样例项目，.sln文件所在路径
$solutionPaths = @(
		"../src/AbpvNext.BlobStoring.Extensions",
		"../src/AbpvNext.BlobStoring.Extensions.Aliyun",
		"../src/AbpvNext.BlobStoring.Extensions.Aws",
		"../src/AbpvNext.BlobStoring.Extensions.Azure",
		"../src/AbpvNext.BlobStoring.Extensions.FileSystem",
		"../src/AbpvNext.BlobStoring.Extensions.Minio"
	)

if ($full -eq "-f")
{
	# 完整构建需要的其他解决方案，譬如Demo项目
	$solutionPaths += (
		"../Demo/Demo.Blob.Aliyun",
		"../Demo/Demo.Blob.FileSystem",
		"../Demo/Demo.Blob.WebApi"
	) 
}else{ 
	Write-host ""
	Write-host ":::::::::::::: !!! You are in development mode !!! ::::::::::::::" -ForegroundColor red -BackgroundColor  yellow
	Write-host "" 
} 

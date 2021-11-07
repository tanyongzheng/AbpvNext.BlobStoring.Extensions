$full = $args[0]

# ����·�� 
# ��ȡ��Ŀ¼����·��
$rootFolder = (Get-Item -Path "./" -Verbose).FullName

# ����ģʽ�µĽ�������б���Ҫ���������Ŀ�Ͳ���������Ŀ��.sln�ļ�����·��
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
	# ����������Ҫ���������������Ʃ��Demo��Ŀ
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

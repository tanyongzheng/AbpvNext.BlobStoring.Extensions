# Paths
$packFolder = (Get-Item -Path "./" -Verbose).FullName
$rootFolder = Join-Path $packFolder "../"

# List of solutions
$solutions = (
	"src"
)

# List of projects
$projects = (

    # ÏîÄ¿
	"src/AbpvNext.BlobStoring.Extensions",
	"src/AbpvNext.BlobStoring.Extensions.Aliyun",
	"src/AbpvNext.BlobStoring.Extensions.Aws",
	"src/AbpvNext.BlobStoring.Extensions.Azure",
	"src/AbpvNext.BlobStoring.Extensions.FileSystem",
	"src/AbpvNext.BlobStoring.Extensions.Minio"
)

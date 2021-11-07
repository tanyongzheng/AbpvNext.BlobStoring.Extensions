. ".\common_nupkg.ps1"

$apiKey = $args[0]

# Get the version
[xml]$commonPropsXml = Get-Content (Join-Path $rootFolder "common.props")
# [xml]$commonPropsXml = Get-Content ($rootFolder + "/common.props")
$version = $commonPropsXml.Project.PropertyGroup.Version
("Pakcage Vserions:" + $version)

# ȥ����β�ո�
$version = (""+$version+"").Trim()

# Publish all packages
foreach($project in $projects) {
    $projectName = $project.Substring($project.LastIndexOf("/") + 1)
    # ��ӡ�����İ�
    $nupkgName = ($projectName + "." + $version + ".nupkg")
    "Publish Package:" + $nupkgName
    & dotnet nuget push $nupkgName -s https://api.nuget.org/v3/index.json --api-key "$apiKey"
}

# Go back to the pack folder
Set-Location $packFolder

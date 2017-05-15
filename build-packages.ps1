Add-Type -assembly "system.io.compression.filesystem"

cd .\.nuget

$moduleName = "ScheduledJobOverview"
$source = $PSScriptRoot + "\src\" + $moduleName + "\modules\_protected\TechFellow." + $moduleName
$destination = $PSScriptRoot + "\src\" + $moduleName + "\TechFellow." + $moduleName + ".zip"

If(Test-path $destination) {Remove-item $destination}
[io.compression.zipfile]::CreateFromDirectory($source, $destination)

.\nuget.exe pack ..\src\ScheduledJobOverview\ScheduledJobOverview.csproj -Properties Configuration=Release
cd ..\

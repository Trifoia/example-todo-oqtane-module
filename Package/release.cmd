del "*.nupkg"
"..\..\oqtane.framework\oqtane.package\nuget.exe" pack Trifoia.Module.Todo.nuspec 
XCOPY "*.nupkg" "..\..\oqtane.framework\Oqtane.Server\Packages\" /Y


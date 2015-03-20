:: nuget spec
set /p version=Version number:
nuget pack Webhook.csproj -Prop Configuration=Debug -Symbols -IncludeReferencedProjects -Version %version%
nuget push Webhook.%version%.nupkg
pause;
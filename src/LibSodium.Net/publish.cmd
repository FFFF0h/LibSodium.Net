dotnet pack %~dp0LibSodium.Net.csproj -c Release -p:ContinuousIntegrationBuild=true --output %~dp0nupkgs
for %%f in ("%~dp0nupkgs\LibSodium.Net.*.nupkg") do if /I not "%%~xf"==".snupkg" dotnet nuget push "%%~ff" --api-key %NUGET_API_KEY% --source https://api.nuget.org/v3/index.json

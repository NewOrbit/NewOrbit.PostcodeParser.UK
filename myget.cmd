set PackageVersion=
dotnet restore
dotnet test ./test/Tests/Tests.csproj
if not "%errorlevel%"=="0" exit -1
dotnet pack ./src/PostcodeParser.UK/PostcodeParser.UK.csproj -c Release --include-source
nuget push ./src/PostcodeParser.UK/bin/Release/*
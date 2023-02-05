build: 
	dotnet build SparkPlug.sln

tests:
	dotnet test SparkPlug.sln

samples:
	dotnet build ./sample/WebApi/WebApi.csproj

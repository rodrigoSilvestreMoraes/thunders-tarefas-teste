dotnet test --test-adapter-path Tests/Tarefas.Test.csproj /p:CollectCoverage=true /p:CoverletOutputFormat=opencover /p:CoverletOutput=Coverage/ /p:excludebyattribute=*.ExcludeFromCodeCoverage*

%USERPROFILE%\.nuget\packages\reportgenerator\5.2.4\tools\net8.0\ReportGenerator.exe "-reports:Test/Coverage/coverage.opencover.xml" "-targetdir:Test/Coverage"
pause
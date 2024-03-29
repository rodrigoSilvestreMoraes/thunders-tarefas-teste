dotnet test --test-adapter-path Tests/FluxoCaixa.Test.csproj /p:CollectCoverage=true /p:CoverletOutputFormat=opencover /p:CoverletOutput=Coverage/ /p:excludebyattribute=*.ExcludeFromCodeCoverage*

%USERPROFILE%\.nuget\packages\reportgenerator\5.1.10\tools\net6.0\ReportGenerator.exe "-reports:Test/Coverage/coverage.opencover.xml" "-targetdir:Test/Coverage"
pause
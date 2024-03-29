# Projeto Gestão de Tarefas!

Projeto para avaliação, aplicação baseada em API Rest usando banco de dados Mongo.
Contém uma ideia de envio de eventos para um simulador de Service Bus para geração de log.
Projeto contém o uso de boas práticas no que coube ser implementado.
Peço a gentileza que rodem a aplicação e analisem todo o código.

# Instalando o banco de dados Mongo

Será necessário possuir o Docker na máquina local.

 1. Baixe o projeto do repositório git.
 2. Acesse a pasta ***Tarefas\Docker\Mongo***.
 3. Usando um prompt de comando de sua preferência execute o seguinte comando: **docker-compose up -d**
 4. Espere carregar o mongo, em seu prompt algo parecido com a imagem abaixo deve estar sendo exibido:
 ![mongo carregado](https://github.com/rodrigoSilvestreMoraes/tarefas/blob/main/mongo_1.png)
 5. Use um client para mongo de sua preferência que possua acesso ao shell do mongo, recomendamos o [Downloads - NoSQLBooster for MongoDB](https://nosqlbooster.com/downloads).
 6. Uma vez dentro do sheel do mongo, acesso arquivo ***Tarefas\DataBase\scripts\scriptCriacaoBaseDadosCompleta.js***
 7. Execute esse script todo, ele vai criar o DataBase, o usuário e as collection's assim como uma quantidade de massa de dados.
 8. A imagem abaixo mostra se o banco foi criado com sucesso:
 
![Mongo Instalado e configurado](https://github.com/rodrigoSilvestreMoraes/tarefas/blob/main/mongo_2.png)
 

## Rodando aplicação API:

Com Visual Studio 2022 e dotnet core **versão 6.0.404** instalado na máquina, acesse a solução do projeto e rode a API.

 1. API Rest utilizando swagger para demostrar o uso das rotas.
 2. As operações podem ser construídas utilizando o próprio swagger e o retorno das rotas de domínio. 
 3. Para acessar o healthcheck da api execute a rota: http://localhost:5227/health

## Rodando a cobertura de teste unitário e gerando relatório de cobertura:

É possível rodar a cobertura de teste unitário e gerar um relatório de cobertura utilizando o padrão **opencover**.
Acesse via prompt de comando a pasta **Tarefas**, a pasta raíz do projeto.
Utilize os exemplos de comando localizado no arquivo **Coverage.bat**.

 1. Para gerar a cobertura rode o comando:`dotnet test --test-adapter-path Tests/Tarefas.Test.csproj /p:CollectCoverage=true /p:CoverletOutputFormat=opencover /p:CoverletOutput=Coverage/ /p:excludebyattribute=*.ExcludeFromCodeCoverage*`
 2. Instale o componente ReportGenerator: 
 2. Para gerar o relatório em HTML rode o comando:`%USERPROFILE%\.nuget\packages\reportgenerator\5.1.10\tools\net6.0\ReportGenerator.exe "-reports:Test/Coverage/coverage.opencover.xml" "-targetdir:Test/Coverage"`
 3. Uma vez gerado a cobertura, é possível ver a página com resultado acessando o arquivo localizado: ***Tarefas\Test\Coverage\index.htm***
 4. Será possível ver o resultado igual a imagem abaixo:  
 
 ![enter image description here](https://github.com/rodrigoSilvestreMoraes/tarefas/blob/main/mongo_3.png)

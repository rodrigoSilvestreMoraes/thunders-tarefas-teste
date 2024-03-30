# Projeto Gestão de Tarefas!

Projeto para avaliação técnica. 
Aplicação CRUD responsável pelo gerenciamento de Tarefas.

**Divisão dos projeto:**
-   ***Tarefas.Api:*** Projeto .Net API Rest, responsável em receber as solicitações de CRUD para o gerenciamento de tarefas, possui swagger e postman para ser utilizado.
-   ***Tarefas.Core:*** Projeto Library .Net, responsável em conter todos os recursos de código necessários para implementação da solução, está sub dividida em:
    -   ***Domain:***  Contém o código responsável pelo dominio da aplicação.
        -   **Application:** Contém a implementação da comunicação com componentes que estão fora do dominio.
        -   **Entities:** Contém as entidades do dominio, se encontram isoladas.
        -   **Models:** Contém uma representação das entidades de dominio e servem para funções específicas orientadas pelas responsabilidades do dominio.
        -   **Repositorys:** Contém uma abstração das operações necessárias para a gestão dos dados em uma base de dados.
        -   **ServiceBusiness:** Contém o processamento das entidades de dominio, executando regras de negócio e realizando o processamento dos dados.
    -   ***Infra:*** Contém recursos de infra, load, cross e execução de IO's.
        -   **BootStrap:** Responsável pelo IOC da aplicação.
        -   **CustomLogger:** Recursos para geração de log em fila
        -   **EventBus:** Implementação em memória para processamento multi thread simulando uma fila.
        -   **Extension:** Extensões de uso comum.
        -   **Repository:** Implementação do banco de dados utilizado na aplicação.
        -   **Rest:** Models padrões para comunicação Rest com a API.
        -   **Validator:** Classes auxiliadoras para o validators.
-   ***Tarefas.Test:*** Projeto que contém os testes unitários do projeto, utilizando xunit e Mock.

Peço a gentileza que rodem a aplicação e analisem todo o código.

# Pré requisitos para rodar a solução.
1.  Visual Studio 2022
2.  Dot Net Core 8.x
3.  Docker
4.  Client para acessar o Mongo: recomendamos o [Downloads - NoSQLBooster for MongoDB](https://nosqlbooster.com/downloads).
5.  Postman ou algum de sua preferência


# Preparando o ambiente para rodar aplicação:

***Preparando a Base de Dados.***

 1. Baixe o projeto do repositório git.
 2. Acesse a pasta ***pathProjeto\Docker\Mongo***.
 3. Usando um prompt de comando de sua preferência execute o seguinte comando: **docker-compose up -d**
 4. Espere carregar o mongo, usando uma ferramenta client do mongo, conecte-se ao mongo com localhost, não precisa informar usuário me senha:
 ![conexão com mongo](https://github.com/rodrigoSilvestreMoraes/thunders-tarefas-teste/blob/main/imagens/mongo_conexao.png)
 5. Crie um DataBase chamado TarefasDB.
 6. Uma vez dentro do sheel do mongo, acesso arquivo ***DataBase\scripts\scriptCriacaoBaseDadosCompleta.js***.
 7. Execute esse script todo, ele vai criar o DataBase, o usuário e as collection's assim como uma quantidade de massa de dados.
 8. A imagem abaixo mostra se o banco foi criado com sucesso:
 ![Mongo Instalado e configurado](https://github.com/rodrigoSilvestreMoraes/thunders-tarefas-teste/blob/main/imagens/mongo_db_criado.png)
 
 
## Rodando aplicação API:

Com Visual Studio 2022 e dotnet core **versão 8.0.101** instalado na máquina, acesse a solução do projeto e rode a API.

 1. API Rest utilizando swagger para demostrar o uso das rotas.
 2. As operações podem ser construídas utilizando o próprio swagger e o retorno das rotas de domínio. 
 3. Para acessar o healthcheck da api execute a rota: http://localhost:5227/health
 4. Baixe as collections do Postman: [TarefasPostman_collection.zip] (https://github.com/rodrigoSilvestreMoraes/thunders-tarefas-teste/blob/main/files).

## Rodando a cobertura de teste unitário e gerando relatório de cobertura:  

1. Instale o componente ReportGenerator: 
2. Acesse via prompt de comando a pasta **Tarefas**, a pasta raíz do projeto.
3. Rode o arquivo **Coverage.bat**, dentro desse arquivo encontram-se os comandos:
    -   Para gerar a cobertura rode o comando:`dotnet test --test-adapter-path Tests/Tarefas.Test.csproj /p:CollectCoverage=true /p:CoverletOutputFormat=opencover /p:CoverletOutput=Coverage/ /p:excludebyattribute=*.ExcludeFromCodeCoverage*`
    -   Para gerar o relatório em html: `%USERPROFILE%\.nuget\packages\reportgenerator\5.2.4\tools\net8.0\ReportGenerator.exe "-reports:Test/Coverage/coverage.opencover.xml" "-targetdir:Test/Coverage"`
    -   ***OBS:*** A versão do ReportGenerator pode alterar a pasta do executável, verifique antes de rodar.
4. Uma vez gerado a cobertura, é possível ver a página com resultado acessando o arquivo localizado: ***Tarefas\Test\Coverage\index.htm***
 ![Imagem ilustrativa da cobertura de teste](https://github.com/rodrigoSilvestreMoraes/thunders-tarefas-teste/blob/main/imagens/cobertura_teste_unit.png)
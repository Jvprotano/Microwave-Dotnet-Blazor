# Projeto Micro-ondas

## Descrição

Este projeto é uma aplicação completa de controle de um micro-ondas. Ele é composto por uma API desenvolvida em .NET 8, um frontend construído com Blazor, e testes automatizados utilizando xUnit. O banco de dados utilizado é o SQL Server, configurado com migrations do Entity Framework Core.

## Estrutura do Projeto

- **API**: Desenvolvida em .NET 8, responsável pela lógica de negócios e manipulação dos dados.
- **Frontend**: Interface criada com Blazor, consumindo a API e permitindo a interação com o micro-ondas.
- **Testes**: Testes de unidade implementados com xUnit para garantir a qualidade e funcionalidade da aplicação.
- **Banco de Dados**: SQL Server, gerenciado por migrations do Entity Framework Core.

## Tecnologias Utilizadas

- .NET 8 (Backend e API)
- Blazor WASM (Frontend)
- Entity Framework Core (ORM)
- SQL Server (Banco de Dados)
- xUnit (Testes unitários)
- Docker (Opcional, se for containerizar a aplicação)
  
---

## Alguns padrões e princípios aplicados

- Singleton
- Dependency Injection
- SOLID
- DRY
  
---

## Como Rodar o Projeto

### Pré-requisitos

- **.NET SDK 8**: [Instale o SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- **SQL Server**: Pode ser instalado localmente ou usar o [Docker](https://hub.docker.com/_/microsoft-mssql-server) para rodar em containers.
- **Visual Studio 2022** ou **VS Code**

### Passos para Rodar o Projeto Localmente

1. **Clonar o Repositório**

   Clone o projeto do Git:
   ```bash
   git clone https://github.com/Jvprotano/Microwave.git
   cd microwave
   ```

2. **Configurar o Banco de Dados SQL Server**

   Certifique-se de que o SQL Server está rodando na sua máquina. Configure a string de conexão para o SQL Server no arquivo `appsettings.json` da API ou utilizando o dotnet user-secrets:

   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "ConnectionStrings:Default" "Server=localhost,1433;Database=microwave;User ID=sa;Password=SUA_SENHA;Trusted_Connection=False; TrustServerCertificate=True;"
     }
   }
   ```
   ou

    ```json
   dotnet user-secrets init
    dotnet user-secrets set "ConnectionStrings:Default" "Server=localhost,1433;Database=microwave;User ID=sa;Password=SUA_SENHA;Trusted_Connection=False; TrustServerCertificate=True;"
     ```

3. **Rodar as Migrations**

   No diretório da API, aplique as migrations do Entity Framework para configurar o banco de dados:

   ```bash
   cd Microwave.Api
   dotnet ef database update
   ```

   Caso a string de conexão tenha sido criada corretamente e o servidor do SQLServer rodando, o Entity Framework deverá vai criar o banco de dados e as tabelas necessárias com base nas migrations.

4. **Rodar o Backend (API)**

   No diretório da API, execute o comando para rodar a aplicação:

   ```bash
   cd Microwave.Api
   dotnet run
   ```

   A API vai rodar por padrão no endereço `https://localhost:5181`.

5. **Rodar o Frontend (Blazor)**

   No diretório do frontend, execute o comando para iniciar a aplicação Blazor:

   ```bash
   cd Microwave.Web
   dotnet run
   ```

   O frontend estará disponível no endereço `https://localhost:5065`.

6. **Rodar os Testes**

   No diretório dos testes, execute o comando para rodar os testes unitários:

   ```bash
   cd Microwave.Tests
   dotnet test
   ```

   Todos os testes xUnit devem passar sem erros.

---

## Testes

Os testes unitários foram desenvolvidos utilizando o xUnit para garantir que a lógica de negócio está funcionando corretamente. 
---

## API Endpoints

- **Com exceção dos endpoints de Registro e Login de usuário, todos os endpoints requerem autenticação.**

### Após inicializar a API, adicione a seguinte roda à URL para acessar o Swagger e obter informações dos endpoints

- `/swagger` - Listará todos os endpoints criados

## Autenticação

A autenticação se dá através dos endpoints de registro e login e deverá ser feita utilizando o Bearer Token retornado pelo endpoint de login.
Padrão para uso no Swagger: 
   ```bash
Bearer token
   ```

### O funcionamento do micro-ondas se dá através dos seguintes endpoints: 
- `/v1/start` - Inicia o aquecimento usando os valores informados ou, caso informado o ID de um programa predefinido, usando os valores deste.

- `/v1/pause-stop` - Caso o aquecimento esteja em execução, acionar este endpoint uma vez pausará a execução e, em uma segunda vez, parará limpando os dados.

- `/v1/get-execution-status` - Através deste endpoint é possível acompanhar o andamento da execução de aquecimento, exibindo dados como tempo total, tempo decorrido e label de execução.

- `/v1/predefined-programs` - Retornará uma lista com todos os programas pré-definidos, criados por padrão ou pelo usuário.

- `/v1/predefined-programs/custom-programs` - Retornará uma lista com todos os programas pré-definidos CRIADOS PELO USUÁRIO, ou seja, personalizados.

- `/v1/predefined-programs/custom-programs` - Permite criar novos programas pré-definidos.


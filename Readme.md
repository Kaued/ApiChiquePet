# ApiChiquePet

## Descrição
O ApiChiquePet é um projeto desenvolvido em .NET 7.0 que oferece uma API para gerenciar informações relacionadas a animais de estimação com estilo e elegância. O projeto utiliza o Entity Framework Core como ferramenta de mapeamento objeto-relacional (ORM) e requer um banco de dados MySQL.

## Pré-requisitos
- [.NET 7.0](https://dotnet.microsoft.com/download)
- [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/get-started/install)
- [MySQL](https://www.mysql.com/downloads/)
- [Dotnet Watch Tool](https://docs.microsoft.com/en-us/aspnet/core/tutorials/dotnet-watch)

## Configuração do Banco de Dados
Antes de iniciar o projeto, é necessário configurar as informações do banco de dados no arquivo `appsettings.json`. Substitua as configurações de conexão para refletir as credenciais do seu banco MySQL:

```json
{
  "ConnectionStrings": {
    "ChiquePetDatabase": "Server=localhost;Database=ChiquePetDb;User=root;Password=senha;"
  },
  // ... outras configurações ...
}
```

## Configuração do Banco de Dados no MySQL
Antes de executar o comando `dotnet ef database update`, certifique-se de criar o banco de dados no MySQL. Você pode fazer isso com o seguinte comando SQL:

```sql
CREATE DATABASE ChiquePetDb;
```

## Migrações do Banco de Dados
Execute o seguinte comando para aplicar as migrações e criar o banco de dados:

```bash
dotnet ef database update
```

Certifique-se de que o Entity Framework Core CLI esteja instalado:

```bash
dotnet tool install --global dotnet-ef
```

## Rodando o Projeto
Após configurar o banco de dados, você pode iniciar o projeto. As rotas disponíveis podem ser visualizadas no Swagger. Execute o seguinte comando:

```bash
dotnet watch run --launch-profile "http"
```

O Swagger estará disponível em `https://localhost:5169/swagger/index.html`.

## Observações
Certifique-se de ter o MySQL instalado e em execução antes de aplicar as migrações.

Este projeto utiliza o Swagger para documentação de API. Consulte a documentação gerada pelo Swagger para obter detalhes sobre as rotas e os modelos de dados disponíveis.

**Aproveite o ApiChiquePet!** 🐾🌟
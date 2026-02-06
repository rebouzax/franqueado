# Franqueado API — Inventário de Produtos

API REST para **gestão de inventário de franqueados**, focada em **produtos** e **estoque** (entradas/saídas), com **auditoria de movimentações** e **controle de concorrência otimista**.  
Projeto desenvolvido em **.NET 10**, seguindo boas práticas de **Clean Code**, **arquitetura em camadas** e **CQRS**.

---

## Visão geral

Esta API permite:

- **Cadastro e manutenção de produtos** (CRUD)
- **Consulta de produtos** com paginação, busca e ordenação
- **Gestão de estoque por franqueado e produto**
- **Movimentações de estoque** (entrada/saída) com **auditoria**
- **Histórico de movimentações** com paginação e filtro por período
- **Concorrência otimista** via `RowVersion` para evitar conflitos em atualizações simultâneas

---

## Arquitetura

A solução está organizada em camadas:

- **Franqueado.Api**  
  Controllers, contratos HTTP, middlewares e configuração de DI

- **Franqueado.Application**  
  Casos de uso (CQRS com Commands/Queries), validações, behaviors e abstrações

- **Franqueado.Domain**  
  Entidades, enums, exceções e interfaces de repositórios (regras de negócio)

- **Franqueado.Infra**  
  Persistência (EF Core + SQL Server), implementações de repositórios, migrations e serviços de infraestrutura


> Organização por **feature folder**: cada command/query fica em sua própria pasta (`Command`, `Handler`, `Validator`).

---

## Tecnologias e padrões

- **.NET 10**
- **ASP.NET Core Web API**
- **Entity Framework Core + SQL Server** (migrations)
- **CQRS** com **MediatR**
- **FluentValidation** + Pipeline Behavior
- **Swagger** (documentação e testes)
- **Auditoria de estoque** com tabela de movimentações
- **Concorrência otimista** com `RowVersion` (SQL Server `rowversion`)

---

## Regras importantes

### SKU único
O `SKU` do produto é **único** (unique index no banco).

### Estoque por Franqueado + Produto
A tabela de estoque possui **restrição única** para o par:

- `(FranqueadoId, ProdutoId)`

### Concorrência (RowVersion)
Atualizações de estoque exigem `RowVersion` para evitar race conditions:

- Você deve obter o estoque via `GET` e usar o `rowVersion` retornado no `PUT`
- Se o `rowVersion` estiver desatualizado, a API retorna **409 (Conflict)**

### Auditoria de movimentações
Entradas/saídas geram registros em `MovimentacoesEstoque` com:

- tipo (Entrada/Saida)
- quantidade
- motivo
- usuário
- data/hora

---

## Como executar

### Pré-requisitos
- .NET SDK 10 instalado
- SQL Server disponível (local ou remoto)

### Configuração
Edite `Franqueado.Api/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "Default": "Server=localhost;Database=FranqueadoDb;Trusted_Connection=True;TrustServerCertificate=True"
  }
}
```

na raiz do projeto adicionar este comando:
```bash
dotnet ef migrations add InitialCreate -p franqueado.infra -s franqueado.api -o Persistence/Migrations
dotnet ef database update -p franqueado.infra -s franqueado.api
```




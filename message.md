# Desafio Prático - Desenvolvedor Back-end C# .NET Core | vSaúde

## Objetivo do Desafio

Desenvolver uma API REST em C# .NET Core para gerenciamento de tarefas médicas que permita aos profissionais de saúde organizarem suas atividades diárias de forma eficiente e segura.

### Principais Requisitos:
- **CRUD Completo**: Implementação de operações Create, Read, Update e Delete para tarefas médicas
- **Filtros e Consultas**: Sistema de busca avançada com filtros por status, prioridade e categoria
- **Segurança e Validação**: Implementação de validações robustas e tratamento de exceções
- **Escalabilidade**: Arquitetura preparada para crescimento e alta demanda

## Funcionalidades Obrigatórias

### CREATE
- Criar novas tarefas médicas com validação

### READ
- Listar tarefas com filtros e paginação

### UPDATE
- Atualizar informações da tarefa

### DELETE
- Remover tarefas com soft delete

### Endpoints da API
- GET /api/tarefas
- GET /api/tarefas/{id}
- POST /api/tarefas
- PUT /api/tarefas/{id}
- PATCH /api/tarefas/{id}/status
- DELETE /api/tarefas/{id}

### Modelo de Dados - Tarefa Médica
```csharp
public class TarefaMedica
{
    public int Id { get; set; }
    public string Titulo { get; set; }
    public string Descricao { get; set; }
    public PrioridadeEnum Prioridade { get; set; }
    public CategoriaEnum Categoria { get; set; }
    public StatusEnum Status { get; set; }
    public DateTime DataCriacao { get; set; }
}
```

### Filtros e Consultas
- Status e prioridade
- Filtro por data
- Busca por texto
- Paginação

## Requisitos Técnicos

### Framework e Linguagem
- .NET Core 6.0+ (preferencialmente .NET 8)
- C# 10+ com nullable reference types

### Banco de Dados e ORM
- SQL Server como banco de dados principal
- Entity Framework Core como ORM principal

### Ferramentas e Bibliotecas
- AutoMapper para mapeamento de DTOs
- FluentValidation para validação de dados
- Swagger/OpenAPI para documentação
- Logging estruturado (Serilog recomendado)

### Padrões de Projeto Requeridos
- Repository Pattern
- Unit of Work
- DTO Pattern
- Exception Handling

## Diferenciais Técnicos

### Tecnologias Opcionais
- Dapper: Micro ORM para alta performance
- ADO.NET: Acesso direto ao banco
- Redis: Cache distribuído
- JWT: Autenticação segura

### Padrões e Práticas
- MediatR: Padrão CQRS
- Testes: xUnit com Moq
- Docker: Containerização
- Health Checks: Monitoramento


### Código Fonte
- Repositório GitHub
- README detalhado

### Dicas para se destacar
- Implemente primeiro as funcionalidades obrigatórias
- Utilize boas práticas de Clean Code e SOLID
- Documente decisões técnicas importantes
- Adicione testes unitários para validar a lógica de negócio

**Boa sorte! Estamos ansiosos para ver sua solução!**
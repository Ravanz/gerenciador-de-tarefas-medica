# vSaúde API

API REST para gerenciamento de tarefas médicas desenvolvida em .NET 8.

## Tecnologias

- .NET 8
- Entity Framework Core
- SQL Server
- AutoMapper
- FluentValidation
- Serilog
- Swagger/OpenAPI
- Docker

## Como Executar

### Com Docker (Recomendado)

```bash
docker-compose up -d
```

Acesse: http://localhost:5000/swagger

### Sem Docker

Requer SQL Server instalado localmente.

```bash
dotnet restore
dotnet run
```

Configure a connection string em `appsettings.json`.

## Endpoints

| Método | Endpoint | Descrição |
|--------|----------|-----------|
| GET | `/api/tarefas` | Lista tarefas com filtros e paginação |
| GET | `/api/tarefas/{id}` | Busca tarefa por ID |
| POST | `/api/tarefas` | Cria nova tarefa |
| PUT | `/api/tarefas/{id}` | Atualiza tarefa completa |
| PATCH | `/api/tarefas/{id}/status` | Atualiza apenas status |
| DELETE | `/api/tarefas/{id}` | Remove tarefa (soft delete) |
| GET | `/health` | Health check |

## Filtros (GET /api/tarefas)

- `status` - Status do atendimento
  - 0 = Aguardando Atendimento
  - 1 = Em Atendimento
  - 2 = Atendimento Concluído
  - 3 = Cancelada pelo Paciente

- `prioridade` - Nível de urgência
  - 0 = Baixa
  - 1 = Média
  - 2 = Alta
  - 3 = Crítica

- `categoria` - Tipo de atendimento
  - 1 = Consulta Médica
  - 2 = Procedimento Cirúrgico
  - 3 = Exame Laboratorial/Imagem
  - 4 = Administração de Medicamento
  - 5 = Atendimento de Emergência
  - 6 = Internação Hospitalar

- `busca` - Busca por texto no título/descrição
- `dataInicio` / `dataFim` - Filtro por intervalo de datas
- `page` / `pageSize` - Paginação (padrão: page=1, pageSize=10)

## Exemplos

### Criar tarefa

```bash
curl -X POST http://localhost:5000/api/tarefas \
  -H "Content-Type: application/json" \
  -d '{
    "titulo": "Consulta com paciente",
    "descricao": "Revisão de exames",
    "prioridade": 1,
    "categoria": 1
  }'
```

### Listar tarefas

```bash
curl http://localhost:5000/api/tarefas
```

### Buscar com filtros

```bash
curl "http://localhost:5000/api/tarefas?status=0&prioridade=2&page=1&pageSize=10"
```

### Atualizar status

```bash
curl -X PATCH http://localhost:5000/api/tarefas/1/status \
  -H "Content-Type: application/json" \
  -d '1'
```

## Docker

### Comandos úteis

```bash
# Subir
docker-compose up -d

# Ver logs
docker-compose logs -f

# Parar
docker-compose down

# Rebuild após mudanças
docker-compose up -d --build

# Remover tudo (inclui dados)
docker-compose down -v
```

### Portas

- API: http://localhost:5000
- SQL Server: localhost:1433

### Credenciais SQL Server

- Usuário: `sa`
- Senha: `Senha@Forte123`
- Database: `vSaudeDb`

## Estrutura

```
vSaude/
├── Controllers/          # Endpoints da API
├── Models/              # Entidades, DTOs, Repositórios, Validações
├── Program.cs           # Configuração da aplicação
├── Dockerfile           # Imagem Docker da API
├── docker-compose.yml   # Orquestração dos containers
└── appsettings.json     # Configurações
```

## Padrões Implementados

- Repository Pattern
- Unit of Work
- DTO Pattern
- Dependency Injection
- Soft Delete com Query Filters

## Licença

MIT

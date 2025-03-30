# Construindo uma API .NET 8 com Clean Architecture, DDD, CQRS e EF Core para Gestão de Produtos e Departamentos

Este guia detalhado explora a criação de uma API .NET 8 robusta, seguindo os princípios de Clean Architecture, Domain-Driven Design (DDD), Command Query Responsibility Segregation (CQRS) e utilizando Entity Framework Core (EF Core) e MediatR. O objetivo é desenvolver um sistema que monitora um arquivo de texto, importa produtos para um banco de dados MySQL e permite a associação de produtos a Departamentos.

## 1. Arquitetura da Solução

A estrutura do projeto será organizada em camadas, de acordo com os princípios da Clean Architecture, para garantir alta coesão e baixo acoplamento:

```
src/
│
├── Application/         # Camada de Aplicação (casos de uso e lógica de negócio)
│   ├── Commands/        # Comandos CQRS (ações de escrita)
│   ├── Queries/         # Queries CQRS (ações de leitura)
│   ├── Handlers/        # Manipuladores de comandos e queries
│   ├── Interfaces/      # Interfaces para serviços de domínio e repositórios
│   ├── Mappers/         # Mapeamento de objetos de domínio para DTOs
│   ├── DTOs/            # Data Transfer Objects
│   └── Exceptions/      # Exceções personalizadas
│
├── Domain/              # Camada de Domínio (entidades, Value Objects e lógica de negócio)
│   ├── Entities/        # Entidades do domínio (Produto, Departamento)
│   ├── ValueObjects/    # Value Objects (Preço, etc.)
│   ├── Interfaces/      # Interfaces para repositórios de domínio
│   ├── Events/          # Eventos de domínio (opcional)
│   └── Exceptions/      # Exceções de domínio
│
├── Infrastructure/      # Camada de Infraestrutura (EF Core, acesso ao banco de dados, monitoramento de arquivos)
│   ├── Persistence/     # Contexto do EF Core e repositórios de dados
│   ├── FileMonitoring/  # Implementação do monitoramento de arquivos
│   ├── Services/        # Serviços externos (email, logs, etc.)
│   └── Mappings/        # Configurações de mapeamento do EF Core
│
├── Presentation/        # Camada de Apresentação (API Web)
│   ├── Controllers/     # Controladores da API
│   ├── Models/          # Modelos de requisição e resposta da API
│   └── Filters/         # Filtros de requisição e resposta da API
│
└── WebAPI.csproj        # Arquivo de projeto .NET 8
```

## 2. Camadas e Arquivos Detalhados

### 2.1. Domain (Domínio)

- **Entities**:
  - `Produto.cs`: Representa um produto com propriedades como `Codigo`, `Descricao`, `Preco`.
  - `Departamento.cs`: Representa uma Departamento com propriedades como `Id`, `Nome` e uma lista de `Produtos`.

- **Value Objects**:
  - `Preco.cs`: Representa o preço do produto com validações específicas.

- **Interfaces**:
  - `IProdutoRepository.cs`: Define operações CRUD para a entidade Produto.
  - `IDepartamentoRepository.cs`: Define operações CRUD para a entidade Departamento.

- **Events (Opcional)**:
  - `ProdutoImportadoEvent.cs`: Representa um evento de domínio quando um produto é importado.

- **Exceptions**:
  - `ProdutoNaoEncontradoException.cs`: Exceção personalizada para quando um produto não é encontrado.

### 2.2. Application (Aplicação)

- **Commands**:
  - `ImportarProdutosCommand.cs`: Representa o comando para importar produtos de um arquivo.
  - `AssociarProdutoDepartamentoCommand.cs`: Representa o comando para associar um produto a uma Departamento.

- **Queries**:
  - `ObterProdutosQuery.cs`: Representa a query para obter todos os produtos.
  - `ObterProdutoPorCodigoQuery.cs`: Representa a query para obter um produto por código.
  - `ObterDepartamentosQuery.cs`: Representa a query para obter todas as Departamentos.

- **Handlers**:
  - `ImportarProdutosCommandHandler.cs`: Manipula o comando `ImportarProdutosCommand`.
  - `AssociarProdutoDepartamentoCommandHandler.cs`: Manipula o comando `AssociarProdutoDepartamentoCommand`.
  - `ObterProdutosQueryHandler.cs`: Manipula a query `ObterProdutosQuery`.
  - `ObterProdutoPorCodigoQueryHandler.cs`: Manipula a query `ObterProdutoPorCodigoQuery`.
  - `ObterDepartamentosQueryHandler.cs`: Manipula a query `ObterDepartamentosQuery`.

- **Interfaces**:
  - `IProdutoService.cs`: Define operações de serviço para a entidade Produto.
  - `IDepartamentoservice.cs`: Define operações de serviço para a entidade Departamento.

- **DTOs**:
  - `ProdutoDto.cs`: Data Transfer Object para a entidade Produto.
  - `DepartamentoDto.cs`: Data Transfer Object para a entidade Departamento.

- **Mappers**:
  - `ProdutoMapper.cs`: Mapeia entre Produto e ProdutoDto.
  - `DepartamentoMapper.cs`: Mapeia entre Departamento e DepartamentoDto.

- **Exceptions**:
  - `ProdutoNaoEncontradoException.cs`: Exceção personalizada da camada de aplicação.

### 2.3. Infrastructure (Infraestrutura)

- **Persistence**:
  - `AppDbContext.cs`: Contexto do EF Core para o banco de dados MySQL.
  - `ProdutoRepository.cs`: Implementação de `IProdutoRepository` usando EF Core.
  - `DepartamentoRepository.cs`: Implementação de `IDepartamentoRepository` usando EF Core.

- **FileMonitoring**:
  - `ArquivoMonitorService.cs`: Implementa a lógica de monitoramento de arquivos e processamento de produtos.

- **Services**:
  - Serviços externos (ex: logs).

- **Mappings**:
  - Configurações de mapeamento do EF Core para as entidades Produto e Departamento.

### 2.4. Presentation (Apresentação)

- **Controllers**:
  - `ProdutoController.cs`: Controla as operações relacionadas a produtos (importação, associação, listagem).
  - `DepartamentoController.cs`: Controla as operações relacionadas a Departamentos (listagem).

- **Models**:
  - `ImportarProdutosRequest.cs`: Modelo de requisição para importar produtos.
  - `AssociarProdutoDepartamentoRequest.cs`: Modelo de requisição para associar um produto a uma Departamento.
  - `ProdutoResponse.cs`: Modelo de resposta para produtos.
  - `DepartamentoResponse.cs`: Modelo de resposta para Departamentos.

- **Filters**:
  - Filtros para tratamento de erros e validações.

## 3. Fluxo de Execução

1. **Monitoramento de Arquivos**: O `ArquivoMonitorService` monitora o arquivo de texto em busca de novas linhas (produtos).
2. **Importação de Produtos**: Ao detectar novas linhas, o serviço dispara o comando `ImportarProdutosCommand`, que é manipulado por `ImportarProdutosCommandHandler`.
3. **Persistência**: O handler usa o `ProdutoRepository` para salvar os produtos no banco de dados MySQL via EF Core.
4. **Associação de Departamentos**: A API expõe um endpoint para associar produtos a Departamentos usando o comando `AssociarProdutoDepartamentoCommand`.
5. **Listagem de Produtos e Departamentos**: A API expõe endpoints para listar produtos e Departamentos usando as queries `ObterProdutosQuery` e `ObterDepartamentosQuery`.
6. **MediatR**: O MediatR é usado para desacoplar as camadas e facilitar a comunicação entre os controladores e os handlers de comandos e queries.

## 4. Tecnologias e Dependências

- **.NET 8**: Framework principal.
- **Entity Framework Core (EF Core)**: ORM para mapeamento objeto-relacional.
- **MySQL**: Banco de dados relacional.
- **MediatR**: Biblioteca para implementação do padrão Mediator.
- **AutoMapper**: Biblioteca para mapeamento de objetos.
- **FluentValidation**: Biblioteca para validação de dados.

## 5. Considerações Finais

- Este guia oferece uma visão geral detalhada da arquitetura e implementação da API.
- A implementação real pode variar dependendo dos requisitos específicos do projeto.
- Testes unitários e de integração são essenciais para garantir a qualidade do código.
- A segurança da API deve ser considerada, incluindo autenticação e autorização.
- A escalabilidade da API pode ser melhorada usando padrões de cache e filas de mensagens.
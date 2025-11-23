# Revoo (WorkWell)

## Visão geral
Revoo é uma API para gestão de hábitos corporativos, metas semanais e progresso dos colaboradores.

## Arquitetura
Clean Architecture:
- Revoo.Domain: entidades + invariantes do negócio.
- Revoo.Application: DTOs e serviços de aplicação (casos de uso).
- Revoo.Infrastructure: EF Core + repositórios Oracle.
- Revoo.Api: controllers, Swagger, ProblemDetails e HATEOAS.

## Como rodar
1. Configure a connection string Oracle em `src/Revoo.Api/appsettings.json`.
2. Defina Revoo.api como projeto de inicialização e execute.
3. Digite /swagger na barra de endereços para acessar a documentação.

## Imagens do projeto compilado rodando, via Swagger



## Endpoints principais
### Usuários
- POST `/api/usuarios`
- GET `/api/usuarios/{id}`
- PUT `/api/usuarios/{id}`
- DELETE `/api/usuarios/{id}`

### Hábitos
- POST `/api/habitos`
- GET `/api/habitos/{id}`
- PUT `/api/habitos/{id}`
- DELETE `/api/habitos/{id}`
- GET `/api/habitos/search?term=&categoriaId=&ativo=S&page=1&pageSize=10&orderBy=nome&desc=false`

### Metas Semanais
- POST `/api/metas-semanais`
- GET `/api/metas-semanais/{id}`
- PUT `/api/metas-semanais/{id}`
- DELETE `/api/metas-semanais/{id}`
- GET `/api/metas-semanais/search?...`

### Registros de Progresso
- POST `/api/registros-progresso`
- GET `/api/registros-progresso/{id}`
- PUT `/api/registros-progresso/{id}`
- DELETE `/api/registros-progresso/{id}`
- GET `/api/registros-progresso/search?...`

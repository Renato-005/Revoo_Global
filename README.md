# Revoo

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

<img width="1716" height="994" alt="image" src="https://github.com/user-attachments/assets/c15d1f5f-726b-4199-bae6-076c10b238d3" />
<img width="1561" height="620" alt="image" src="https://github.com/user-attachments/assets/55f71828-8050-4f80-aa4a-a2410263fe13" />
<img width="1499" height="786" alt="image" src="https://github.com/user-attachments/assets/4897be3f-e192-46c3-97b6-68551f7cd957" />


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

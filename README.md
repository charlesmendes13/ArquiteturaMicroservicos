# Arquitetura de Microserviços e Mobile
Projeto de disciplina.

## Arquitetura

Toda a aplicação foi desenvolvida utilizando microserviços em .NET Core 6 banco de dados SQL Server e RabbitMQ.

## Ambiente

### Docker
Acesse o link abaixo para baixar as seguintes imagens do docker:
* https://hub.docker.com/_/microsoft-mssql-server
* https://hub.docker.com/_/rabbitmq

Em seguida execute o seguintes comandos:
```
CMD> docker pull mcr.microsoft.com/mssql/server
CMD> docker pull rabbitmq
```
Após a execução dos comandos acima, basta inicializar os container das imagens com os comandos:
```
CMD> docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=Str0ngPa$$w0rd" -p 1433:1433 -d mcr.microsoft.com/mssql/server
CMD> docker run -d --hostname rabbitserver --name rabbitmq-server -p 15672:15672 -p 5672:5672 rabbitmq:3-management
```
* Para MacOs com arquitetura AMR64, utiliza o Azure-SQL-Edge ao invés do MSSQL
```
CLI> docker run --cap-add SYS_PTRACE -e 'ACCEPT_EULA=1' -e 'MSSQL_SA_PASSWORD=Str0ngPa$$w0rd' -p 1433:1433 -d mcr.microsoft.com/azure-sql-edge
```

### Banco de Dados
Para a criação do banco de dados e tabelas, entre no Gerenciador de Pacotes Nuget de cada microserviço e selecione o projeto "Infrastructure" e execute o seguinte comando:
```
PM> Update-Database
```
* Para MasOs/Linux,ir até o diretório "Api" de cada microserviço e executar o seguinte comando:
```
CLI> dotnet ef database update
```
Para acessar o SQL Server no client, utilize as seguintes credenciais:
```
Server: localhost
User: sa
Password: Str0ngPa$$w0rd
```

### RabiitMQ
Para acessar o RabbitMQ Management, utilize as seguintes credenciais:
```
Host: http://localhost:15672/
User: guest
Password: guest
```

## Requisições

O projeto possui Documentação pelo Swagger, portanto possui os seguintes endpoints:

### POST User

Para criar Usuário utilize o endpoint abaixo:
```
curl -X 'POST' \
  'https://localhost:5000/api/User' \
  -H 'accept: text/plain' \
  -H 'Content-Type: application/json' \
  -d '{
  "name": "teste",
  "email": "teste@test.com",
  "senha": "123" 
}'
```

### POST AccessToken

Para criar um Token JWT e utilizar as demais funcionalidades dos microserviços, utilize o endpoint abaixo:
```
curl -X 'POST' \
  'https://localhost:5000/api/AccessToken' \
  -H 'accept: text/plain' \
  -H 'Content-Type: application/json' \
  -d '{
  "email": "teste@test.com",
  "senha": "123" 
}'
```

### GET Product

Para obter a lista com todos os Produtos, utilize o endpoint abaixo:
```
curl -X 'GET' \
  'https://localhost:5000/api/Product' \
  -H 'accept: text/plain' \
  -H 'Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJ0ZXN0ZUBjaW5lbWFyay5jb20iLCJleHAiOjE2NTYwMjc0MjQsImlzcyI6ImNoYXJsZXMubWVuZGVzIiwiYXVkIjoiY2hhcmxlcy5tZW5kZXMifQ.UubI-d6hL1KsqZiZxSoDbLHL2PG7k83qiS2TAgpkIWA'
```

### GET BasketByUserId

Para obter os Itens adicionados no carrinho de compra , utilize o endpoint abaixo:
```
curl -X 'GET' \
  'https://localhost:5000/api/Basket/250c0e4d-b8bd-4724-8792-c16b40f00126' \
  -H 'accept: text/plain' \
  -H 'Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJ0ZXN0ZUBjaW5lbWFyay5jb20iLCJleHAiOjE2NTYwMjc0MjQsImlzcyI6ImNoYXJsZXMubWVuZGVzIiwiYXVkIjoiY2hhcmxlcy5tZW5kZXMifQ.UubI-d6hL1KsqZiZxSoDbLHL2PG7k83qiS2TAgpkIWA'
```

### POST AddItem

Para adicionar os itens ao carrinho utilize o endpoint abaixo:
```
curl -X 'POST' \
  'https://localhost:5000/api/Basket/AddItem?userId=250c0e4d-b8bd-4724-8792-c16b40f00126' \
  -H 'accept: text/plain' \
  -H 'Content-Type: application/json' \
  -H 'Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJ0ZXN0ZUBjaW5lbWFyay5jb20iLCJleHAiOjE2NTYwMjc0MjQsImlzcyI6ImNoYXJsZXMubWVuZGVzIiwiYXVkIjoiY2hhcmxlcy5tZW5kZXMifQ.UubI-d6hL1KsqZiZxSoDbLHL2PG7k83qiS2TAgpkIWA' \
  -d '{
  "quantity": 1,
  "productId": 1
}'
```

### DELETE RemoveItem

Para remover os itens ao carrinho utilize o endpoint abaixo:
```
curl -X 'DELETE' \
  'https://localhost:5000/api/Basket/RemoveItem?userId=250c0e4d-b8bd-4724-8792-c16b40f00126&itemId=7' \
  -H 'accept: text/plain' \
  -H 'Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJ0ZXN0ZUBjaW5lbWFyay5jb20iLCJleHAiOjE2NTYwMjc0MjQsImlzcyI6ImNoYXJsZXMubWVuZGVzIiwiYXVkIjoiY2hhcmxlcy5tZW5kZXMifQ.UubI-d6hL1KsqZiZxSoDbLHL2PG7k83qiS2TAgpkIWA'
```

### DELETE Basket

Para excluir todo o carrinho de compra, utilize o endpoint abaixo:
```
curl -X 'DELETE' \
  'https://localhost:5000/api/Basket/250c0e4d-b8bd-4724-8792-c16b40f00126' \
  -H 'accept: text/plain' \
  -H 'Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJ0ZXN0ZUBjaW5lbWFyay5jb20iLCJleHAiOjE2NTYwMjc0MjQsImlzcyI6ImNoYXJsZXMubWVuZGVzIiwiYXVkIjoiY2hhcmxlcy5tZW5kZXMifQ.UubI-d6hL1KsqZiZxSoDbLHL2PG7k83qiS2TAgpkIWA'
```

### POST Payment

Para excluir todo o carrinho de compra, utilize o endpoint abaixo:
```
curl -X 'POST' \
  'https://localhost:5000/api/Payment' \
  -H 'accept: text/plain' \
  -H 'Content-Type: application/json' \
  -H 'Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJ0ZXN0ZUBjaW5lbWFyay5jb20iLCJleHAiOjE2NTYwMjc0MjQsImlzcyI6ImNoYXJsZXMubWVuZGVzIiwiYXVkIjoiY2hhcmxlcy5tZW5kZXMifQ.UubI-d6hL1KsqZiZxSoDbLHL2PG7k83qiS2TAgpkIWA' \
  -d '{
  "userId": "250c0e4d-b8bd-4724-8792-c16b40f00126",
}'
```

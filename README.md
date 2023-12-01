# Tech Lanches

Repositório dedicado ao projeto TechChallenge da FIAP - Turma 4SOAT

## Build & Tests
| CI/CD | Status |
| --- | --- | 
| Build & Unit Tests | [![.NET Build and Test](https://github.com/g12-4soat/tech-lanches/actions/workflows/build-tests.yml/badge.svg)](https://github.com/g12-4soat/tech-lanches/actions/workflows/build-tests.yml)
| TechLanches API | [![Build API Docker Image](https://github.com/g12-4soat/tech-lanches/actions/workflows/dockerfile-api-build-ci.yml/badge.svg)](https://github.com/g12-4soat/tech-lanches/actions/workflows/dockerfile-api-build-ci.yml)
| TechLanches Worker FilaPedidos | [![Build Worker Docker Image](https://github.com/g12-4soat/tech-lanches/actions/workflows/dockerfile-worker-build-ci.yml/badge.svg)](https://github.com/g12-4soat/tech-lanches/actions/workflows/dockerfile-worker-build-ci.yml)
| Docker Compose | [![Build Docker Compose](https://github.com/g12-4soat/tech-lanches/actions/workflows/docker-compose-build-ci.yml/badge.svg)](https://github.com/g12-4soat/tech-lanches/actions/workflows/docker-compose-build-ci.yml)

## Deployment
| CI/CD | Status |
| --- | --- | 
| API Docker Publish Develop | [![API Docker Publish Develop](https://github.com/g12-4soat/tech-lanches/actions/workflows/docker-publish-api-develop.yml/badge.svg)](https://github.com/g12-4soat/tech-lanches/actions/workflows/docker-publish-api-develop.yml) | 
| Worker Docker Publish Develop | [![Worker Docker Publish Develop](https://github.com/g12-4soat/tech-lanches/actions/workflows/docker-publish-worker-develop.yml/badge.svg)](https://github.com/g12-4soat/tech-lanches/actions/workflows/docker-publish-worker-develop.yml) | 
| API Docker Publish | [![API Docker Publish](https://github.com/g12-4soat/tech-lanches/actions/workflows/docker-publish-api.yml/badge.svg)](https://github.com/g12-4soat/tech-lanches/actions/workflows/docker-publish-api.yml) | 
| Worker Docker Publish | [![Worker Docker Publish](https://github.com/g12-4soat/tech-lanches/actions/workflows/docker-publish-worker.yml/badge.svg)](https://github.com/g12-4soat/tech-lanches/actions/workflows/docker-publish-worker.yml) | 

# Dependências
- [Docker](https://docs.docker.com/desktop/)
- [Docker Compose](https://docs.docker.com/compose/install/)

# Executar o projeto

O procedimento de inicialização do projeto é simples e leva poucos passos: 

1. Clone o repositório: _[https://github.com/g12-4soat/tech-lanches](https://github.com/g12-4soat/tech-lanches.git)_
 
1. Abra a pasta via linha de comando no diretório escolhido no **passo 1**. _Ex.: c:\> cd “c:/projetos/tech-lanches”_

1. Da raiz do repositório, onde se encontra o arquivo _**docker-compose.yml**_ _(Ex.: c:/tech-lanches)_, execute o comando no terminal:
> c:\tech-lanches> docker-compose up

Com o projeto inicializado, você terá acesso aos links abaixo e poderá abri-los em uma aba do seu navegador:

- Swagger: [http://localhost:5050/swagger/index.html](http://localhost:5050/swagger/index.html)
- Swagger Json: [http://localhost:5050/swagger/v1/swagger.json](http://localhost:5050/swagger/v1/swagger.json)  
- Redoc: [http://localhost:5050/api-docs/index.html](http://localhost:5050/api-docs/index.html)

Para importar as collections do postman, basta acessar os links a seguir:
- Collection: https://github.com/g12-4soat/tech-lanches/blob/main/docs/TechLanches.postman_collection.json
- Local Environment: https://github.com/g12-4soat/tech-lanches/blob/main/docs/TechLanches-Local.postman_environment.json

> Por padrão, a API está configurada para ser executada na porta 5050, como definido no [docker-compose.yml](https://github.com/g12-4soat/tech-lanches/blob/main/docker-compose.yml). Caso tenha problemas de inicialização, verifique se a porta já está sendo utilizada.

---

Visite a nossa [Wiki](https://github.com/g12-4soat/tech-lanches/wiki)

---

Documentação DDD pelo [Miro](https://miro.com/app/board/uXjVModCVvo=/?share_link_id=379818088124)

---

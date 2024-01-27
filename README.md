<p dir="auto"><img src="https://github.com/g12-4soat/tech-lanches/blob/feature/README/src/TechLanches/Adapter/Driver/TechLanches.Adapter.API/wwwroot/SwaggerUI/images/android-chrome-192x192.png" alt="TECHLANCHES" title="TECHLANCHES" align="right" height="60" style="max-width: 100%;"></p>

# Tech Lanches

Repositório dedicado ao projeto TechChallenge da FIAP - Turma 4SOAT

## Problema

Há uma lanchonete de bairro que está expandindo devido seu grande sucesso. Porém, com a expansão e sem um sistema de controle de pedidos, o atendimento aos clientes pode ser caótico e confuso. Por exemplo, imagine que um cliente faça um pedido complexo, como um hambúrguer personalizado com ingredientes específicos, acompanhado de batatas fritas e uma bebida. O atendente pode anotar o pedido em um papel e entregá-lo à cozinha, mas não há garantia de que o pedido será preparado corretamente.

Sem um sistema de controle de pedidos, pode haver confusão entre os atendentes e a cozinha, resultando em atrasos na preparação e entrega dos pedidos. Os pedidos podem ser perdidos, mal interpretados ou esquecidos, levando à insatisfação dos clientes e a perda de negócios.

Em resumo, um sistema de controle de pedidos é essencial para garantir que a lanchonete possa atender os clientes de maneira eficiente, gerenciando seus pedidos e estoques de forma adequada. Sem ele, expandir a lanchonete pode acabar não dando certo, resultando em clientes insatisfeitos e impactando os negócios de forma negativa.

<p dir="auto">Fonte: <a href="https://www.fiap.com.br/" rel="nofollow">FIAP</a></p>

## Proposta

Para solucionar o problema, a lanchonete irá investir em um sistema de autoatendimento de fast food, que é composto por uma série de dispositivos e interfaces que permitem aos clientes selecionar e fazer pedidos sem precisar interagir com um atendente. E, para que isso aconteça, criamos um sistema de controle de pedidos robusto e eficiente para a lanchonete em expansão, visando otimizar o atendimento ao cliente e garantir uma gestão adequada dos pedidos e estoques. A arquitetura do sistema será centrada em um aplicativo de autoatendimento intuitivo, permitindo que os clientes personalizem seus pedidos de maneira fácil e rápida, escolhendo lanches, acompanhamentos, bebidas e sobremesas.

Nesse primeiro momento o sistema integrará apenas com a opção de pagamento via QR Code do Mercado Pago, porém no futuro poderão ser implementadas novas opções de pagamento com o objetivo de proporcionar uma maior flexibilidade aos clientes. 

A escalabilidade será um ponto-chave na arquitetura, permitindo que o sistema se adapte facilmente ao aumento e diminuição da demanda. Utilizamos as melhores tecnologias e práticas disponíveis no mercado para assegurar a eficiência operacional mesmo em momentos de alta demanda. A arquitetura sistêmica e de infraestrutura facilitará futuras atualizações e integrações no sistema.

# Documentação

<h4 tabindex="-1" dir="auto" data-react-autofocus="true">Stack</h4>

<p>
  <a target="_blank" rel="noopener noreferrer nofollow" href="https://camo.githubusercontent.com/ffd9b9f100120fd49ebdbe8064adec834a0927f7be93551d12804c85fb92a298/68747470733a2f2f696d672e736869656c64732e696f2f62616467652f432532332d3233393132303f7374796c653d666f722d7468652d6261646765266c6f676f3d637368617270266c6f676f436f6c6f723d7768697465"><img src="https://camo.githubusercontent.com/ffd9b9f100120fd49ebdbe8064adec834a0927f7be93551d12804c85fb92a298/68747470733a2f2f696d672e736869656c64732e696f2f62616467652f432532332d3233393132303f7374796c653d666f722d7468652d6261646765266c6f676f3d637368617270266c6f676f436f6c6f723d7768697465" data-canonical-src="https://img.shields.io/badge/CSHARP-6A5ACD.svg?style=for-the-badge&amp;logo=csharp&amp;logoColor=white" style="max-width: 100%;"></a>
  <a target="_blank" rel="noopener noreferrer nofollow" href="https://camo.githubusercontent.com/71ae40a5c68bd66e1cb3813f84a5b71dd3c270c8f2506143d33be1c23f0b0783/68747470733a2f2f696d672e736869656c64732e696f2f62616467652f2e4e45542d3531324244343f7374796c653d666f722d7468652d6261646765266c6f676f3d646f746e6574266c6f676f436f6c6f723d7768697465"><img src="https://camo.githubusercontent.com/71ae40a5c68bd66e1cb3813f84a5b71dd3c270c8f2506143d33be1c23f0b0783/68747470733a2f2f696d672e736869656c64732e696f2f62616467652f2e4e45542d3531324244343f7374796c653d666f722d7468652d6261646765266c6f676f3d646f746e6574266c6f676f436f6c6f723d7768697465" data-canonical-src="https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&amp;logo=dotnet&amp;logoColor=white" style="max-width: 100%;"></a>
  <a target="_blank" rel="noopener noreferrer nofollow" href="https://camo.githubusercontent.com/962d06ebd5fabc44e392464f770a47947bae95440f3de3a7dbc3701c0b0c089e/68747470733a2f2f696d672e736869656c64732e696f2f62616467652f4d6963726f736f667425323053514c2532305365727665722d4343323932373f7374796c653d666f722d7468652d6261646765266c6f676f3d6d6963726f736f667425323073716c253230736572766572266c6f676f436f6c6f723d7768697465"><img src="https://camo.githubusercontent.com/962d06ebd5fabc44e392464f770a47947bae95440f3de3a7dbc3701c0b0c089e/68747470733a2f2f696d672e736869656c64732e696f2f62616467652f4d6963726f736f667425323053514c2532305365727665722d4343323932373f7374796c653d666f722d7468652d6261646765266c6f676f3d6d6963726f736f667425323073716c253230736572766572266c6f676f436f6c6f723d7768697465" data-canonical-src="https://img.shields.io/badge/Microsoft%20SQL%20Server-CC2927?style=for-the-badge&amp;logo=microsoft%20sql%20server&amp;logoColor=white" style="max-width: 100%;"></a>
  <a target="_blank" rel="noopener noreferrer nofollow" href="https://camo.githubusercontent.com/bce5c9b25447afefd9c8dc63febce5936fbff659beee51466a130b41a2821a9b/68747470733a2f2f696d672e736869656c64732e696f2f62616467652f446f636b65722d3243413545303f7374796c653d666f722d7468652d6261646765266c6f676f3d646f636b6572266c6f676f436f6c6f723d7768697465"><img src="https://camo.githubusercontent.com/bce5c9b25447afefd9c8dc63febce5936fbff659beee51466a130b41a2821a9b/68747470733a2f2f696d672e736869656c64732e696f2f62616467652f446f636b65722d3243413545303f7374796c653d666f722d7468652d6261646765266c6f676f3d646f636b6572266c6f676f436f6c6f723d7768697465" data-canonical-src="https://img.shields.io/badge/Docker-2CA5E0?style=for-the-badge&amp;logo=docker&amp;logoColor=white" style="max-width: 100%;"></a>
  <a target="_blank" rel="noopener noreferrer nofollow" href="https://camo.githubusercontent.com/e342de77242cf9645ea1aefb92e0dcfa7cd2f15cdeb5b0124a19ac270d613e30/68747470733a2f2f696d672e736869656c64732e696f2f62616467652f6b756265726e657465732d3332366365352e7376673f267374796c653d666f722d7468652d6261646765266c6f676f3d6b756265726e65746573266c6f676f436f6c6f723d7768697465"><img src="https://camo.githubusercontent.com/e342de77242cf9645ea1aefb92e0dcfa7cd2f15cdeb5b0124a19ac270d613e30/68747470733a2f2f696d672e736869656c64732e696f2f62616467652f6b756265726e657465732d3332366365352e7376673f267374796c653d666f722d7468652d6261646765266c6f676f3d6b756265726e65746573266c6f676f436f6c6f723d7768697465" data-canonical-src="https://img.shields.io/badge/kubernetes-326ce5.svg?&amp;style=for-the-badge&amp;logo=kubernetes&amp;logoColor=white" style="max-width: 100%;"></a>
</p>

<details>
  <summary>Diagrama Arquitetural Sistêmica</summary>

  ## Arquitetura Sistêmica
A aplicação possuí atualmente uma estrutura monolítica que está modularizada, visando como objetivo implementar uma estrutura de micro serviços no decorrer do projeto. Utilizamos o Github para gerenciar todo o código fonte, implementando automações CI/CD através do Github Actions. Além disso, fazemos uso do DockerHub como Container Registry para gerenciar as imagens de containers do projeto. Todos os nossos serviços internos são gerenciados pelo Cluster Kubernetes, que realiza a orquestração de todos os recursos da aplicação.

- <b>API</b>: Tem como responsabilidade o recebimento e envio de requisições REST para a aplicação Tech Lanches.
- <b>Fila de Pedidos</b>: Serviço do tipo "background service" que executa e gerencia a fila de pedidos.
- <b>Application Core</b>: Responsável por implementar os principais requisitos da aplicação.
- <b>SQL Server</b>: Banco de dados relacional cujo a responsabilidade é cuidar do armazenamento de dados.
- <b>ACL Pagamento</b>: Intermediário entre a comunicação da API com o serviço externo do Mercado Pago, visando a proteção da API para que não seja impactado diretamente na aplicação caso algo no serviço seja modificado.
- <b>[Mercado Pago](https://www.mercadopago.com.br/developers/pt)</b>: Serviço externo utilizado para efetuação de pagamento dos pedidos. Para mais informações sobre a implementação clique no nome do serviço.
- <b>[NGROK](https://ngrok.com/)</b>: Utilizamos como intermediário para realizar a comunicação do Mercado Pago com a API Tech Lanches, atráves de uma URL estática que é enviada ao webhook do Mercado Pago, que após a efetivação do pagamento é enviado uma requisição para o NGrok com o status do pagamento que faz o redirecionamento para o endpoint responsável da API Tech Lanches. Para mais informações clique no nome da ferramenta.
- <b>[RABBITMQ](https://www.rabbitmq.com/)</b>: Utilizado para auxiliar a fila de pedido no controle e gerenciamento. Para mais informações clique no nome da ferramenta.

  <img src="https://github.com/g12-4soat/tech-lanches/blob/feature/README/docs/arquitetura-sistemica.png" style="max-width: 100%;">
  
</details>

<details>
  <summary>Diagrama Arquitetural Kubernetes</summary>

  ## Arquitetura Kubernetes (K8S)
Na arquitetura Kubernetes (K8S), utilizamos o próprio Kubernetes em conjunto com o Docker como provedor de infraestrutura, buscando explorar plenamente os recursos nativos oferecidos pelo Kubernetes. Dentro do cluster Kubernetes, estabelecemos o namespace "techlanches", designado para armazenar todos os recursos relacionados diretamente à aplicação. Adicionalmente, temos o namespace "kube-system", destinado a objetos criados pelo sistema Kubernetes. No contexto do namespace "techlanches", trabalhamos na segmentação dos componentes com base nas responsabilidades, buscando facilitar a compreensão visual da estrutura arquitetônica. Essa abordagem visa proporcionar uma organização clara e compreensível dos elementos envolvidos na aplicação.

- <b>TechLanches.API</b>: Implementamos um Deployment com replicas 1/1 para garantir a disponibilidade da aplicação durante períodos de baixa demanda. Além disso, incorporamos um serviço Load Balancer responsável pelo balanceamento de carga entre os Pods e expor a API para a internet na porta 5050 do Container. Para monitoramento, integramos Probes do tipo Liveness, que verifica a execução adequada da aplicação TechLanches e a resposta correta a todas as solicitações, assim como Readiness, que assegura que a aplicação está pronta para receber requisições. Adicionalmente, contamos com ConfigMap que nos auxilia no armazenamento de configurações, Secrets para armazenamento de dados e senhas privadas e também incluímos um HPA 1/5 para escalabilidade automática, aumentando a quantidade de instâncias em caso de utilização de memória superior a 50%. Esse processo de escalabilidade é revertido de forma proporcional à redução no uso de memória do container, garantindo alta disponibilidade e resiliência nos serviços.

- <b>TechLanches.WORKER</b>: Adotamos uma estrutura semelhante à empregada na API, exceto a ausência da implementação do serviço Load Balancer. Além disso, incorporamos um HPA 2/5 para escalabilidade automática, considerando-o como o motor da aplicação para otimizar eficiência em todo o processo do pedido.

- <b>TechLanches.SQL</b>: Optamos por implementar um StatefulSet neste contexto devido à sua adequação para lidar com bancos de dados. Isso se deve à sua característica de implementar cada réplica após a anterior estar 100% funcional, iniciando e encerrando os Pods em sequência, ao contrário do Deployment, que o faz em paralelo. O StatefulSet inclui replicas 1/1 e um serviço Load Balancer, semelhante ao contexto da API. No entanto, difere ao ser exposto para a internet na porta 1433, reservado exclusivamente para o ambiente de desenvolvimento. Por fim, incorporamos Secrets e PersistentVolumeClaim para realizar o provisionamento dinâmico de recursos, garantindo a persistência dos dados.

- <b>TechLanches.NGROK</b>: No contexto NGROK, desenvolvemos um recurso simples e semelhante ao contexto da API, porém o serviço Load Balancer NGROK, expõe para a internet na porta 4040. Desta forma não houve a necessidade de implementar os demais recursos tais como: HPA, Secrets, ConfigMaps e Probes.
A implementação do contexto NGROK surgiu da necessidade de um serviço capaz de receber o retorno de pagamentos via webhook do Mercado Pago e encaminhá-las para a API TechLanches. Dessa forma, atua como um intermediário entre o cliente TechLanches e a instituição Mercado Pago.

- <b>TechLanches.RABBITMQ</b>: Para o RABBITMQ, adotamos uma estrutura bem próxima à implementada para o SQL, com a exceção da porta exposta para a internet no Load Balancer, que é a 15672, destinada exclusivamente ao Dashboard da ferramenta. Essa escolha foi feita considerando que o RABBITMQ desempenha um papel crucial no auxílio ao Worker para controlar e gerenciar a fila de pedidos, sendo uma função fundamental para a agilidade dos processos.

- <b>TechLanches.METRICS</b>: O Metrics coleta métricas sobre os recursos do cluster, como pods e nodes, e disponibiliza essas métricas para ferramentas externas de monitoramento e análise.
  ServiceAccount (metrics-server): Tem como responsabilidade definir uma identidade que será usada pelos pods relacionados ao Metrics dentro do namespace kube-system.

  ClusterRole (system:aggregated-metrics-reader): Define regras de acesso que permitem leitura agregada de métricas, configurando permissões para acessar informações de pods e nodes do Kubernetes.

  ClusterRole (system:metrics-server): Este ClusterRole configura permissões adicionais para leitura de informações específicas, como configmaps e namespaces.

  RoleBinding (metrics-server-auth-reader): Associa a Role "extension-apiserver-authentication-reader" ao ServiceAccount "metrics-server" no namespace kube-system, tem como objetivo condecer permissões específicas para o Metrics apenas no namespace. 

  ClusterRoleBinding (system:metrics-server): Associa o ClusterRole "system:metrics-server" ao ServiceAccount "metrics-server" no namespace kube-system, tem como objetivo condecer permissões específicas para o Metricse em todo o Cluster. 

  Service (metrics-server): Esse serviço está expondo a porta 443, permitindo a comunicação externa com o Metrics.

  Deployment (metrics-server): Define um conjunto de pods chamado "metrics-server" no namespace kube-system. Os Pods contêm Probes, ServiceAccount e também Metrics e são gerenciados pelo Deployment.

  APIService (v1beta1.metrics.k8s.io): Define um recurso APIService para o Metrics, especificando a versão v1beta1 do grupo de métricas. Esse recurso permite a exposição de métricas do Kubernetes através da API.

  Em resumo, esse contexto configura e provisiona todos os recursos necessários para implementar o Metrics Server no Kubernetes, garantindo permissões adequadas e funcionalidade para coleta de métricas do cluster.

 <img src="https://github.com/g12-4soat/tech-lanches/blob/feature/README/docs/arquitetura-k8s.png" style="max-width: 100%;">

</details>

<details>
  <summary>Como executar o projeto?</summary>
  
## Executando o Projeto
O procedimento de inicialização do projeto é simples e leva poucos passos: 

1. Clone o repositório: _[https://github.com/g12-4soat/tech-lanches](https://github.com/g12-4soat/tech-lanches.git)_
 
1. Abra a pasta via linha de comando no diretório escolhido no **passo 1**. _Ex.: c:\> cd “c:/tech-lanches”_

## Via Kubernetes
Da raiz do repositório, entre no diretório _**./k8s**_ _(onde se encontram todos os manifestos .yaml para execução no kubernetes)_, dê um duplo clique no arquivo "apply-all.sh" ou execute o seguinte comando no terminal:

### Windows 
> PS c:\tech-lanches\k8s> sh apply-all.sh

### Unix Systems (Linux distros | MacOS)
> $ exec apply-all.sh

## Via Docker Compose

Da raiz do repositório, onde se encontra o arquivo _**docker-compose.yml**_ _(Ex.: c:/tech-lanches)_, execute o comando no terminal:
> c:\tech-lanches> docker-compose up

---
### Swagger & Redoc 
Com o projeto inicializado, você terá acesso aos links abaixo e poderá abri-los em uma aba do seu navegador:

- Swagger: [http://localhost:5050/swagger/index.html](http://localhost:5050/swagger/index.html)
- Swagger Json: [http://localhost:5050/swagger/v1/swagger.json](http://localhost:5050/swagger/v1/swagger.json)  
- Redoc: [http://localhost:5050/api-docs/index.html](http://localhost:5050/api-docs/index.html)

### Postman 
Para importar as collections do postman, basta acessar os links a seguir:
- Collection: https://github.com/g12-4soat/tech-lanches/blob/main/docs/TechLanches.postman_collection.json
- Local Environment: https://github.com/g12-4soat/tech-lanches/blob/main/docs/TechLanches-Local.postman_environment.json

> Por padrão, a API está configurada para ser executada na porta 5050, como definido no [docker-compose.yml](https://github.com/g12-4soat/tech-lanches/blob/main/docker-compose.yml). Caso tenha problemas de inicialização, verifique se a porta já está sendo utilizada.
  ---
</details>

<details>
  <summary>Como efetuar um pagamento via Qr Code no Mercado Pago?</summary>

   ## Pagamento via Qr Code no Mercado Pago
 
Para os testes de pagamento, utilizamos o Swagger ou o Postman para a execução dos endpoints. :rotating_light: <b>OBS:</b> No tópico anterior informamos como utilizar as ferramentas informadas. :rotating_light:

A realização do pagamento pode ocorrer de duas maneiras. A primeira envolve o fluxo mockado, que simula aleatoriamente o resultado do pagamento, seja ele aprovado ou recusado. Entretanto, mesmo em caso de recusa, há a possibilidade de realizar uma nova tentativa para efetuar o pagamento do pedido. A segunda opção é via integração do fluxo com o Mercado Pago, onde uma URL de pagamento é fornecida após o checkout. Essa URL pode ser aberta no browser, exibindo um QR Code na tela para efetuar o pagamento por meio do aplicativo do Mercado Pago.

Para testar ambos os fluxos, é obrigatório que o pedido esteja criado e o checkout tenha sido concluído. Isso permitirá prosseguir com o passo a passo dos fluxos de pagamento com sucesso.

  <h4>Fluxo de Pagamento Mockado:</h4>
  <ol>
    <li>Localize o endpoint relacionado ao fluxo de pagamento mockado dentro do contexto de pagamento, por exemplo: <code>/api/pagamentos/webhook/mockado</code></li>
    <li>Insira apenas o ID do pedido desejado no parâmetro <code>pedidoId</code> no corpo do JSON e execute a request.</li>
    <li>Verifique a resposta do endpoint. Se o pagamento for <code>recusado</code>, é permitido realizar uma nova tentativa de pagamento. Se o pagamento for <code>aprovado</code>, o pedido seguirá o fluxo da fila de pedidos com o status <code>PedidoEmPreparacao</code>.</li>
  </ol>

  <h4>Fluxo de Pagamento Integrado ao Mercado Pago:</h4> 
  <ol>
    <li>Após a conclusão do checkout, verifique a resposta do endpoint que retorna alguns parâmetros.</li>
    <li>Um desses parâmetros é o <code>urlData</code>, que é uma URL que, ao ser aberta no browser, exibirá o <code>QR Code</code> para efetuar o pagamento.</li>
    <li>Faça o download do aplicativo do Mercado Pago. (<a href="https://www.mercadopago.com.br/c/app" rel="nofollow">Android</a>/<a href="https://apps.apple.com/br/app/mercado-pago-banco-digital/id925436649" rel="nofollow">IOS</a>)</li>
    <li>Acesse a conta de teste do comprador no Mercado Pago. <code>Usuário Comprador</code> TESTUSER298503702 <code>Senha</code> Xb7hdlnygo</li>
    <li>Na parte inferior central da tela, clique na opção <code>"Pix"</code> e <code>escaneie o QR Code</code>.</li>
    <li>Pronto, seu pedido foi pago com sucesso no Mercado Pago! O pedido está seguindo o fluxo da fila de pedidos com o status <code>PedidoEmPreparacao</code>.</li>
    <li>Caso queira verificar o histórico da transação, clique em <code>"Atividade"</code> na parte inferior da tela e selecione seu pagamento.</li>
    <li>Se desejar conferir a conta de teste do vendedor no Mercado Pago, as credenciais estão a seguir. <code>Usuário Vendedor</code> TESTUSER451316434 <code>Senha</code> fhA3QgrGbg</li>
  </ol>
</details>

<details>
  <summary>Como executar o teste de carga?</summary>
  
 ## Executando o Teste de Carga
Para executar o teste de carga é necessário a instalação do [k6](https://k6.io/docs/get-started/installation/) conforme seu sistema operacional. 
Após a instalação do k6, apartir da raiz do repositório, entre no diretório **./test/TechLanches.StressTests** e execute o comando no terminal: 

  ###  
  > c:\tech-lanches\test\TechLanches.StressTests> k6 run hpa-test.js
---
</details>

<details>
  <summary>Versões</summary>

## Software
- C-Sharp - 10.0
- .NET - 6.0
- MSSQL Server - 2019
 --- 
</details>

# Dependências
- [Docker](https://docs.docker.com/desktop/)
- [Docker Compose](https://docs.docker.com/compose/install/)
- [k6](https://k6.io/docs/get-started/installation/)

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

---

## Vídeo Explicativo da Fase 2
Abaixo está o link disponível para o vídeo explicativo que detalha a estrutura da arquitetura sistêmica e infraestrutura, e como implementamos o Clean Architecture na aplicação, abordando de forma clara e objetiva os principais tópicos desta fase.

link xpto

---

Visite a nossa [Wiki](https://github.com/g12-4soat/tech-lanches/wiki)

---

Documentação DDD pelo [Miro](https://miro.com/app/board/uXjVModCVvo=/?share_link_id=379818088124)

---

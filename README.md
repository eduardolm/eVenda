# eVenda API
![License](/img/license.svg)
![Coverage](/img/coverage.svg)
![Version](/img/version.svg)

Projeto de serviços distribuídos, desenvovido durante Aceleração Avanade / Digital Innovation One.
Nesse projeto, foram criadas duas APIs, chamadas respectivamente de Warehouse e Sale.

## Objetivos
Desenvolver um sistema de serviços distribuídos, controlados por eventos.

## Tecnologias utilizadas
 + C#
 + .NET Core 3.1.14
 + XUnit
 + Github
 + Azure Service Bus
 + Microsoft SQL Server 2019

## Funcionamento
A API Warehouse controla o estoque. Essa API permite cadastro, listagem, alteração e exclusão de produtos.
A API Sale controla as vendas possuindo uma cópia fiel do estoque da API Warehouse.

Ambas as APIs utilizam serviços de mensageria para controlar os eventos.
A API Warehouse envia mensagens para cada uma das ações realizadas sobre os produtos. A API Sale recebe essas mensagens e executa as mesmas ações no seu banco de dados de estoque.
Já a API Sale cadastra, altera e exclui as vendas, atualizando seu estoque próprio a cada uma dessas ações e enviando um evento para que a API Warehouse faça o mesmo com seu banco de dados.

Para testar o funcionamento das APIs, é preciso possuir uma conta no Azure, com os serviços de mensageria devidamente configurados.
Para efeitos de demonstração, esses serviços já foram configurados e as strings de conexão serão enviadas para a plataforma da DIO.
As APIs utilizam o MS SQL Server para persistência dos dados.

Para maiores informações sobre os endpoints e os payloads passados, favor ler a documentação completa em: `docs\DOCUMENTATION.md`


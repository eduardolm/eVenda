# Documentação
## Instruções
Com as APIs em execução, podemos utilizar o Postman ou similar para realizar requisições aos endpoints das APIs.
Para todas as ações executadas tanto pela API Warehouse quanto Sale, são enviados eventos, de forma que a outra API possa executar as ações corretamente.

### Endpoints
#### Product
**Ação** | **Endpoint** | **Método**
------ | ----- | -----
Cadastro de produtos | _/product_ | POST
Listar todos os registros | _/product_ | GET
Retornar registro por id | _/product/{id}_ | GET
Alterar registro | _/product/{id}_ | PUT
Apagar registro | _/product/{id}_ | DELETE

#### Sale
**Ação** | **Endpoint** | **Método**
------ | ----- | -----
Cadastro de venda | _sale_ | POST
Listar todos os registros | _sale_ | GET
Retornar registro por id | _/sale/{id}_ | GET
Alterar registro | _/sale/{id}_ | PUT
Apagar registro | _/sale/{id}_ | DELETE

### Layout dos Payloads (Requests e Responses)
#### Product
##### POST - Cadastrar produtos
    {
    "Sku": "3604",
    "Name": "Bola Homem-Aranha",
    "Price": 60.88,
    "Quantity": 100
    }

##### GET - Listar todos os registros
    [
        {
        "sku": "3600",
        "name": "Bola Loki",
        "price": 60.88,
        "quantity": 90,
        "createdAt": "2020-12-18T13:44:35.339172",
        "updatedAt": "2020-12-18T13:45:15.047702",
        "id": 1
        }
    ]

##### GET - Listar por id
Informar o id na URI:

    {
    "sku": "3600",
    "name": "Bola Loki",
    "price": 60.88,
    "quantity": 90,
    "createdAt": "2020-12-18T13:44:35.339172",
    "updatedAt": "2020-12-18T13:45:15.047702",
    "id": 1
    }

##### PUT - Alteração
Informar o id na URI:

    {
    "Sku": "3602",
    "Name": "Bola Hulk",
    "Price": 60.88,
    "Quantity": 100
    }

##### DELETE - Remover produto
Informar o id na URI:

 - Sem payload


#### Sale
##### POST - Cadastro de venda
    {
    "ProductId": 5,
    "Quantity": 10
    }

##### GET - Listar todos os regisros
    [
        {
            "productId": 5,
            "quantity": 10,
            "total": 608.80,
            "createdAt": "2020-12-18T19:47:10.8886848",
            "updatedAt": "2020-12-18T19:47:10.888802",
            "id": 3
        }
    ]

##### GET - Listar por id
Informar id da URI:

    {
        "productId": 5,
        "quantity": 10,
        "total": 608.80,
        "createdAt": "2020-12-18T19:47:10.8886848",
        "updatedAt": "2020-12-18T19:47:10.888802",
        "id": 3
    }

##### PUT - Alterar venda
Informar o id na URI:

    {
    "ProductId": 1,
    "Quantity": 20
    }

##### DELETE - Cancelar venda
Informar o id na URI:

 - Sem payload
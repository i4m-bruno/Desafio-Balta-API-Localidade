## API de Localidades

## Descrição
Projeto de estudo feito em um desafio do grande https://balta.io/
API responsável pelo gerenciamento de localidades, permitindo operações de criação, leitura, atualização e exclusão de dados relacionados a localidades brasileiras.

## Tecnologias Utilizadas
- .NET 7
- SQL Server
- Azure App Service
- Azure SQL Database

## Endpoints

### GET /v1/api/localidade
**Descrição**: Retorna uma lista paginada de localidades.  
**Parâmetros**: 
- `page`: Número da página para a paginção (inteiro).
- `pageSize`: Quantidade de itens por página (inteiro).

### GET /v1/api/localidade/{id}
**Descrição**: Retorna uma localidade específica pelo seu código IBGE.  
**Parâmetros**: 
- `id`: Código IBGE da localidade (string).  
**Requer Autenticação**: Sim

### GET /v1/api/localidade/cidade/{cidade}
**Descrição**: Retorna uma localidade específica pelo nome da cidade.  
**Parâmetros**: 
- `cidade`: Nome da cidade (string).  
**Requer Autenticação**: Sim

### GET /v1/api/localidade/estado/{estado}
**Descrição**: Retorna uma lista de localidades de um estado específico.  
**Parâmetros**: 
- `estado`: Sigla do estado (string).  
**Requer Autenticação**: Sim

### POST /v1/api/localidade
**Descrição**: Cria uma nova localidade.  
**Corpo da Requisição**: Um objeto JSON representando a localidade.  
**Requer Autenticação**: Sim

### POST /v1/api/localidade/excel
**Descrição**: Faz o upload de um arquivo Excel para adicionar/atualizar localidades em massa.  
**Corpo da Requisição**: `multipart/form-data` com o arquivo Excel.  
**Requer Autenticação**: Sim

### PUT /v1/api/localidade
**Descrição**: Atualiza uma localidade.  
**Corpo da Requisição**: Um objeto JSON representando a localidade com o ID para atualização.  
**Requer Autenticação**: Sim (Apenas usuários com a role "Admin")

### DELETE /v1/api/localidade/delete/{id}
**Descrição**: Exclui uma localidade pelo seu código IBGE.  
**Parâmetros**: 
- `id`: Código IBGE da localidade (string).  
**Requer Autenticação**: Sim (Apenas usuários com a role "Admin")

## Hospedagem
Este serviço está hospedado no Azure, utilizando o Azure App Service para a aplicação e o Azure SQL Database para o banco de dados.
- `URL`: https://desafiolocalidadeapi20231022143958.azurewebsites.net/

## Contato
Para mais informações, entre em contato conosco através de [ossbruno@outlook.com](mailto:ossbruno@outlook.com).

## Agradecimentos
Deixo registrado minha gratidão aos colaborades Telmo e Ezequiel que estiveram presentes no desenvolvimento do projeto.

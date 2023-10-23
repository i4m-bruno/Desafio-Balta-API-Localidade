## API de Localidades

## Descrição
Projeto de estudo feito em um desafio do grande https://balta.io/
API responsável pelo gerenciamento de localidades, permitindo operações de criação, leitura, atualização e exclusão de dados relacionados a localidades brasileiras.

## Tecnologias Utilizadas
- .NET 7
- SQL Server
- Azure App Service
- Azure SQL Database

## Autenticação
A maioria dos endpoints exigem autenticação. Para obter acesso, é necessário fazer uma requisição para o endpoint de login e utilizar o token JWT fornecido nas requisições subsequentes.
---
### POST /v1/api/usuario/login
**Descrição**: Realiza o login e retorna um token JWT para autenticação.
- **Corpo da Requisição**: 
  ```json
  {
    "username": "admin@teste.com",
    "password": "SenhaSecreta@123"
  }
  ```
- **Resposta de Sucesso**: Um objeto contendo o token JWT.
  ```json
  {
    "token": "ey...your.jwt.token...xx"
  }
  ```
- **Resposta de Erro**: Um objeto contendo as mensagens de erro.
  ```json
  {
    "erros": ["mensagem de erro"]
  }
  ```

### POST /v1/api/usuario
**Descrição**: Cadastra um novo usuário.
- **Corpo da Requisição**: 
  ```json
  {
    "username": "novo_usuario",
    "password": "nova_senha"
  }
  ```
- **Resposta de Sucesso**: Uma mensagem indicando que o usuário foi criado com sucesso.
- **Resposta de Erro**: Um objeto contendo as mensagens de erro ou notificações de validação.
  ```json
  {
    "erros": ["mensagem de erro"],
    "notifications": ["mensagem de validação"]
  }
  ```

---

## Endpoints de Localidades

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

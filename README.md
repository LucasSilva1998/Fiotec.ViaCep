# FiotecViaCep - API de Consulta de Endereços

API RESTful desenvolvida em .NET 9 com arquitetura em camadas, responsável por consultar endereços a partir do CEP utilizando o serviço público ViaCEP. Centraliza o serviço de consulta para ser reutilizado por outras APIs, garantindo consistência e simplicidade na integração.

---

## Tecnologias Utilizadas

- ASP.NET Core 9.0
- HttpClientFactory para chamadas externas
- Polly para retry em falhas transientes
- CancellationToken suportado
- Swagger (Swashbuckle)
- Clean Architecture / DDD
- JsonSerializer (System.Text.Json)


---

## Como executar localmente

### Pré-requisitos

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)

### 1. Restaurar pacotes

dotnet restore

### 2. Executar API
cd fiotec-viaCep.API
dotnet run

---

## Configuração (appsettings.json)
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ViaCepSettings": {
    "BaseUrl": "https://viacep.com.br/ws/",
    "TimeoutSeconds": 5,
    "RetryCount": 3
  }
}

---

## Endpoints
GET	/api/endereco/{cep}	Consulta endereço pelo CEP<br><br>
200 OK → Retorna o endereço completo<br>
404 Not Found → CEP não encontrado<br>
500 Internal Server Error → Erro inesperado

# Exemplo de requisição:

GET http://localhost:5000/api/endereco/21040361


# Exemplo de retorno (200 OK):

<img width="353" height="271" alt="image" src="https://github.com/user-attachments/assets/6649ec55-7cd6-4ebf-bb29-0d81b75610e1" />

----

# Boas práticas aplicadas

- Clean Architecture / DDD: separação clara entre Presentation, Application e Infrastructure
- Middleware de tratamento de exceções: captura erros globais e retorna JSON padronizado
- Retry e Timeout: implementados via HttpClientFactory e Polly para chamadas externas
- CancellationToken: suporta cancelamento de requisições do cliente
- Swagger: documentação clara e interativa

# Testes

Testes de integração criados na camada Infra.Tests

Validam:

CEP válido → retorna endereço completo

CEP inexistente → retorna null (mapeado para 404)

## Executar testes:

dotnet test fiotec-viaCep.Infra.Tests

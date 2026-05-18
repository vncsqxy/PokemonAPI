# 🎯 PokemonAPI — Plataforma de Ingestão e Transformação de Dados Pokémon

<div align="center">

[![C#](https://img.shields.io/badge/C%23-11.0+-239120?style=for-the-badge\&logo=csharp)](https://learn.microsoft.com/pt-br/dotnet/csharp/)
[![.NET](https://img.shields.io/badge/.NET-8.0+-512BD4?style=for-the-badge\&logo=dotnet)](https://dotnet.microsoft.com/)
[![Firebase](https://img.shields.io/badge/Firebase-Realtime%20Database-FFCA28?style=for-the-badge\&logo=firebase)](https://firebase.google.com/)
[![Licença](https://img.shields.io/badge/Licen%C3%A7a-MIT-green?style=for-the-badge)](LICENSE)

### Plataforma desenvolvida para consumir, transformar e persistir dados Pokémon utilizando processamento assíncrono e arquitetura em camadas.

</div>

---

# 📖 Sobre o Projeto

O **PokemonAPI** é uma aplicação construída com **C# + .NET 8** que realiza:

* Consumo de dados da PokéAPI
* Transformação de dados em modelos de domínio
* Processamento de formas especiais Pokémon
* Persistência em Firebase Realtime Database
* Execução paralela com controle de concorrência
* Tratamento robusto de erros
* Logging estruturado

A solução foi desenvolvida aplicando conceitos modernos de arquitetura de software, concorrência segura e separação de responsabilidades.

---

# 🚀 Principais Funcionalidades

## ⚡ Processamento Assíncrono

* Requisições paralelas
* Controle de concorrência com `SemaphoreSlim`
* Uso de `async/await`
* Processamento não bloqueante

---

## 🧠 Arquitetura Organizada

* Separação em camadas
* Serviços desacoplados
* Injeção de dependência
* Regras de negócio isoladas

---

## 🛡️ Tratamento de Falhas

Utilizando Polly:

* Retry automático
* Timeout controlado
* Circuit Breaker
* Recuperação de falhas transitórias

---

## ☁️ Persistência em Nuvem

* Firebase Realtime Database
* Persistência em JSON
* Escritas em lote
* Estrutura otimizada para consultas

---

# 🏗️ Fluxo da Aplicação

```text
PokéAPI
   ↓
Cliente HTTP
   ↓
Transformação dos dados
   ↓
Processamento de formas especiais
   ↓
Persistência no Firebase
   ↓
Logs e monitoramento
```

---

# 🔄 Fluxo Técnico da Arquitetura

```text
┌─────────────────────┐
│     PokéAPI         │
│ Dados em JSON       │
└──────────┬──────────┘
           │
           ▼
┌────────────────────────────┐
│ PokemonHttpClient          │
│----------------------------│
│ • Retry automático         │
│ • Timeout                  │
│ • Controle de falhas       │
└──────────┬─────────────────┘
           │
           ▼
┌────────────────────────────┐
│ Serviço de Transformação   │
│----------------------------│
│ • DTO → Domínio            │
│ • Regras de negócio        │
│ • Formas especiais         │
│ • Cálculo de status        │
└──────────┬─────────────────┘
           │
           ▼
┌────────────────────────────┐
│ Repositório Firebase       │
│----------------------------│
│ • Escrita em lote          │
│ • Persistência             │
│ • Consultas                │
└──────────┬─────────────────┘
           │
           ▼
┌────────────────────────────┐
│ Firebase Realtime Database │
└────────────────────────────┘
```

---

# ⚡ Exemplo de Processamento Assíncrono

```csharp
public async Task<IEnumerable<Pokemon>> IngestPokemonDataAsync(
    IEnumerable<int> pokemonIds)
{
    var semaphore = new SemaphoreSlim(10);

    var tasks = pokemonIds.Select(async id =>
    {
        await semaphore.WaitAsync();

        try
        {
            var dto = await _pokemonHttpClient
                .FetchPokemonByIdAsync(id);

            return await _transformationService
                .TransformAsync(dto);
        }
        finally
        {
            semaphore.Release();
        }
    });

    var results = await Task.WhenAll(tasks);

    await _repository.SavePokemonBatchAsync(results);

    return results;
}
```

---

# 📁 Estrutura do Projeto

```text
PokemonAPI/
│
├── src/
│   ├── PokemonAPI.Core/
│   │   ├── Domain/
│   │   ├── Repositories/
│   │   └── Services/
│   │
│   ├── PokemonAPI.Infrastructure/
│   │   ├── Http/
│   │   ├── Persistence/
│   │   └── Transformers/
│   │
│   ├── PokemonAPI.Application/
│   │   ├── UseCases/
│   │   ├── Options/
│   │   └── Exceptions/
│   │
│   └── PokemonAPI.Presentation/
│
├── tests/
│
├── docs/
│
└── README.md
```

---

# 🧱 Organização das Camadas

```text
┌──────────────────────────┐
│      CAMADA VISUAL       │
│ Interface e ViewModels   │
└─────────────┬────────────┘
              │
┌─────────────┴────────────┐
│   CAMADA DE APLICAÇÃO    │
│ Fluxos e casos de uso    │
└─────────────┬────────────┘
              │
┌─────────────┴────────────┐
│    CAMADA DE DOMÍNIO     │
│ Regras de negócio        │
└─────────────┬────────────┘
              │
┌─────────────┴────────────┐
│ CAMADA DE INFRAESTRUTURA │
│ HTTP, Firebase e Logs    │
└──────────────────────────┘
```

---

# 📌 Conceitos Aplicados

| Conceito               | Aplicação                      |
| ---------------------- | ------------------------------ |
| Arquitetura em Camadas | Separação de responsabilidades |
| Injeção de Dependência | Serviços desacoplados          |
| Repository Pattern     | Abstração da persistência      |
| Async/Await            | Processamento paralelo         |
| Value Objects          | Objetos imutáveis              |
| Logging Estruturado    | Monitoramento                  |
| Retry Policies         | Recuperação automática         |

---

# ⚡ Concorrência

A aplicação utiliza:

* `Task.WhenAll`
* `SemaphoreSlim`
* `CancellationToken`
* Paralelismo controlado

Objetivo:

✅ Melhor desempenho
✅ Evitar excesso de conexões
✅ Processamento seguro

---

# 🧬 Formas Especiais Pokémon

O sistema possui suporte para:

* Mega Evolutions
* Gigantamax
* Formas Alola
* Outras variações

Cada forma possui regras específicas de transformação e cálculo de atributos.

---

# 📊 Logging e Observabilidade

Exemplo de log estruturado:

```json
{
  "Timestamp": "2026-05-18T14:25:30.123Z",
  "Level": "Information",
  "PokemonId": 25,
  "Forma": "Mega",
  "TempoMs": 145
}
```

---

# 🔐 Segurança

* HTTPS obrigatório
* Timeout configurável
* Credenciais fora do Git
* Validação de entrada
* Tratamento seguro de exceptions

---

# 📦 Tecnologias Utilizadas

| Tecnologia | Finalidade     |
| ---------- | -------------- |
| C# 11      | Linguagem      |
| .NET 8     | Plataforma     |
| RestSharp  | Cliente HTTP   |
| Firebase   | Banco em nuvem |
| Polly      | Resiliência    |
| Serilog    | Logs           |
| xUnit      | Testes         |
| Moq        | Mocking        |

---

# 🧪 Testes

## Testes Unitários

* Transformers
* Serviços
* Value Objects
* Cálculos

## Testes de Integração

* Firebase
* Cliente HTTP
* Pipeline completo

---

# 🚀 Como Executar

## Pré-requisitos

* .NET 8 SDK
* Visual Studio 2022 ou VS Code
* Firebase Realtime Database
* Git

---

## Clonar o Projeto

```bash
git clone https://github.com/vncsqxy/PokemonAPI.git

cd PokemonAPI
```

---

## Restaurar Dependências

```bash
dotnet restore
```

---

## Configurar Firebase

```json
{
  "Firebase": {
    "DatabaseUrl": "https://seu-projeto.firebaseio.com",
    "CredentialsPath": "firebase-credentials.json"
  }
}
```

---

## Compilar Projeto

```bash
dotnet build -c Release
```

---

## Executar Testes

```bash
dotnet test
```

---

## Executar Aplicação

```bash
dotnet run --project src/PokemonAPI.Presentation
```

---

# 📈 Fluxo Completo

```text
Buscar Pokémon
      ↓
Converter JSON
      ↓
Transformar dados
      ↓
Processar formas
      ↓
Persistir Firebase
      ↓
Gerar logs
```

---

# 👤 Autor

Desenvolvido por **@vncsqxy**

GitHub:
https://github.com/vncsqxy

---

# 🤝 Contribuições

1. Faça um fork
2. Crie uma branch
3. Commit suas alterações
4. Faça push
5. Abra um Pull Request

---

# 📄 Licença

MIT License

---

<div align="center">

### Projeto desenvolvido com foco em arquitetura limpa, concorrência segura e processamento assíncrono.

</div>

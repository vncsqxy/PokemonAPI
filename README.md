# 🎯 PokemonAPI - Platform de Ingestão e Transformação de Dados Pokémon

[![C#](https://img.shields.io/badge/C%23-11.0+-239120?style=flat-square&logo=csharp)](https://learn.microsoft.com/en-us/dotnet/csharp/)
[![.NET](https://img.shields.io/badge/.NET-8.0+-512BD4?style=flat-square&logo=dotnet)](https://dotnet.microsoft.com/)
[![REST](https://img.shields.io/badge/REST-API-FF6B6B?style=flat-square)](https://restfulapi.net/)
[![Firebase](https://img.shields.io/badge/Firebase-Realtime%20DB-FFCA28?style=flat-square&logo=firebase)](https://firebase.google.com/)
[![Async/Await](https://img.shields.io/badge/Concurrency-Async%2FAwait-00A4EF?style=flat-square)](https://learn.microsoft.com/en-us/dotnet/csharp/async)
[![License](https://img.shields.io/badge/License-MIT-green?style=flat-square)](LICENSE)

---

## 📋 Sumário Executivo

O **PokemonAPI** é uma plataforma **enterprise-grade** de ingestão, transformação e persistência de dados Pokémon com **processamento assíncrono em escala**. A solução consome dados da [PokéAPI](https://pokeapi.co/) com alta disponibilidade, aplica transformações complexas com suporte a variações de formas (Mega, G-Max, Alola), e persiste dados normalizados em **Firebase Realtime Database** com garantias de integridade.

Construído com **C# 11+** e **.NET 8**, o projeto implementa padrões arquiteturais enterprise como **Domain-Driven Design**, **Separation of Concerns**, **Repository Pattern** e **Dependency Injection** nativo.

---

## 🎯 Contexto de Negócio

### O Problema

Organizações que operam plataformas analíticas Pokémon enfrentam desafios críticos:

- **Fragmentação de dados**: Dados distribuídos em múltiplas fontes externas sem normalização
- **Inconsistência de formatos**: Variações de formas (Mega/G-Max/Alola) requerem tratamento especializado
- **Latência elevada**: Consultas síncronas causam bloqueio de threads e degradação de performance
- **Perda de oportunidades analíticas**: Dados em silos impedem insights agregados sobre metágame Pokémon

### A Solução

O PokemonAPI resolve esses desafios através de:

✅ **Ingestão assíncrona de alta performance** – Múltiplas requisições paralelas sem bloqueio  
✅ **Normalização de dados complexos** – Tratamento de polimorfismo (formas especiais via interfaces)  
✅ **Cálculos de estatísticas em tempo real** – Agregação de base stats com suporte a variações  
✅ **Persistência em cloud-native** – Firebase Realtime DB com JSON estruturado  
✅ **Tratamento robusto de erros** – Circuit breaker, retry policies, logging estruturado  

---

## 🏗️ Arquitetura e Fluxo de Dados

### Diagrama de Fluxo

# 📮 CepHub

> ⚠️ **Em desenvolvimento**

API REST em C# (.NET 6) para consulta de endereços por CEP, utilizando a API pública [ViaCEP](https://viacep.com.br/). Desenvolvida como desafio do Forma.

***

## 🧠 Plano de desenvolvimento
<center><img width="307" height="394" alt="image" src="https://github.com/user-attachments/assets/59b80835-c20a-4b3f-b520-2d7c762fdc45" />

## ✅ Funcionalidades

- Consulta de endereço a partir de um CEP via endpoint REST
- Normalização automática do CEP informado
- Registro de logs com timestamp a cada consulta realizada
- Listagem do histórico de consultas via endpoint REST

***

## ▶️ Como executar

1. Clone o repositório:
```bash
git clone https://github.com/lucasgls/cep-hub.git
cd cep-hub
```

2. Execute o projeto:
```bash
dotnet run --project CepHub
```

3. Acesse a documentação Swagger em:
```
http://localhost:<porta>
```

***

## 🔗 Endpoints

| Método | Rota | Descrição |
|--------|------|-----------|
| `GET` | `/api/cep/{cep}` | Consulta o endereço de um CEP |
| `GET` | `/api/cep/logs` | Retorna o histórico de consultas |

***

## 📋 Logs

A cada consulta, uma entrada é gravada automaticamente em `CepHub/Data/RegistroLogs.txt`:

```
[18-06-2026 14:30:00] 01310-100 | Avenida Paulista, Bela Vista, São Paulo - SP (Ibge: 3550308 | Gia: 1004 | Ddd: 11 | Siafi: 7107)
```

***

## 🛠️ Tecnologias

- C# / .NET 6
- ASP.NET Core Web API
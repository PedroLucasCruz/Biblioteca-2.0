# Biblioteca 2.0

## Descrição

Sistema de gerenciamento de biblioteca desenvolvido como parte da pós-graduação em Arquitetura de Sistemas .NET na FIAP. O projeto visa aplicar conceitos de arquitetura em camadas, injeção de dependência e acesso a dados utilizando Dapper.

## Tecnologias Utilizadas

- ASP.NET Core
- Dapper
- SQL Server
- Injeção de Dependência (IoC)
- xUnit (a ser implementado)
- Swagger (a ser implementado)
- Docker (a ser implementado)

## Estrutura do Projeto

- `Biblioteca.2.0.Application`: Contém a lógica de aplicação.
- `Biblioteca.2.0.Domain`: Entidades e interfaces do domínio.
- `Biblioteca.2.0.Infra.Data`: Implementações de repositórios utilizando Dapper.
- `Biblioteca.2.0.Crosscutting.IoC`: Configuração de injeção de dependência.
- `Biblioteca.2.0.Client`: Interface do usuário (a ser detalhada).

## Como Executar

1. Clone o repositório:
   ```bash
   git clone https://github.com/PedroLucasCruz/Biblioteca-2.0.git

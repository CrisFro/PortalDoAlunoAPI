# Portal Acadêmico

Um sistema desenvolvido para gerenciar alunos e turmas de forma eficiente, facilitando a relação entre eles e permitindo a edição e visualização de informações.

## Índice

- [Descrição](#descrição)
- [Tecnologias Utilizadas](#tecnologias-utilizadas)
- [Funcionalidades](#funcionalidades)
- [Instalação](#instalação)
- [Scripts de Banco de Dados](#scripts-de-banco-de-dados)

## Descrição

O Portal Acadêmico é uma aplicação web que permite a gestão de alunos e turmas, proporcionando uma interface amigável para realizar associações entre alunos e suas respectivas turmas. Com este sistema, você pode facilmente adicionar, editar e visualizar informações, tornando a gestão educacional mais simples e organizada.

## Tecnologias Utilizadas

- **ASP.NET Core**: Framework para construção de aplicações web.
- **Razor Pages**: Abordagem para construir páginas dinâmicas em ASP.NET.
- **Dapper**: Micro ORM utilizado para facilitar a interação com o banco de dados.
- **Bootstrap**: Framework CSS para estilização e layout responsivo.
- **HTML/CSS/JavaScript**: Tecnologias fundamentais para desenvolvimento web.
- **SQL Server**: Banco de dados utilizado para armazenar dados.

## Funcionalidades

- **Gerenciar Alunos**: Adicionar, editar e visualizar informações de alunos.
- **Gerenciar Turmas**: Adicionar, editar e visualizar informações de turmas.
- **Relacionar Alunos às Turmas**: Facilitar a associação entre alunos e suas respectivas turmas através de um formulário intuitivo.
- **Edição e Remoção de Relacionamentos**: Atualizar ou remover associações conforme necessário, garantindo flexibilidade na gestão.
- **Interface Responsiva**: Acesso fácil em diferentes dispositivos.

## Scripts de Banco de Dados

Os scripts para a criação e manipulação do banco de dados estão localizados na seguinte pasta do projeto:

- [PortalDoAluno.Infrastructure/DatabaseScripts](https://github.com/CrisFro/PortalDoAlunoAPI/tree/master/PortalDoAluno.Infrastructure/DatabaseScripts)

Esses scripts contêm as instruções necessárias para criar as tabelas e os relacionamentos conforme o modelo do banco de dados.

## Instalação

1. Clone o repositório:
   ```bash
   git clone https://github.com/CrisFro/PortalDoAluno.git

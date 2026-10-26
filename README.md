\# Sistema de Eleição



\## 1. Sobre o projeto



O Sistema de Eleição é uma aplicação desenvolvida em C# com .NET para simular o processo de uma eleição. O sistema permite cadastrar candidatos e eleitores, registrar votos e apurar os resultados.



O projeto foi desenvolvido com foco na qualidade de software e na utilização de testes automatizados com xUnit.



\## 2. Funcionalidades



\- Cadastro de candidatos com nome e número único.

\- Cadastro de eleitores com nome e título de eleitor simulado único.

\- Registro de votos para candidatos cadastrados.

\- Registro de votos em branco e nulos.

\- Prevenção de votos duplicados pelo mesmo eleitor.

\- Consulta dos candidatos cadastrados.

\- Apuração dos votos por candidato.

\- Contagem de votos em branco e nulos.

\- Identificação do vencedor ou de empate.



\## 3. Tecnologias utilizadas



\- C#

\- .NET 10

\- Git

\- GitHub

\- xUnit para testes automatizados



\## 4. Estrutura do projeto



\- `SistemaEleicao.Console`: aplicação de console e regras do sistema eleitoral.

\- `SistemaEleicao.Tests`: projeto de testes automatizados.

\- `SistemaEleicao.sln`: solução que reúne os projetos.



\## 5. Como executar o projeto



\### Pré-requisitos



\- SDK do .NET 10 instalado.

\- Git instalado, caso queira clonar o repositório.



\### Execução



No terminal, dentro da pasta do projeto, execute:



```powershell

dotnet run --project .\\SistemaEleicao.Console

```



O menu será exibido no terminal, permitindo escolher as opções disponíveis.



\## 6. Como executar os testes



Na pasta principal do projeto, execute:



```powershell

dotnet test

```



Os testes automatizados verificam o cadastro de candidatos, o cadastro de eleitores, o registro de votos e a apuração dos resultados.



Na última execução, os 13 testes foram aprovados, sem falhas.



\## 7. Convenção de commits



O projeto utiliza mensagens de commit baseadas no padrão Conventional Commits, incluindo tipos como:



\- `feat`: implementação de funcionalidades.

\- `fix`: correção de erros.

\- `test`: criação ou alteração de testes.

\- `refactor`: melhoria na organização do código.

\- `docs`: documentação.

\- `chore`: tarefas de configuração e manutenção.



\## 8. Repositório



GitHub: https://github.com/Yasmin-850/SistemaEleicao



\## 9. Observação



Os dados de candidatos, eleitores e votos são mantidos em memória durante a execução. Eles não são armazenados permanentemente após o encerramento do programa.




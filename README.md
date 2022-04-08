LOCADORA é um pequeno projeto de teste, feito utilizando
as tecnologias C#, Blazor, ASPNET.Core, WebApi e MySql.


Algumas considerações para executar o aplicativo:
- recomendável abrir utilizando o Visual Studio Community 2022;
- crie uma base de dados com o nome 'locadora', usuario 'root' e senha 'jurubeba';
- rode o script 'LOCADORA_SCRIPTS.sql' que está no diretório raíz do projeto;
- caso queira personalizar o ambiente de conexão, altere "as duas connectionStrings", que estão em "appsettings.json" (para o Dapper) e "Program.cs" (para o EF), que estão  no projeto 'Locadora.Server';
    ** obs -> não deu tempo de organizar e deixar apenas uma connectionString **

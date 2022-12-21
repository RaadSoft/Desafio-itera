# Desafio-itera
DESAFIO .NET C#

# IDE 
Visual Studio Communit 2022

# SDK
[ASP.NET CORE 5](https://dotnet.microsoft.com/en-us/download/dotnet/5.0)

# Autentificação
JWT

# Clone
git clone https://github.com/RaadSoft/Desafio-itera.git

# Executar Visual Studio Communit 2022
- Arquivos-> Abrir -> Projeto/Solução -> Escolher o arquivo Desafio_ITERA.sln
- Clicar em IIS Express-executar

# Executar command
- Entrar na pasta do repositorio
- Executar ```dotnet run``` 
*OBS. Verificar antes se o SDK esta instalado corretamente*

# Testar pelo Postman
- Importar a colection ITERA.postman_collection.json que esta na pasta postman dentro do respositorio.
- Trocar na aba variables do postamn a variavel url informando a url de execução exemplo: ```https://localhost:5001```
- A Colection já tem um Token salvo caso queira testar o endpoint login utilizar o usuario="admin" e password="admin123" em seguida pode trocar o token na aba Authorization do postman para o token retornado.


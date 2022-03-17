# MovieListApp
A full-stack application for CRUD operation on movies.
It's simple and easy to use, has multiple language versions support and form validators.
The web application is based on ASP.NET (dotnet 6) and vue.js. It's written 
in C# and JavaScript, also employs vualidate, Axios, and entity framework, 
Bulma is used as a CSS framework.

#### Dependencies
- vue
- dotnet
- entity framework
- axios
- vuelidate 

#### TODO
- docker-compose

#### How to run
```bash
git clone https://github.com/asadafasab/MovieListApp
cd  MovieListApp/MovieListApp/
docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=Password123!' -p 1433:1433 -d mcr.microsoft.com/mssql/server:latest

dotnet add package microsoft.entityframeworkcore
dotnet add package microsoft.entityframeworkcore.design
dotnet add package microsoft.entityframeworkcore.sqlserver

dotnet tool install --global dotnet-ef
dotnet ef database update
dotnet watch &
cd ../movielistappvue/
yarn add @vuelidate/core @vuelidate/validators
yarn serve
```


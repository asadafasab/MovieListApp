# MovieListApp


#### Dependencies
- vue
- dotnet
- entity framework
- axios
- vuelidate 

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


#### TODO
- docker-compose
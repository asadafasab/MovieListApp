FROM mcr.microsoft.com/dotnet/nightly/sdk:6.0 as build-env
WORKDIR /app

COPY *.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o out

# FROM mcr.microsoft.com/dotnet/nightly/aspnet:6.0
FROM mcr.microsoft.com/dotnet/nightly/sdk:6.0
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:5000
EXPOSE 5000
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet","MovieListApp.dll"]
FROM mcr.microsoft.com/dotnet/nightly/sdk:6.0 as build

WORKDIR /app
COPY ["MovieListApp.csproj", "./"]
COPY Setup.sh Setup.sh

RUN dotnet tool install --global dotnet-ef

RUN dotnet restore "./MovieListApp.csproj"
COPY . .
WORKDIR "/app/."

RUN /root/.dotnet/tools/dotnet-ef migrations add InitialMigrations
# RUN dotnet ef migrations add InitialMigrations

RUN chmod +x ./Setup.sh
CMD /bin/bash ./Setup.sh
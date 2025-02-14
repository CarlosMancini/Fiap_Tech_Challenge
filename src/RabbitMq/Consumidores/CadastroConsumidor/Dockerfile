FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

COPY src/RabbitMq/Consumidores/CadastroConsumidor/CadastroConsumidor.csproj RabbitMq/Consumidores/CadastroConsumidor/

COPY src/Core/Core.csproj Core/
COPY src/Infrastructure/Infrastructure.csproj Infrastructure/
COPY src/Microservicos/Cadastro/Cadastro.csproj Microservicos/Cadastro/

RUN dotnet restore RabbitMq/Consumidores/CadastroConsumidor/CadastroConsumidor.csproj

COPY . .

RUN dotnet build -c Release

RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "CadastroConsumidor.dll"]

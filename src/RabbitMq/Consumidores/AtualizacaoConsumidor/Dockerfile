FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

COPY src/RabbitMq/Consumidores/AtualizacaoConsumidor/AtualizacaoConsumidor.csproj RabbitMq/Consumidores/AtualizacaoConsumidor/

COPY src/Core/Core.csproj Core/
COPY src/Infrastructure/Infrastructure.csproj Infrastructure/
COPY src/Microservicos/Atualizacao/Atualizacao.csproj Microservicos/Atualizacao/

RUN dotnet restore RabbitMq/Consumidores/AtualizacaoConsumidor/AtualizacaoConsumidor.csproj

COPY . .

RUN dotnet build -c Release

RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "AtualizacaoConsumidor.dll"]

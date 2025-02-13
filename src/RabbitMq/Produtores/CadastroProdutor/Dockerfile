FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

COPY src/RabbitMq/Produtores/CadastroProdutor/CadastroProdutor.csproj RabbitMq/Produtores/CadastroProdutor/

COPY src/Core/Core.csproj Core/

RUN dotnet restore RabbitMq/Produtores/CadastroProdutor/CadastroProdutor.csproj

COPY . .

RUN dotnet build -c Release

RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "CadastroProdutor.dll"]

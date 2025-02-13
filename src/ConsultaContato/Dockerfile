FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

COPY src/ConsultaContato/ConsultaContato.csproj ConsultaContato/

COPY src/Infrastructure/Infrastructure.csproj Infrastructure/

RUN dotnet restore ConsultaContato/ConsultaContato.csproj

COPY . .

RUN dotnet build -c Release

RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "ConsultaContato.dll"]

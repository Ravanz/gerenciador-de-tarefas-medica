# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia o arquivo de projeto e restaura dependências
COPY ["vSaude.csproj", "./"]
RUN dotnet restore "vSaude.csproj"

# Copia todo o código e faz o build
COPY . .
RUN dotnet build "vSaude.csproj" -c Release -o /app/build

# Publish
FROM build AS publish
RUN dotnet publish "vSaude.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "vSaude.dll"]



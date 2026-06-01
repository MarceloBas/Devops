# Stage 1: build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar csproj y restaurar dependencias primero (cacheable)
COPY app/src/Project3.Api.csproj ./ 
RUN dotnet restore --disable-parallel

# Copiar todo y publicar
COPY app/src/ ./ 
RUN dotnet publish -c Release -o /app/publish /p:ContinuousIntegrationBuild=true /p:Deterministic=true

# Stage 2: runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish ./
ENV ASPNETCORE_URLS=http://+:8081
EXPOSE 8081
ENTRYPOINT ["dotnet", "Project3.Api.dll"]

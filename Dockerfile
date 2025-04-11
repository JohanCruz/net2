# Imagen base para producción
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Imagen para compilación
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copiamos el csproj y restauramos dependencias
COPY Contactly.csproj ./
RUN dotnet restore "./Contactly.csproj"

# Copiamos el resto del código
COPY . ./

# Compilamos
RUN dotnet build "./Contactly.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publicamos
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Contactly.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Imagen final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Contactly.dll"]
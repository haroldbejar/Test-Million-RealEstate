# Usa la imagen del SDK de .NET para compilar la aplicación
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copia el archivo .csproj y restaura las dependencias para aprovechar el almacenamiento en caché de capas de Docker
COPY ["APIGateway/ApiGateway/ApiGateway.csproj", "APIGateway/ApiGateway/"]
RUN dotnet restore "APIGateway/ApiGateway/ApiGateway.csproj"

# Copia el resto del código fuente del proyecto
COPY ["APIGateway/ApiGateway/", "APIGateway/ApiGateway/"]
WORKDIR "/src/APIGateway/ApiGateway"
RUN dotnet build "ApiGateway.csproj" -c Release -o /app/build

# Publica la aplicación
FROM build AS publish
RUN dotnet publish "ApiGateway.csproj" -c Release -o /app/publish

# Construye la imagen final usando la imagen de tiempo de ejecución de ASP.NET
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Expone el puerto 8080 para la aplicación
EXPOSE 8080

# Define el punto de entrada del contenedor
ENTRYPOINT ["dotnet", "ApiGateway.dll"]

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY ["PropertyService/PropertyService.API/PropertyService.API.csproj", "PropertyService/PropertyService.API/"]
COPY ["PropertyService/PropertyService.Application/PropertyService.Application.csproj", "PropertyService/PropertyService.Application/"]
COPY ["PropertyService/PropertyService.Domain/PropertyService.Domain.csproj", "PropertyService/PropertyService.Domain/"]
COPY ["PropertyService/PropertyService.Infrastructure/PropertyService.Infrastructure.csproj", "PropertyService/PropertyService.Infrastructure/"]

RUN dotnet restore "PropertyService/PropertyService.API/PropertyService.API.csproj"

COPY PropertyService/. ./PropertyService/

WORKDIR "/src/PropertyService/PropertyService.API"
RUN dotnet publish "PropertyService.API.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "PropertyService.API.dll"]
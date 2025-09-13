FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY ["OwnerService/OwnerService.API/OwnerService.API.csproj", "OwnerService/OwnerService.API/"]
COPY ["OwnerService/OwnerService.Application/OwnerService.Application.csproj", "OwnerService/OwnerService.Application/"]
COPY ["OwnerService/OwnerService.Domain/OwnerService.Domain.csproj", "OwnerService/OwnerService.Domain/"]
COPY ["OwnerService/OwnerService.Infrastructure/OwnerService.Infrastructure.csproj", "OwnerService/OwnerService.Infrastructure/"]

RUN dotnet restore "OwnerService/OwnerService.API/OwnerService.API.csproj"

COPY OwnerService/. ./OwnerService/

WORKDIR "/src/OwnerService/OwnerService.API"
RUN dotnet publish "OwnerService.API.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "OwnerService.API.dll"]
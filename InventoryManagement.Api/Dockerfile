FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY . .

RUN dotnet restore InventoryManagement.Api/04_InventoryManagement.Api.csproj
RUN dotnet publish InventoryManagement.Api/04_InventoryManagement.Api.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

ENV ASPNETCORE_URLS=http://+:8080

COPY --from=build /app/publish .
	
EXPOSE 8080

ENTRYPOINT ["dotnet", "04_InventoryManagement.Api.dll"]
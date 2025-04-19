FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY ./APIGateway /app/APIGateway

RUN dotnet restore /app/APIGateway/APIGateway.WebApi/APIGateway.WebApi.csproj
RUN dotnet publish /app/APIGateway/APIGateway.WebApi/APIGateway.WebApi.csproj -c Release -o /app/out

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

COPY --from=build /app/out .

EXPOSE 80
ENTRYPOINT ["dotnet", "APIGateway.WebApi.dll"]

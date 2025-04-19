FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY ./Documents /app/Documents

RUN dotnet restore /app/Documents/Documents.WebApi/Documents.WebApi.csproj
RUN dotnet publish /app/Documents/Documents.WebApi/Documents.WebApi.csproj -c Release -o /app/out

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

COPY --from=build /app/out .

EXPOSE 80
ENTRYPOINT ["dotnet", "Documents.WebApi.dll"]

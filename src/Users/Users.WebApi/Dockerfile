FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# O caminho agora está certo com o contexto raiz
COPY ./Users /app/Users

RUN dotnet restore /app/Users/Users.WebApi/Users.WebApi.csproj
RUN dotnet publish /app/Users/Users.WebApi/Users.WebApi.csproj -c Release -o /app/out

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

COPY --from=build /app/out .

EXPOSE 80
ENTRYPOINT ["dotnet", "Users.WebApi.dll"]

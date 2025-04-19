FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY ./PaintCalculation /app/PaintCalculation

RUN dotnet restore /app/PaintCalculation/PaintCalculation.WebApi/PaintCalculation.WebApi.csproj
RUN dotnet publish /app/PaintCalculation/PaintCalculation.WebApi/PaintCalculation.WebApi.csproj -c Release -o /app/out

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

COPY --from=build /app/out .

EXPOSE 80
ENTRYPOINT ["dotnet", "PaintCalculation.WebApi.dll"]

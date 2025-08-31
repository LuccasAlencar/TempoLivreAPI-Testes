# 1) Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# copia csproj e restaura
COPY *.csproj ./
RUN dotnet restore

# copia restante e compila
COPY . ./
RUN dotnet publish -c Release -o /app/publish

# 2) Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build /app/publish .

# garante que a aplicação ouça em 0.0.0.0:5000
ENV ASPNETCORE_URLS=http://+:5000
# variável de fuso (compatível com o banco)
ENV TZ=America/Sao_Paulo

EXPOSE 5000

ENTRYPOINT ["dotnet", "TempoLivreAPI.dll"]

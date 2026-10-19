# Imagen base parametrizable. Cambiando DOTNET_VERSION podés mover toda la app
# a otro runtime sin tocar nada más.
ARG DOTNET_VERSION=8.0

# ---------- Etapa de build ----------
FROM mcr.microsoft.com/dotnet/sdk:${DOTNET_VERSION} AS build
WORKDIR /src

# Copiamos solo los .csproj para aprovechar la caché de restore
COPY ["API Rest de Videos Juegos/API Rest de Videos Juegos.csproj", "API Rest de Videos Juegos/"]
COPY ["Aplicacion/Aplicacion.csproj", "Aplicacion/"]
COPY ["Dominio/Dominio.csproj", "Dominio/"]
COPY ["Infraestructura/Infraestructura.csproj", "Infraestructura/"]
RUN dotnet restore "API Rest de Videos Juegos/API Rest de Videos Juegos.csproj"

# Copiamos el resto del código
COPY . .

WORKDIR "/src/API Rest de Videos Juegos"
RUN dotnet publish "API Rest de Videos Juegos.csproj" -c Release -o /app/publish /p:UseAppHost=false

# ---------- Etapa de runtime ----------
FROM mcr.microsoft.com/dotnet/aspnet:${DOTNET_VERSION} AS final
WORKDIR /app
COPY --from=build /app/publish .

# Render expone el puerto 10000 por defecto
ENV ASPNETCORE_URLS=http://+:10000
EXPOSE 10000

ENTRYPOINT ["dotnet", "API Rest de Videos Juegos.dll"]

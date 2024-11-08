# Utiliza la imagen oficial de .NET SDK para construir la aplicación
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

# Copia los archivos del proyecto y restaura las dependencias
COPY Web/*.csproj ./Web/
WORKDIR /app/Web
RUN dotnet restore

# Copia el resto de los archivos y compila la aplicación
COPY Web/. .
RUN dotnet publish -c Release -o out

# Utiliza una imagen runtime de .NET para ejecutar la aplicación
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
COPY --from=build /app/Web/out .

# Comando para ejecutar la aplicación
ENTRYPOINT ["dotnet", "ApiSampleFinal.dll"]

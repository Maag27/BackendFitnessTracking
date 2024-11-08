# Utiliza la imagen oficial de .NET SDK para construir la aplicación
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copia los archivos de proyecto y la solución
COPY Web/ApiSampleFinal.sln ./Web/
COPY Domain/Domain/Domain.csproj ./Domain/Domain/
COPY Infrastructure/Infrastructure/Infrastructure.csproj ./Infrastructure/Infrastructure/
COPY Services/Services/Services.csproj ./Services/Services/
COPY Web/ApiSampleFinal.csproj ./Web/

# Cambia el directorio de trabajo a /app/Web antes de ejecutar dotnet restore
WORKDIR /app/Web
RUN dotnet restore

# Copia el resto de los archivos y compila la aplicación
COPY . ./
RUN dotnet publish -c Release -o out

# Utiliza una imagen runtime de .NET para ejecutar la aplicación
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/Web/out .

# Comando para ejecutar la aplicación
ENTRYPOINT ["dotnet", "ApiSampleFinal.dll"]

# Etapa de construcción con la imagen oficial de .NET SDK 8.0
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiamos el archivo de solución y los archivos de proyecto para restaurar dependencias
COPY Web/ApiSampleFinal.sln ./Web/
COPY Domain/Domain/Domain.csproj ./Domain/Domain/
COPY Infrastructure/Infrastructure/Infrastructure.csproj ./Infrastructure/Infrastructure/
COPY Services/Services/Services.csproj ./Services/Services/
COPY Web/ApiSampleFinal.csproj ./Web/

# Restauramos las dependencias del proyecto
WORKDIR /src/Web
RUN dotnet restore

# Eliminamos carpetas de compilación anteriores
RUN find . -name obj -o -name bin | xargs rm -rf

# Copiamos el resto de los archivos y compilamos la aplicación
COPY . ./
RUN dotnet publish -c Release -o /app/out

# Etapa de ejecución con la imagen de runtime de .NET ASP.NET Core 8.0
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .

# Comando para ejecutar la aplicación
ENTRYPOINT ["dotnet", "ApiSampleFinal.dll"]

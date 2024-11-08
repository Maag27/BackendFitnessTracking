# Utiliza la imagen oficial de .NET SDK para construir la aplicación
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia los archivos de proyecto y la solución al lugar adecuado
COPY Web/ApiSampleFinal.sln .
COPY Domain/Domain/Domain.csproj Domain/Domain/
COPY Infrastructure/Infrastructure/Infrastructure.csproj Infrastructure/Infrastructure/
COPY Services/Services/Services.csproj Services/Services/
COPY Web/ApiSampleFinal.csproj Web/

# Restaura las dependencias del proyecto usando el archivo .sln
RUN dotnet restore ./ApiSampleFinal.sln

# Copia el resto de los archivos y compila la aplicación
COPY . .
WORKDIR /src/Web
RUN dotnet clean
RUN dotnet publish -c Release -o out

# Utiliza una imagen runtime de .NET para ejecutar la aplicación
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /src/Web/out .

# Comando para ejecutar la aplicación
ENTRYPOINT ["dotnet", "ApiSampleFinal.dll"]

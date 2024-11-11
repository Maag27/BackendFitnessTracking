# Etapa de construcción con la imagen oficial de .NET SDK 8.0
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar el archivo de solución y archivos de proyecto
COPY Web/ApiSampleFinal.sln ./Web/
COPY Domain/Domain/Domain.csproj ./Domain/Domain/
COPY Infrastructure/Infrastructure/Infrastructure.csproj ./Infrastructure/Infrastructure/
COPY Services/Services/Services.csproj ./Services/Services/
COPY Web/ApiSampleFinal.csproj ./Web/

# Restaurar las dependencias del proyecto
WORKDIR /src/Web
RUN dotnet restore

# Limpiar archivos generados en los directorios de todos los proyectos
RUN rm -rf /src/Web/obj /src/Web/bin
RUN rm -rf /src/Domain/Domain/obj /src/Domain/Domain/bin
RUN rm -rf /src/Infrastructure/Infrastructure/obj /src/Infrastructure/Infrastructure/bin
RUN rm -rf /src/Services/Services/obj /src/Services/Services/bin

# Copiar el resto de los archivos y publicar la aplicación
COPY . ./
RUN dotnet publish -c Release -o /app/out

# Etapa de ejecución con la imagen de runtime de .NET ASP.NET Core 8.0
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .

# Comando para ejecutar la aplicación
ENTRYPOINT ["dotnet", "ApiSampleFinal.dll"]

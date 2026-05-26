# Inventory Management API

## Descripción

API REST desarrollada con .NET 8 para la gestión de productos, categorías e inventario, implementando Clean Architecture, CQRS, Entity Framework Core y SQL Server.

---

# Requisitos Previos

Antes de ejecutar la aplicación asegúrese de tener instalado:

- .NET SDK 8
- .NET CLI
- SQL Server 2022 o superior
- SQL Server Management Studio (SSMS)
- Visual Studio 2022
- Git
- Docker Desktop (Opcional)

---

# Verificar Instalación de .NET

Abrir una terminal y ejecutar:

```bash
dotnet --version
```

El comando debe devolver una versión compatible con .NET 8.

Verificar Entity Framework CLI:

```bash
dotnet ef --version
```

Si la herramienta no está instalada, ejecutar:

```bash
dotnet tool install --global dotnet-ef
```

---

# Clonar el Repositorio

Abrir Git Bash o una terminal y ejecutar:

```bash
git clone <URL_DEL_REPOSITORIO>
cd InventoryManagement
```

---

# Estructura de la Solución

```text
InventoryManagement
│
├── docker-compose.yml
│
├── 04_InventoryManagement.Api
│   └── Dockerfile
│
├── 03_InventoryManagement.Infrastructure
├── 02_InventoryManagement.Application
├── 01_InventoryManagement.Domain
└── InventoryManagement.UnitTests
```

---

# Ejecución Local

## Crear Base de Datos

Desde SQL Server Management Studio ejecutar:

```sql
CREATE DATABASE InventarioDB;
```

---

## Configurar Cadena de Conexión

Editar el archivo síguente en la solución:

```text
04_InventoryManagement.Api/appsettings.json
```

Ejemplo:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost,1433;Database=InventarioDB;User Id=sa;Password=<PASSWORD>;TrustServerCertificate=True"
  }
}
```

Reemplazar `<PASSWORD>` por la contraseña configurada en su instancia local de SQL Server.

---

## Restaurar Dependencias

Ubicarse en la raíz de la solución y ejecutar:

```bash
dotnet restore
```

---

## Aplicar Migraciones

La aplicación está configurada para ejecutar migraciones automáticamente al iniciar.

Si se desea ejecutar manualmente:

```bash
dotnet ef database update --project 03_InventoryManagement.Infrastructure --startup-project 04_InventoryManagement.Api
```

---

## Ejecutar la Aplicación

```bash
dotnet run --project 04_InventoryManagement.Api
```

También es posible ejecutar la aplicación directamente desde Visual Studio 2022 estableciendo `04_InventoryManagement.Api` como proyecto de inicio y presionando **F5**.

---

## Acceder a Swagger

Una vez iniciada la aplicación, acceder a la URL mostrada por la consola de ASP.NET Core.

Por ejemplo:

```text
https://localhost:7110/swagger/index.html
```

Utilizar los datos de autenticación al dar click en el Boton Authorize:

-Datos de prueba en el archivo .env.example (AUTH_USERNAME y AUTH_PASSWORD)

---

# Ejecutar Pruebas Unitarias

Ubicarse en la raíz de la solución y ejecutar:

```bash
dotnet test
```

---

# Ejecución mediante Docker

## Requisitos

Antes de ejecutar la aplicación mediante contenedores asegúrese de tener instalado:

- Docker Desktop
- Docker Compose

Verificar instalación:

```bash
docker --version
docker compose version
```
---

```

## Levantar los Contenedores

Ubicarse en la raíz de la solución:

```bash
cd InventoryManagement
```
Ejecutar:

```bash
docker compose up -d --build
```

Este comando:

- Construye la imagen de la API.
- Crea el contenedor de SQL Server.
- Crea el contenedor de la API.
- Ejecuta automáticamente las migraciones pendientes.

---

## Verificar Contenedores

```bash
docker ps
```

Se deben mostrar los siguientes contenedores:

```text
inventory-api
sqlserver-inventory
```

---

## Acceder a Swagger

Abrir el navegador y acceder a:

```text
http://localhost:5000/swagger
```

Utilizar los datos de autenticación al dar click en el Boton Authorize:

-Datos de prueba en el archivo .env.example (AUTH_USERNAME y AUTH_PASSWORD)

---

## Consultar Logs

Logs de la API:

```bash
docker logs inventory-api
```

Logs de SQL Server:

```bash
docker logs sqlserver-inventory
```

---

## Detener Contenedores

Detener los contenedores sin eliminarlos:

```bash
docker compose stop
```

Reanudar los contenedores:

```bash
docker compose start
```

# Tecnologías Utilizadas

- .NET 8
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- MediatR
- CQRS
- FluentValidation
- Swagger / OpenAPI
- xUnit
- Moq
- Docker
- Docker Compose

---

# Autor

José Luis Fuentes Albarez.
Proyecto desarrollado por como parte de una evaluación técnica para la gestión de inventario utilizando Clean Architecture y buenas prácticas de desarrollo con .NET v8.


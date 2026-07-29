# 🏥 Sistema de Gestión Clínica - API REST

Sistema completo de gestión clínica desarrollado con .NET 10 y Angular, que permite administrar pacientes, citas médicas y registros clínicos a través de una API REST moderna y documentada.

![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?style=for-the-badge&logo=dotnet)
![C#](https://img.shields.io/badge/C%23-14.0-239120?style=for-the-badge&logo=c-sharp)
![SQL Server](https://img.shields.io/badge/SQL%20Server-Express-CC2927?style=for-the-badge&logo=microsoft-sql-server)
![Angular](https://img.shields.io/badge/Angular-22-DD0031?style=for-the-badge&logo=angular)
![License](https://img.shields.io/badge/License-MIT-green?style=for-the-badge)

---

## 📋 Tabla de Contenidos

- [Características](#-características)
- [Tecnologías](#-tecnologías)
- [Arquitectura](#-arquitectura)
- [Requisitos Previos](#-requisitos-previos)
- [Instalación](#-instalación)
- [Configuración](#-configuración)
- [Ejecución](#-ejecución)
- [Frontend (Angular)](#-frontend-angular)
- [API Endpoints](#-api-endpoints)
- [Documentación](#-documentación)
- [Base de Datos](#-base-de-datos)
- [CORS](#-cors)
- [Estructura del Proyecto](#-estructura-del-proyecto)
- [Contribuir](#-contribuir)
- [Licencia](#-licencia)

---

## ✨ Características

- ✅ **API RESTful** completa con operaciones CRUD
- ✅ **Documentación interactiva** con Scalar UI y OpenAPI
- ✅ **Arquitectura en capas** (Controllers, Data, Models)
- ✅ **Manejo de errores** centralizado y consistente
- ✅ **Validaciones** de datos robustas
- ✅ **CORS configurado** para aplicaciones frontend
- ✅ **Stored Procedures** para operaciones de base de datos
- ✅ **Respuestas estandarizadas** en formato JSON
- ✅ **Soporte HTTP y HTTPS** configurables

---

## 🚀 Tecnologías

### Backend
- **.NET 10** - Framework principal
- **ASP.NET Core** - Framework web
- **C# 14.0** - Lenguaje de programación
- **ADO.NET** - Acceso a datos
- **Microsoft.Data.SqlClient** - Proveedor de SQL Server
- **Scalar.AspNetCore** - Documentación API interactiva
- **OpenAPI** - Especificación de la API

### Frontend
- **Angular 22** (standalone components, sin `zone.js` / *zoneless*) - Framework frontend
- **Signals** (`signal()`) - Manejo de estado reactivo compatible con change detection zoneless
- **Reactive Forms** (`ReactiveFormsModule`) - Formularios con validaciones
- **TypeScript** - Lenguaje tipado
- **RxJS** - Programación reactiva (Observables del `HttpClient`)
- **Angular HttpClient** (`provideHttpClient(withFetch())`) - Consumo de API
- **Bootstrap 5.3.3** (CDN) - Estilos de formulario y tabla
- **Vitest** - Test runner (`npm test`)

### Base de Datos
- **SQL Server Express** - Sistema de gestión de base de datos
- **Stored Procedures** - Lógica de negocio en BD

---

## 🏗️ Arquitectura

```
┌─────────────────────────────────────────┐
│         Frontend (Angular)              │
│         Puerto: 4200                    │
└──────────────────┬──────────────────────┘
                   │ HTTP/HTTPS
                   │ CORS Enabled
┌──────────────────▼──────────────────────┐
│         API REST (.NET 10)              │
│         Puerto: 5146 (HTTP)             │
│                 7065 (HTTPS)            │
├─────────────────────────────────────────┤
│  ┌─────────────────────────────────┐   │
│  │   Controllers Layer             │   │
│  │   - PacientesController         │   │
│  └──────────────┬──────────────────┘   │
│  ┌──────────────▼──────────────────┐   │
│  │   Data Layer                    │   │
│  │   - Datos (ADO.NET)             │   │
│  │   - ConexionBase                │   │
│  └──────────────┬──────────────────┘   │
│  ┌──────────────▼──────────────────┐   │
│  │   Models                        │   │
│  │   - Paciente                    │   │
│  └─────────────────────────────────┘   │
└──────────────────┬──────────────────────┘
                   │ ADO.NET
┌──────────────────▼──────────────────────┐
│     SQL Server Express                  │
│     Base de datos: Clinica              │
│     - Stored Procedures                 │
│     - Tablas normalizadas               │
└─────────────────────────────────────────┘
```

---

## 📦 Requisitos Previos

Antes de comenzar, asegúrate de tener instalado:

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0) o superior
- [SQL Server Express](https://www.microsoft.com/sql-server/sql-server-downloads) o SQL Server
- [Visual Studio 2026](https://visualstudio.microsoft.com/) o [Visual Studio Code](https://code.visualstudio.com/)
- [Node.js](https://nodejs.org/) (para el frontend Angular)
- [Git](https://git-scm.com/)

---

## 🔧 Instalación

### 1. Clonar el repositorio

```bash
git clone https://github.com/car-albavel/Prueba_Clinica.git
cd Prueba_Clinica
```

### 2. Restaurar dependencias del backend

```bash
cd Api_Clinica/WebApiClinica
dotnet restore
```

### 3. Configurar base de datos

Ejecuta el script SQL para crear la base de datos y los stored procedures:

```bash
# El script SQL está en: /Database/script.sql
```

O usa SQL Server Management Studio (SSMS) para ejecutar el script manualmente.

---

## ⚙️ Configuración

### 1. Configurar la cadena de conexión

Edita el archivo `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "StrClinica": "Server=localhost\\SQLEXPRESS2;Database=Clinica;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

**Nota:** Ajusta el servidor según tu instalación de SQL Server.

### 2. Configurar CORS (si usas frontend en otro puerto)

El proyecto ya tiene CORS configurado para `http://localhost:4200` (Angular).

Para agregar más orígenes, edita `Program.cs`:

```csharp
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", policy =>
    {
        policy.WithOrigins(
            "http://localhost:4200",  // Angular
            "http://localhost:3000"   // Agrega más si necesitas
        )
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials();
    });
});
```

---

## ▶️ Ejecución

### Opción 1: Visual Studio

1. Abre la solución `WebApiClinica.slnx` en Visual Studio
2. Presiona `F5` para ejecutar en modo debug
3. El navegador abrirá automáticamente en `http://localhost:5146/scalar/v1`

### Opción 2: Línea de comandos

```bash
cd Api_Clinica/WebApiClinica
dotnet run
```

La API estará disponible en:
- **HTTP:** `http://localhost:5146`
- **HTTPS:** `https://localhost:7065`
- **Documentación:** `http://localhost:5146/scalar/v1`

---

## 💻 Frontend (Angular)

El frontend se encuentra en `Angular_Clinica/ProyectoClinica` y consume el módulo de Pacientes de la API.

### Instalación

```bash
cd Angular_Clinica/ProyectoClinica
npm install
```

### Configuración

La URL base de la API se configura en el servicio [src/app/Services/services.ts](Angular_Clinica/ProyectoClinica/src/app/Services/services.ts):

```typescript
url: string = 'http://localhost:5146/api/pacientes';
```

Ajusta este valor si tu API corre en un puerto o host diferente. Recuerda que el backend debe tener habilitado **CORS** para `http://localhost:4200` (ver sección [CORS](#-cors)).

### Ejecución

```bash
npm start
```

La aplicación estará disponible en `http://localhost:4200`.

Otros scripts disponibles:

```bash
npm run build   # Compilación de producción
npm run watch   # Build en modo desarrollo con watch
npm test        # Pruebas unitarias con Vitest
```

### Funcionalidades implementadas

- ✅ **Listado de pacientes** en tabla (Bootstrap) consumiendo `GET /api/pacientes`
- ✅ **Crear paciente** mediante formulario reactivo (`POST /api/pacientes`)
- ✅ **Editar paciente** precargando el formulario con los datos seleccionados (`PUT /api/pacientes/{id}`)
- ✅ **Eliminar paciente** con confirmación (`DELETE /api/pacientes/{id}`)
- ✅ **Validaciones de formulario**: campos requeridos (documento, nombre, fecha de nacimiento, género) y formato de correo electrónico
- ✅ **Mensajes de éxito/error** mostrados como alertas de Bootstrap

### Estructura relevante

```
Angular_Clinica/ProyectoClinica/
├── src/
│   ├── index.html                       # Incluye CDN de Bootstrap 5.3.3
│   ├── main.ts                          # bootstrapApplication (standalone, sin zone.js)
│   └── app/
│       ├── app.config.ts                # provideRouter, provideHttpClient(withFetch())
│       ├── app.routes.ts                 # Ruta 'pacientes' + redirect por defecto
│       ├── Models/
│       │   ├── respuesta-paciente.ts     # Interfaz de respuesta del API (GET)
│       │   └── peticion-paciente.ts      # Interfaz para crear/editar (POST/PUT)
│       ├── Services/
│       │   └── services.ts               # Servicio HTTP (getPacientes, addPaciente, editPaciente, deletePaciente)
│       └── Home/
│           └── pacientes/
│               ├── pacientes.ts          # Componente standalone (signals + Reactive Forms)
│               ├── pacientes.html        # Formulario + tabla de pacientes
│               └── pacientes.css
```

### Notas técnicas

- El proyecto **no usa `zone.js`**, por lo que cualquier estado que se actualice de forma asíncrona (respuestas HTTP, timers, etc.) debe declararse con `signal()` para que la vista se refresque correctamente.
- Los valores de los `<select>` (Tipo de documento, Género) deben coincidir exactamente (string) con los valores almacenados en la base de datos; si los datos existentes no siguen un estándar (códigos vs. palabras completas), ajusta las opciones del formulario en `pacientes.html`.

---

## 🌐 API Endpoints

### Base URL
```
http://localhost:5146/api
```

### Módulo: Pacientes

| Método | Endpoint | Descripción |
|--------|----------|-------------|
| `GET` | `/pacientes` | Obtener todos los pacientes |
| `GET` | `/pacientes/{id}` | Obtener un paciente por ID |
| `POST` | `/pacientes` | Crear un nuevo paciente |
| `PUT` | `/pacientes/{id}` | Actualizar un paciente existente |
| `DELETE` | `/pacientes/{id}` | Eliminar un paciente |

### Ejemplo de uso

#### Obtener todos los pacientes

```bash
curl -X GET http://localhost:5146/api/pacientes
```

#### Crear un paciente

```bash
curl -X POST http://localhost:5146/api/pacientes \
  -H "Content-Type: application/json" \
  -d '{
    "tipoDocumento": "CC",
    "numeroDocumento": "1234567890",
    "nombrePaciente": "Juan Pérez",
    "fechaNacimiento": "1990-05-15",
    "correoElectronico": "juan@example.com",
    "genero": "M",
    "direccion": "Calle 123",
    "numeroTelefono": "3001234567",
    "activo": true
  }'
```

---

## 📖 Documentación

### Documentación Interactiva (Scalar UI)

Accede a la documentación interactiva completa en:

```
http://localhost:5146/scalar/v1
```

Características:
- ✅ Explorar todos los endpoints
- ✅ Probar la API directamente desde el navegador
- ✅ Ver esquemas de datos (modelos)
- ✅ Ejemplos de request/response
- ✅ Códigos de estado HTTP documentados

### OpenAPI JSON

El documento OpenAPI está disponible en:

```
http://localhost:5146/openapi/v1.json
```

### Documentación detallada

Para documentación completa sobre cómo consumir la API desde un frontend, consulta:

```
Api_Clinica/WebApiClinica/API_PACIENTES_DOCUMENTACION.md
```

---

## 🗄️ Base de Datos

### Estructura

**Base de datos:** `Clinica`  
**Servidor:** `localhost\SQLEXPRESS2`

### Tablas Principales

#### Pacientes
```sql
CREATE TABLE Pacientes (
    PacienteID INT PRIMARY KEY IDENTITY(1,1),
    TipoDocumento NVARCHAR(20),
    NumeroDocumento NVARCHAR(50) NOT NULL,
    Nombre NVARCHAR(100) NOT NULL,
    FechaNacimiento DATE,
    CorreoElectronico NVARCHAR(100),
    Genero NVARCHAR(10),
    Direccion NVARCHAR(200),
    NumeroTelefono NVARCHAR(20),
    Activo BIT DEFAULT 1
)
```

### Stored Procedures

| Procedimiento | Descripción |
|---------------|-------------|
| `sp_GetAllPacientes` | Obtener todos los pacientes |
| `sp_GetPacienteByID` | Obtener un paciente por ID |
| `sp_InsertPaciente` | Insertar un nuevo paciente |
| `sp_UpdatePaciente` | Actualizar un paciente existente |
| `sp_DeletePaciente` | Eliminar un paciente |

---

## 🔐 CORS

El proyecto tiene CORS configurado para permitir peticiones desde:

- `http://localhost:4200` (Angular - por defecto)

### Características CORS habilitadas:
- ✅ Cualquier header
- ✅ Cualquier método (GET, POST, PUT, DELETE)
- ✅ Credenciales (cookies/autenticación)

### Configuración

La configuración de CORS se encuentra en `Program.cs`:

```csharp
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});
```

---

## 📁 Estructura del Proyecto

```
Prueba_Clinica/
│
├── Api_Clinica/
│   └── WebApiClinica/
│       ├── Controllers/
│       │   ├── PacientesController.cs      # Endpoints REST de Pacientes
│       │   └── WeatherForecastController.cs # Ejemplo
│       │
│       ├── Data/
│       │   ├── ConexionBase.cs             # Clase base de conexión ADO.NET
│       │   ├── Datos.cs                    # Capa de acceso a datos
│       │   └── Paciente.cs                 # Modelo de Paciente
│       │
│       ├── Properties/
│       │   └── launchSettings.json         # Configuración de ejecución
│       │
│       ├── appsettings.json                # Configuración (Connection Strings)
│       ├── appsettings.Development.json    # Configuración de desarrollo
│       ├── Program.cs                      # Punto de entrada y configuración
│       ├── WebApiClinica.csproj            # Archivo de proyecto
│       ├── WebApiClinica.http              # Colección de pruebas HTTP
│       └── API_PACIENTES_DOCUMENTACION.md  # Documentación detallada
│
├── Angular_Clinica/
│   └── ProyectoClinica/                    # Frontend Angular 22 (standalone, zoneless)
│       ├── src/
│       │   ├── index.html                  # Incluye CDN de Bootstrap 5.3.3
│       │   ├── main.ts
│       │   └── app/
│       │       ├── app.config.ts           # Providers: router, HttpClient (withFetch)
│       │       ├── app.routes.ts           # Ruta 'pacientes'
│       │       ├── Models/
│       │       │   ├── respuesta-paciente.ts
│       │       │   └── peticion-paciente.ts
│       │       ├── Services/
│       │       │   └── services.ts         # Consumo del API de Pacientes
│       │       └── Home/
│       │           └── pacientes/          # Formulario + tabla CRUD de pacientes
│       ├── angular.json
│       └── package.json
│
├── Database/
│   └── script.sql                          # Scripts de base de datos
│
└── README.md                               # Este archivo
```

---

## 🎨 Modelo de Datos

### Paciente

```csharp
public class Paciente
{
    public int IdPaciente { get; set; }
    public string TipoDocumento { get; set; }
    public string NumeroDocumento { get; set; }
    public string NombrePaciente { get; set; }
    public DateTime FechaNacimiento { get; set; }
    public string CorreoElectronico { get; set; }
    public string Genero { get; set; }
    public string Direccion { get; set; }
    public string NumeroTelefono { get; set; }
    public bool Activo { get; set; }
}
```

**Campos requeridos:**
- `numeroDocumento` (string)
- `nombrePaciente` (string)

---

## 🧪 Testing

### Herramientas Recomendadas

- **Scalar UI** (incluido): `http://localhost:5146/scalar/v1`
- **Postman**: Importa la colección desde `WebApiClinica.http`
- **Thunder Client**: Extensión de VS Code
- **curl**: Línea de comandos

### Colección de pruebas

El archivo `WebApiClinica.http` contiene ejemplos de todas las peticiones:

```http
### GET - Obtener todos los pacientes
GET http://localhost:5146/api/pacientes

### POST - Crear paciente
POST http://localhost:5146/api/pacientes
Content-Type: application/json

{
  "tipoDocumento": "CC",
  "numeroDocumento": "1234567890",
  "nombrePaciente": "Juan Pérez",
  "fechaNacimiento": "1990-05-15",
  "correoElectronico": "juan@example.com",
  "genero": "M",
  "direccion": "Calle 123",
  "numeroTelefono": "3001234567",
  "activo": true
}
```

---

## 🔒 Seguridad

### Implementado
- ✅ Validación de entrada de datos
- ✅ Manejo de excepciones
- ✅ Prepared statements vía Stored Procedures
- ✅ CORS configurado correctamente
- ✅ HTTPS disponible

### Pendiente (Recomendaciones)
- 🔲 Autenticación JWT
- 🔲 Autorización basada en roles
- 🔲 Rate limiting
- 🔲 Logging estructurado
- 🔲 Auditoría de cambios

---

## 🚧 Roadmap

- [ ] Agregar autenticación con JWT
- [ ] Implementar autorización por roles
- [ ] Módulo de citas médicas
- [ ] Módulo de historias clínicas
- [ ] Módulo de médicos
- [ ] Reportes y estadísticas
- [ ] Notificaciones por correo
- [ ] Integración con sistemas externos
- [ ] Dockerización
- [ ] Pipeline CI/CD

---

## 🤝 Contribuir

Las contribuciones son bienvenidas. Para contribuir:

1. Fork el proyecto
2. Crea una rama para tu feature (`git checkout -b feature/AmazingFeature`)
3. Commit tus cambios (`git commit -m 'Add some AmazingFeature'`)
4. Push a la rama (`git push origin feature/AmazingFeature`)
5. Abre un Pull Request

---

## 👥 Autores

- **Carlos Albavel** - *Desarrollo inicial* - [@car-albavel](https://github.com/car-albavel)

---

## 📝 Licencia

Este proyecto está bajo la Licencia MIT. Ver el archivo `LICENSE` para más detalles.

---

## 📞 Soporte

Si tienes preguntas o necesitas ayuda:

1. **Documentación API:** http://localhost:5146/scalar/v1
2. **Issues:** [GitHub Issues](https://github.com/car-albavel/Prueba_Clinica/issues)
3. **Documentación detallada:** Consulta `API_PACIENTES_DOCUMENTACION.md`

---

## 🙏 Agradecimientos

- [ASP.NET Core](https://dotnet.microsoft.com/apps/aspnet)
- [Scalar UI](https://scalar.com/)
- [Angular](https://angular.io/)
- [SQL Server](https://www.microsoft.com/sql-server/)

---

<div align="center">

**⭐ Si te gusta este proyecto, dale una estrella en GitHub ⭐**

Hecho con ❤️ usando .NET 10

</div>

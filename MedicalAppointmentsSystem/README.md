# Medical Appointments System

### Proyecto Final — Prueba de Desempeño  
**Autor:** Juan Cabrera  
**GitHub:** [@CJuanS](https://github.com/CJuanS)

---

## Descripción General

**Medical Appointments System** es una aplicación de consola desarrollada en **C# (.NET 8)** con **Entity Framework Core** y base de datos **PostgreSQL**, que permite administrar doctores, pacientes y citas médicas.  
El sistema está diseñado con una arquitectura basada en servicios e interfaces, y utiliza **inyección de dependencias (DI)** para la gestión de datos.

---

## Características Principales

✅ Registro de pacientes y doctores  
✅ Asignación de citas médicas (con validaciones de ID y fecha)  
✅ Persistencia de datos mediante Entity Framework Core  
✅ Organización modular por servicios e interfaces  
✅ Sistema escalable para integrar envío de correos electrónicos  

---

## 🧩 Estructura del Proyecto

```
MedicalAppointmentsSystem/
│
├── Data/
│   └── AppDbContext.cs
│
├── Interfaces/
│   ├── IAppointmentService.cs
│   ├── IDoctorService.cs
│   ├── IPatientService.cs
│   └── IEmailService.cs
│
├── Models/
│   ├── Appointment.cs
│   ├── Doctor.cs
│   ├── Patient.cs
│   └── EmailLog.cs
│
├── Services/
│   ├── AppointmentService.cs
│   ├── DoctorService.cs
│   ├── PatientService.cs
│   └── EmailService.cs
│
├── appsettings.json
├── Program.cs
└── MedicalAppointmentsSystem.csproj
```

---

##  Requisitos Previos

### 🔹 1. Instalar .NET SDK
Descarga e instala la versión más reciente de .NET (8 o superior):  
[https://dotnet.microsoft.com/download](https://dotnet.microsoft.com/download)

### 🔹 2. Instalar PostgreSQL
Instala PostgreSQL y asegúrate de recordar:
- Usuario: `postgres`
- Contraseña: la que definas
- Puerto: `5432`

Verifica la instalación:
```bash
psql --version
```

### 🔹 3. Crear la base de datos
```bash
sudo -u postgres psql
CREATE DATABASE "MedicalAppointmentsDB";
\q
```

---

## ⚙️ Configuración de la conexión

Edita el archivo **`appsettings.json`** con tus credenciales de PostgreSQL:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=MedicalAppointmentsDB;Username=postgres;Password=tu_contraseña"
  }
}
```

---

## ▶ Ejecución del Proyecto

1. Abre una terminal en la carpeta del proyecto.
2. Ejecuta los siguientes comandos:

```bash
dotnet restore
dotnet build
dotnet run
```

3. El sistema creará automáticamente las tablas y datos iniciales (doctores y pacientes de ejemplo).
4. Aparecerá el menú interactivo para administrar:

```
 Medical Appointments System — Console + EF Core

[1] Listar doctores
[2] Listar pacientes
[3] Registrar paciente
[4] Registrar doctor
[5] Programar cita
[6] Listar citas
[0] Salir
```

---

##  Base de Datos

El sistema utiliza **Entity Framework Core (Code First)**.  
Las tablas se generan automáticamente al ejecutar la aplicación por primera vez:

- **Doctors**
- **Patients**
- **Appointments**
- **EmailLogs**

Regenerar la base desde cero:

```bash
sudo -u postgres dropdb MedicalAppointmentsDB
sudo -u postgres createdb MedicalAppointmentsDB
dotnet run
```

---

## Envío de Correos (opcional)

El servicio `EmailService.cs` está listo para integrarse con SMTP (por ejemplo Gmail u Outlook).  
Solo necesitas agregar tus credenciales y activarlo en `AppointmentService` para enviar confirmaciones al programar citas. No me dio tiempo a implementarlo.

---

## Tecnologías Utilizadas

| Componente | Descripción |
|-------------|-------------|
| **C# / .NET 8** | Lenguaje y framework principal |
| **Entity Framework Core** | ORM para PostgreSQL |
| **PostgreSQL** | Base de datos relacional |
| **Dependency Injection (DI)** | Gestión de servicios |
| **Console UI** | Interfaz de texto simple e interactiva |

---

## Recomendaciones

- Ejecuta el proyecto desde una terminal con permisos suficientes.  
- Verifica que PostgreSQL esté corriendo antes de usar el programa.  
- Si cambias el nombre de la base de datos, actualiza también el `appsettings.json`.

---

##  Licencia

Este proyecto se distribuye con fines académicos y de evaluación técnica.  
Desarrollado por **Juan Cabrera (@CJuanS)**.  
Todos los derechos reservados © 2025.

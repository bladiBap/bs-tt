# Solución Test - Backend y Frontend

# Tabla de Contenidos
* [Requisitos Previos](#requisitos-previos)
* [Estructura del Proyecto](#estructura-del-proyecto)
* [Instalación del Backend](#backend)
* [Instalación del Frontend](#frontend)
* [Decisiones Técnicas](#decisiones-técnicas)
* [Posibles Mejoras](#posibles-mejoras)

## Requisitos Previos

### Backend
- **SDK de .NET**: .NET 8.0 o superior ([Descargar](https://dotnet.microsoft.com/download))
- **PostgreSQL**: Versión 12 o superior ([Descargar](https://www.postgresql.org/download))

### Frontend
- **Flutter**: SDK 3.11.0 o superior ([Instalar Flutter](https://flutter.dev/docs/get-started/install))

### Herramientas
- **ngrok**: Para exponer el backend ([Descargar](https://ngrok.com/download))

---

## Estructura del Proyecto

```
bs-tt/
├── SolTestBackend/          # Backend ASP.NET Core
│   ├── SolTestBackend.Api/         # API principal (Controladores y contratos)
│   ├── SolTestBackend.Application/ # Lógica de aplicación (Commands, Queries, Handlers)
│   ├── SolTestBackend.Core/        # Interfaces y abstracciones
│   ├── SolTestBackend.Domain/      # Modelos de dominio
│   ├── SolTestBackend.Infrastructure/ # Persistencia, DbContext, Repositorios, QueriesHandlers
│   └── seed.sql            # Script inicial de datos
│
└── sol_test_mobile/         # Frontend Flutter
    ├── lib/                # Código fuente
    │   ├── core/               # servicios y utilidades
    │   ├── features/            # Funcionalidades específicas
    |   |  ├── products/           # Funcionalidad de productos
    |   |  |    ├── data/               # Modelos y repositorios(implementaciones)
    |   |  |    ├── domain/             # Entidades, casos de uso e interfaces(contratos)
    |   |  |    └── presentation/       # Bloc(Estados, eventos), widgets y screens
    |   |  ├── service_locator/         # Configuración de inyección de dependencias
    │   ├── main.dart            # Punto de entrada
    │   └── ...                  # Otros archivos y carpetas
    └── pubspec.yaml           # Configuración de dependencias
```

---

## Instalación y Configuración

### Backend

#### 1. Configurar la Conexión a la Base de Datos

El archivo `SolTestBackend/SolTestBackend.Api/appsettings.json` contiene la configuración de la DB,
reempleza con los datos correspondientes.
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=sol_db;Username=postgres;Password=root"
  }
}
```

#### 3. Aplicar Migraciones y Semilla de Datos

Navega a la carpeta del backend:

```bash
cd SolTestBackend
```

Restaura las dependencias de NuGet:

```bash
dotnet restore
```

Aplica las migraciones a la base de datos:

```bash
# Desde la carpeta SolTestBackend
dotnet ef database update --project SolTestBackend.Infrastructure --startup-project SolTestBackend.Api --context DomainDbContext
```

Importa los datos iniciales ejecutando el script `seed.sql`,
desde pgAdmin o el editor a eleccion, abre el archivo `seed.sql` y ejecútalo,
este se encuentra en la raiz de `SolTestBackend`.

#### 4. Ejecutar la Aplicación

Inicia la aplicación:

Selecciona el proyecto `SolTestBackend.Api` como proyecto de inicio en tu IDE (Visual Studio) y ejecuta la aplicación.
Abrir el navegador y dirigirse a `https://localhost:7234/swagger` para acceder a la documentación de la API generada por Swagger.


### Frontend

#### 1. Instalar Dependencias Flutter

Navega a la carpeta del frontend:

```bash
cd sol_test_mobile
```

Descarga todas las dependencias:

```bash
flutter pub get
```

#### Usar ngrok para Exponer el Backend Localmente

Para acceder al backend desde la app movil:

1. Descarga e instala [ngrok](https://ngrok.com/download)

2. En el sitio de ngrok, regístrate para obtener una cuenta gratuita y obtén tu token de autenticación.
3. Abre una terminal y ejecuta el siguiente comando para configurar tu token de autenticación:
```bash
ngrok config add-authtoken tu_token_aqui
```

4. Expone el backend local:
```bash
ngrok http url_del_backend_local:puerto
```

5. ngrok te proporcionará una URL pública como: `https://xxxx-xx-xxx-xxx.ngrok.io`

6. Usa esta URL en la app de Flutter para conectarte al backend:
   - Reemplaza `baseUrl` que esta en `lib/core/network/dio_client.dart` con la URL de ngrok.
   - Ejemplo: `https://xxxx-xx-xxx-xxx.ngrok.io/api/v1`

---

### Ejecutar el Frontend

Desde la carpeta `sol_test_mobile`:

#### En Emulador
```bash
emulator -avd DEVICE_NAME 
```
Reemplaza `DEVICE_NAME` con el nombre de tu emulador

En caso de no saber el nombre de tu emulador, puedes listar los disponibles con:
```bash
emulator -list-avds
```

Luego ejecuta la aplicación de flutter presionando el botón de "Run" en tu IDE o usando el siguiente comando en la terminal:
```bash
flutter run
```

# Decisiones Técnicas y Posibles Mejoras

## Decisiones Técnicas

### Backend

#### 1. **Arquitectura en Capas**
- **Decisión**: Se implementó una arquitectura limpia (Clean Architecture) con separación clara de responsabilidades.
- **Justificación**: Permite:
  - Mantenibilidad: Fácil de entender, extender y modificar
  - Testabilidad: Cada capa puede probarse independientemente
  - Flexibilidad: Cambios en la persistencia no afectan la lógica de negocio

#### 2. **CQRS con MediatR**
- **Decisión**: Se usó el patrón CQRS (Command Query Responsibility Segregation) con la librería MediatR.
- **Justificación**: 
  - Separación clara entre comandos (escritura) y queries (lectura)
  - Facilita validaciones y manejo de errores centralizado
  - Escalabilidad futura para CQRS completo

#### 3. **Domain-Driven Design (DDD)**
- **Decisión**: Se estructuró el dominio con Aggregates, Entities y Value Objects.
- **Justificación**:
  - Encapsulación de lógica de negocio en entidades
  - Validaciones en Domain Entities (no en Controllers)
  - Product y Currency como Aggregates para garantizar integridad

#### 4. **Dos Contextos de Base de Datos**
- **DomainDbContext**: Para operaciones de dominio (escritura)
- **PersistenceDbContext**: Para queries optimizadas (lectura)
- **Justificación**: Permite optimizar queries de lectura sin afectar el modelo de dominio

#### 5. **Versionamiento de API**
- **Decisión**: URL versioning (`/v1/`, `/v2/`)
- **Justificación**:
  - Permite múltiples versiones simultáneamente
  - Backward compatibility
  - Swagger soporta automáticamente versioning

### Frontend - Flutter

#### 1. **Clean Architecture con BLoC**
- **Decisión**: Arquitectura en capas (Presentation, Domain, Data) + BLoC para state management.
- **Justificación**:
  - Separación clara de responsabilidades
  - Fácil testing de lógica de negocio
  - Reutilización de usecases

#### 2. **BLoC/Cubit para State Management**
- **Decisión**: Se utilizó Cubit (versión simplificada de BLoC).
- **Justificación**:
  - Gestión de estado predecible
  - Separación entre lógica y UI
  - Fácil debugging con DevTools
  - Menos código que BLoC completo

#### 3. **GetIt para Inyección de Dependencias**
- **Decisión**: Service Locator con GetIt.
- **Justificación**:
  - Acceso global a dependencias
  - Singletons y factories simplificadas

#### 4. **Dio para HTTP Client**
- **Decisión**: Librería Dio para peticiones HTTP.
- **Justificación**:
  - Interceptors built-in
  - Timeout configurable
  - Manejo de errores robusto

#### 5. **Repository Pattern**
- **Decisión**: Abstracción de Data Layer con repositorio interfaz.
- **Justificación**:
  - Independencia de la fuente de datos
  - Facilita testing con mocks
  - Cambiar API/BD sin afectar usecases

#### 6. **Usecases para Lógica de Negocio**
- **Decisión**: Cada caso de uso es una clase independiente.
- **Justificación**:
  - Single Responsibility Principle
  - Fácil de testear

#### 7. **Sealed Classes para Estados**
- **Decisión**: Estados del Cubit como sealed classes (Dart 3.0+).
- **Justificación**:
  - Type-safety en switch expressions

---

## Posibles Mejoras

### Backend

#### 1. **Autenticación y Autorización**
- [ ] Implementar JWT authentication
- [ ] Refresh tokens y manejo seguro

#### 2. **Logging y Monitoring**
- [ ] Integrar Serilog con sinks (File, Seq, etc.)
- [ ] Application Insights para monitoreo
- [ ] Correlación de requests (TraceId)

#### 3. **Caching**
- [ ] Redis para caching distribuido
- [ ] Invalidación inteligente de cache
- [ ] Caching a nivel de query

#### 4. **Testing**
- [ ] Unit tests para modelos de dominio, commands y hadlers
- [ ] Integration tests para repositorios

### Frontend

#### 1. **Manejo de Errores Mejorado**
- [ ] Dialog personalizado para errores (Toast).
- [ ] Retry automático en fallos de red.
- [ ] Timeout handling con reintentos exponenciales.

#### 2. **Paginación**
- [ ] Infinite scroll en lista de productos
- [ ] Carga lazy de más productos

#### 3. **Búsqueda y Filtros**
- [ ] Debounce en búsqueda en tiempo real
- [ ] Filtros por precio, categoría
- [ ] Reset de filtros

#### 4. **Separacion de Componentes**
- [ ] Crear componentes reutilizables (SearchBar, Toast, ProductCard)
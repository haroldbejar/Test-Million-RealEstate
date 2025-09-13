Million_RealEstate 

RealEstate luxury 

Prueba técnica para la posición de Fullstack Developer (.NET + ReactJS). 
Este proyecto implementa una arquitectura distribuida con microservicios para gestionar propiedades, dueños y galería de imágenes, autenticación basada en JWT y una SPA desarrollada en React. 

Tabla de Contenido 

1. [Arquitectura del Proyecto] 

2. [Tecnologías Utilizadas] 

3. [Estructura de Carpetas] 

4. [Requisitos Previos] 

5. [Instalación y Ejecución] 

6. [Endpoints y API Gateway] 

7. [Autenticación y Seguridad] 

8. [Pruebas Unitarias] 

9. [Uso de Docker] 

10 [Consideraciones y Mejoras Futuras] 

 ________________________________________________________________________________________

1. Arquitectura del Proyecto 

La solución está compuesta por: 

Microservicios: 

ImageService: gestión de imágenes de propiedades. 

OwnerService: gestión de dueños. 

PropertyService: gestión de propiedades. 

AuthService: autenticación basada en JWT y emisión de tokens. 

API Gateway: implementado con Ocelot, actúa como punto de entrada único para todos los microservicios. 

Aplicación Web: SPA construida con React + Vite + Redux Toolkit. 

Bases de Datos: cada microservicio tiene su propia base de datos MongoDB. 

Orquestación: contenedores Docker gestionados con docker-compose. 

2. Tecnologías Utilizadas 

Backend 

.NET 9 C#, Ocelot API Gateway, JWT Authentication, xUnit (pruebas unitarias), MongoDB  

Frontend 

React 19 Vite, Redux Toolkit, Axios, Tailwind CSS, Vitest (pruebas unitarias) 

DevOps 

Docker, docker-compose 

3. Estructura de Carpetas 
REALSTATE-WORKSPACE/ 
├── API Gateway/ 
│   ├── ApiGateway/ 
│   └── ApiGateway.sln 
│ 
├── AuthService/ 
│   ├── .vscode/ 
│   ├── AuthService.API/ 
│   ├── AuthService.Application/ 
│   ├── AuthService.Domain/ 
│   ├── AuthService.Infrastructure/ 
│   ├── AuthService.Tests/ 
│   ├── AuthService.sln 
│   └── Dockerfile 
│ 
├── OwnerService/ 
│   ├── .vscode/ 
│   ├── OwnerService.API/ 
│   ├── OwnerService.Application/ 
│   ├── OwnerService.Domain/ 
│   ├── OwnerService.Infrastructure/ 
│   ├── OwnerService.Tests/ 
│   ├── OwnerService.sln 
│   └── Dockerfile 
│ 
├── PropertyService/ 
│   ├── .vscode/ 
│   ├── PropertyService.API/ 
│   ├── PropertyService.Application/ 
│   ├── PropertyService.Domain/ 
│   ├── PropertyService.Infrastructure/ 
│   ├── PropertyService.Tests/ 
│   ├── PropertyService.sln 
│   └── Dockerfile 
│ 
├── webapp/ 
│ 
├── docker-compose.yml 
└── Million-RealEstate.sln 

4. Requisitos Previos 

.NET SDK 9.0+ Node.js 20+ npm 10+ Docker Desktop Git 

5. Instalación y Ejecución 

Clonar el repositorio git clone https://github.com/haroldbejar/Test-Million-RealEstate.git 

Levantar la infraestructura con Docker docker-compose up --build -d 

Verificar que los contenedores estén corriendo docker ps 
El flujo de la app es el siguiente:
- al correr la aplicacio; aparece el dashboard mostrando las propiedades exsitenes.
- para crear una propiedad se debe estar logueado y tener un codigo valido de propietario
- los propietarios se registran ingresando en la opcion registrar, una vez se diligencia el formulario y se envia;
  este devuelve en un modal el codigo unico del propietario
- la opcion de crear se habilita una vez este el usuario admin logueadol (no hay formulario de usuarios, se crea por postmas o swagger)
- una vez logueado aparece la opción de crear y se procede a diligenciarlo para crear las propiedades
  

 

6. Endpoints y API Gateway 

El archivo de configuración de Ocelot (ocelot.json) define el enrutamiento. 

Rutas Públicas de la API 

Autenticación 

POST /account/login - Iniciar sesión 

POST /account/register - Registrar usuario 

Propiedades 

GET /properties - Listar todas las propiedades 

GET /properties/search - Buscar propiedades 

GET /properties/{id} - Obtener propiedad por ID 

Propietarios 

GET /owners - Listar todos los propietarios 

POST /owners - Crear nuevo propietario 

Recursos estáticos 

GET /images/{filename} - Acceder a imágenes de propiedades 

 

Autenticación y Seguridad 

Obtener un token JWT Realiza un POST a:  

POST /account/login Content-Type: application/json { "username": "admin", "password": "admin123" } 

 Respuesta: { "user": "admin", "email": "email@email.com",  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..." } 

Implementar mensajería entre microservicios usando RabbitMQ/Kafka. Agregar logs distribuidos y trazabilidad con OpenTelemetry. 
Configurar CI/CD con GitHub Actions para build y despliegue automático. Pruebas de integración y e2e para mayor cobertura. 

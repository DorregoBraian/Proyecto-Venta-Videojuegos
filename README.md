# Proyecto de Portafolio: Plataforma de Venta de Videojuegos

### Introducción

Este proyecto es una aplicación desarrollada como parte de mi portafolio, donde busqué combinar diversas tecnologías y habilidades que he aprendido recientemente. Principalmente, se trata de una plataforma para la venta de videojuegos similar a Steam, desarrollada con Angular para el frontend y varios microservicios en C# con .NET Core para el backend.

El objetivo principal de este proyecto fue aprender y aplicar Angular, junto con la implementación de APIs REST, pruebas unitarias y tareas en segundo plano. Este trabajo me permitió no solo mejorar mis conocimientos técnicos, sino también entender la integración entre sistemas frontend y backend en un entorno profesional.

# Explicación del Proyecto

### Descripción General

La plataforma es una API REST diseñada para gestionar la venta de videojuegos. Incluye funcionalidades clave como:

* **Carrito de Compras:** Los usuarios pueden agregar, modificar y eliminar videojuegos en su carrito.

* **Favoritos:** Posibilidad de marcar videojuegos como favoritos para acceso rápido.

* **Login y Registro de Usuarios:** Sistema de autenticación con recuperación de contraseña y opción de mantener la sesión abierta.

* **Localstorage y Sessionstorage:** Los usuarios guardan el estado de su sesion, sus carritos y favoritos.

* **Filtros Avanzados:** Permite buscar y filtrar videojuegos por género, precio, nombre, etc.

* **Envío de Correos SMTP:** Confirmación de registro y otras notificaciones automáticas mediante correo electrónico.

* **Integración en Tiempo Real:** Los cambios realizados en el backend, como la actualización de datos o la inclusión de nuevos videojuegos, se reflejan inmediatamente en la interfaz del usuario.

# Arquitectura del Backend

### El backend está compuesto por tres microservicios independientes:

1. **API REST Principal:** Diseñada para gestionar la información de videojuegos, usuarios y funcionalidades como paginación de lista de videojuegos , envio de correo para la recuperación de contraseña , Logan y registrar usuarios y distintos tipos de filtros. Este servicio interactúa directamente con la base de datos y expone los endpoints necesarios para el frontend.

2. **Microservicio de Testing:** Implementado para realizar pruebas unitarias y garantizar el correcto funcionamiento de los endpoints y lógica del sistema. Este microservicio asegura que los datos sean procesados y almacenados adecuadamente.

3. **Tarea en Segundo Plano:** Un servicio dedicado a procesar un archivo JSON que contiene videojuegos, cargando automáticamente los datos en la base de datos. Esto permite agregar nuevos productos de manera eficiente y mantener el sistema actualizado.

<br>

![Arquitectura hexagonal](https://github.com/user-attachments/assets/ce06fbbb-0ddd-44db-803e-f1081af31eb9)

<br>

### Frontend

El frontend está construido con Angular 18, utilizando un enfoque modular con microservicios dentro del frontend para manejar diferentes funcionalidades. Algunas características destacadas incluyen:

* **Diseño Moderno:** HTML, CSS, Bootstrap y Angular Material se combinaron para lograr una interfaz atractiva y responsiva.

* **Gestor de Estados:** Se mantiene el estado de la sesión y el carrito de compras utilizando servicios y localstorage y sessionstorage.

* **Modularidad:** Cada funcionalidad, como el carrito de compras, login y favoritos, está desarrollada en componentes y servicios independientes.

### Características Extra 

* La información se guarda en tiempo real en una base de datos gracias a Entity Framework Core.

* Se incluye funcionalidad de recuperación de contraseña, donde los usuarios pueden recibir un enlace por correo para cambiarla.

* Integración con Git y GitHub para control de versiones y despliegue.

<br>

![image](https://github.com/user-attachments/assets/daeceb27-1669-42c3-94b0-b7d8210b0f9e)

<br>

# Herramientas y Tecnologías

### Herramientas

* **Visual Studio Code:** Para el desarrollo del frontend.

* **Visual Studio:** Para el desarrollo del backend.

* **Git y GitHub:** Para control de versiones y colaboración.

### Tecnologías del Backend

* **C# y .NET Core:** Lenguaje y framework principal para los microservicios.

* **Entity Framework Core:** Para el manejo de bases de datos.

* **PostgreSQL:** Base de datos relacional utilizada.

### Tecnologías del Frontend

* **Angular 18:** Framework principal para el desarrollo del frontend.

* **Bootstrap y Angular Material:** Para diseños responsivos y componentes visuales.

* **HTML y CSS:** Para la estructura y estilos.

# Instalacion

### Proceso de instalación

1.	El primer paso es dirigirse a el Backend de Tareas en segundo plano, luego ir al archivo Worker.cs y cambiar la ruta del archivo DataGame.json. El método SeedDataAsync debe recibir la ruta en donde se encuentra ese archivo para luego leerlo.
<br>

![Cambiar direcion del archivo DaraGamin](https://github.com/user-attachments/assets/3cc6cbd2-a240-400a-952a-2a3768007a77)

<br>

3.	Configurar la ConnectionString en el archivo appsettings.json del proyecto principal que se llama API Rest de Videojuego. Hay tienen que introducir los datos necesarios para la conexión a la base de datos que deseen. Los paquetes NuGet que se encuentran instalados son los de PostgreSQL y SQLServer.
   
**Nota:** debe modificar tanto el archivo appsettings.json como el Program.cs para su correcto funcionamiento. 

<br>

![Configurar ConnectionString](https://github.com/user-attachments/assets/4f409fdc-4544-4630-a649-edfec4898d8d)

<br>

3.	El Backend principal y el Backend de Tareas en segundo plano se deben ejecutar al mismo tiempo para que la tarea en segundo plano se ejecute y llene la base de datos con la información del JSON que se encuentra en ese Backend. Para esto siga estos pasos:
    1. Haga clic derecho en la solución
    2. Valla a la opción de propiedades
    3. En la pestaña de Configuración de proyecto de inicio seleccione la opción de varios proyectos de inicio.
    4. En el cuadro de la derecha se verán los proyectos. Debe desplegar, en la columna de acciones.
    5. Debe poner tanto el proyecto llamado API Rest de Videos Juegos y el proyecto de Tarea en Segundo Plano con la opción de inicio.
    6. Al ejecutar el proyecto con normalidad debe compilarse los dos simultáneamente.
    
**Nota:** se puede crear perfiles para inicializar los proyectos que desee. Estos se pueden seleccionar a la izquierda del botón verde de inicio.

<br>

![Ejecucion de varios Proyectos](https://github.com/user-attachments/assets/b132b859-05e0-4679-99b9-9551556ae172)

<br>

4.	En el  Backend de Tareas en segundo plano se encuentra el archivo DataGame.json. en cual contiene todos los juegos que al ejecutar la aplicación se introducirán la base de datos. Al final de todo se encuentra una plantilla, comentada, en la que se puede introducir directamente un juego llenando los campos.

<br>

![Plantilla de DataGame](https://github.com/user-attachments/assets/cdad162a-7f1f-47bd-88a3-759fe3134b2c)

<br>


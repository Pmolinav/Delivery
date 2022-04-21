# API BACKEND PARA EMPRESA DE PAQUETERÍA

## Descripción    
Esta aplicación permite la interacción con los pedidos y vehículos existentes en la empresa de paquetería.

### Acciones disponibles
Dentro de la aplicación se contemplan diferentes acciones a realizar, tanto para los vehículos existentes, como para los pedidos realizados.
* Consultar los datos de todos los vehículos disponibles.
* Consultar los datos de todos los pedidos disponibles.
* Obtener la información de un único vehículo por su Id.
* Obtener la información de un único pedido por su Id.
* Obtener los datos de un vehículo por el conductor que lo lleva.
* Crear un nuevo vehículo.
* Crear un nuevo pedido.
* Modificar los datos referentes a un vehículo.
* Modificar la información de un pedido.
* Eliminar un vehículo.
* Eliminar un pedido.


## Compilación y ejecución del servicio    
 Al ser un proyecto de .NET Core 6, lo más recomendado es abrir la solución con la versión más actual posible de Visual Studio.
 
 Mediante Visual Studio, será posible compilar la solución y ejecutar la API (por ejemplo en un IIS Express) para poder comprobar su funcionamiento.

 Si se desea revisar los datos almacenados en base de datos, es conveniente realizar la conexión con Microsoft SQL Server Management Studio.
    

## Estructura del proyecto  
 Este proyecto posee, además de las creadas por defecto, diferentes carpetas diferentes para facilitar su comprensión y la distribución de sus clases. Estas carpetas son las siguientes:
 * Models. Contiene los modelos que se migrarán y estarán representados en base de datos para las tablas Vehículo y Pedido. También contiene una subcarpeta DTOs (Data Transfer Objects) donde se encuentran los diferentes objetos de datos creados para consultas, creación o inserción de datos.
 * Mapper. Utilizando el paquete Automapper que hemos instalado en nuestro proyecto, se establece un mapeado entre los modelos de datos y los DTOs creados. En esta carpeta podemos ver una clase que establece todas las asignaciones necesarias.
 * Data. Además de tener una subcarpeta "Migrations" con todas las migraciones realizadas en base de datos, esta carpeta contiene una clase que extiende de "DbContext" y que reflejará nuestro contexto de base de datos.
 * Enums. Esta carpeta auxiliar contiene los tipos "enum" que hemos utilizado en la aplicación. En este caso, para la urgencia que tiene un pedido.
 * Repository. Contiene tanto las interfaces correspondientes a Vehículo y Pedido, como la implementación de las mismas. Sus clases se basan en realizar las operaciones necesarias (creación, consulta, borrado...) sobre el contexto de base de datos creado, guardando los cambios necesarios.
 * Controllers. Dentro de ella, se encuentran los controladores correspondientes a cada tabla. En sus clases, se concretan las operaciones GET, POST etc. Desde ellas se llama al repositorio correspondiente, se opera contra base de datos y se devuelve el resultado de la acción (Ok, NotFound etc.).
    
### Pruebas Unitarias    
 El Proyecto DeliveryUnitTest que se encuentra en esta misma solución permite la ejecución de pruebas unitarias para las diferentes acciones y resultados que pueden llevarse a cabo en la aplicación.
 

### Base de datos: SQL Server    
Se ha utilizado una base de datos de SQL Server local para el almacenamiento de datos de la API. Podemos verla dentro del proyecto como "(LocalDb)\\MSSQLLocalDB". 
Dentro del proyecto, se han establecido los modelos de datos que componen las tablas correspondientes a la base de datos denominada "Delivery" (podemos verlos en la carpeta "Models"). Se han creado también DTOs específicos para la selección, creación o actualización de los diferentes registros.
Además, en la carpeta "Data" del proyecto "DeliveryAPI" podemos ver tanto el context creado, como las migraciones realizadas para actualizar la base de datos.

### Control de versiones
Para el control de versiones del proyecto se ha utilizado constantemente GIT.

### Documentación del API (Swagger)
Como estamos utilizando Swagger para nuestra API, es posible consultar el índice de la API accediendo la URL https://localhost:<PUERTO_CONFIGURADO>/swagger/index.html

Desde este índice, Swagger nos permite también realizar llamadas reales (GET, POST etc.).

Si se quiere revisar el JSON correspondiente, se podrá acceder a la URL https://localhost:<PUERTO_CONFIGURADO>/swagger/v1/swagger.json.

Para las pruebas locales, el puerto configurado utilizado como ejemplo ha sido el 44341.


## Ejemplo de llamada a la API
Como hemos visto al inicio, se pueden realizar numerosas acciones con la aplicación. En este apartado dejamos algunos ejemplos de llamadas correctas teniendo en cuenta que nuestro puerto es el 44341.

* GET para obtener todos los pedios existentes.

`curl -X 'GET' \  'https://localhost:44341/api/Pedido' \  -H 'accept: application/json'

Esta llamada nos devolverá un código de estado 200 y la lista completa de súper heroes.

* GET para optener el Vehículo con Id = 1.

`curl -X 'GET' \  'https://localhost:44341/api/Vehiculo/1' \  -H 'accept: application/json'`

* POST para crear un nuevo Vehículo.

`curl -X 'POST' \
  'https://localhost:44341/api/Vehiculo' \  -H 'accept: application/json' \  -H 'Content-Type: application/json' \
  -d '{  "direccion": "Direccion de Ejemplo",  "conductor": "Conductor de Ejemplo",  "latitud": 1.63574,  "longitud": -1.29465}'`

Estos son solo algunos ejemplos de llamadas a nuestra API, pero también podrán realizarse llamadas de forma más sencilla gracias a Swagger.


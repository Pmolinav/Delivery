# BACKEND CHALLENGE - R. FRANCO DIGITAL

## Descripción    
Esta solución contiene dos proyectos: una API para una empresa de paquetería (DeliveryAPI) y el proyecto de pruebas xUnit asociado al mismo.

## Resumen
Al ejecutar esta solución podremos contemplar los dos proyectos existentes.
En un breve resumen podemos decir que el proyecto "DeliveryAPI" constituye la API Backend que será utilizada por la empresa de paquetería para llevar a cabo las diversas acciones necesarias con los vehículos y los pedidos existentes. Además, el proyecto "DeliveryUnitTest" contiene las pruebas unitarias que se ejecutan para el correcto funcionamiento de la aplicación. Además, podemos encontrar los ficheros correspondientes al "docker-compose", el cual se levantará para la creación de la base de datos de SQL Server que se va a utilizar en la API.


## Configuración inicial
Para poder abrir la solución, puede utilizarse la versión más reciente posible de Visual Studio.
* Lo que haremos en primer lugar será levantar el docker-compose. Para ello, abriremos la consola cmd y viajaremos a la ruta en la que se encuentre nuestra solución mediante la instrucción "cd". Por ejemplo: cd D:\Visual Studio Projects\repos\Delivery
* A continuación, abriremos nuestra solución con Visual Studio y, en la parte superior, viajamos a Herramientas / Administrador de paquetes NuGet / Consola del Administrador de paquetes.
* En la parte inferior de la pantalla se nos abrirá una consola. En ella debeos escribir update-database y pulsar intro.
* Una vez hayamos realizado estos pasos, la base de datos creada mediante docker-compose en local, se actualizará correctamente y podremos probar la aplicación.
* Si queremos tener algunos datos de prueba, se ha dejado un script llamado "Script_Ejemplo_Pruebas.sql" en esta misma ubicación. Este script puede ejecutarse directamente en la base de datos para tener algunos datos de ejemplo.
* Con esto, ya podemos ejecutar nuestra API en la parte superior de Visual Studio, con un IIS Express por ejemplo.
* Finalmente, si queremos ejecutar las pruebas unitarias, podemos hacerlo en la parte superior de Visual Studio, viajamos a Prueba / Explorador de pruebas y pulsamos en "Ejecutar todas las pruebas". De este modo, todas deberían ser satisfactorias si el funcionamiento de la aplicacion es el correcto.

## Desarrollo    
La solución se ha desarrollado con Visual Studio 2022 y .Net Core 6. Se han instalado paquetes como EntityFrameworkCore, AutoMapper, AspNetCore.Mvc, xUnit y Swashbuckle.

Las especificaciones y descripciones de cada proyecto pueden verse dentro de sus ficheros "README.md" particulares.

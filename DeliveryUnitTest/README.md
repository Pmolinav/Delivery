# PRUEBAS UNITARIAS DE LA API PARA LA EMPRESA DE PAQUETERÍA

## Descripción    
Este proyecto es complementario a la API "DeliveryAPI" y se utiliza para la ejecución de pruebas unitarias.

### Pruebas realizadas
En este proyecto se han realizado 30 pruebas unitarias diferentes para tener en cuenta los diferentes escenarios que se corresponden con las acciones disponibles dentro de la API (las cuales pueden observarse en el fichero "README.md" que se encuentra en el proyecto "DeliveryAPI").
Por tanto, las pruebas unitarias tendrán como objetivo comprobar las llamadas GET, POST, PATCH y DELETE realizadas tanto para los vehículos, como para los pedidos de la empresa de paquetería.


## Compilación y ejecución de las pruebas   
 Este es un poryecto de pruebas xUnit para una API de .NET Core 6, por lo que lo más recomendado es abrir la solución con la versión más actual posible de Visual Studio.
 
 Mediante Visual Studio, será posible compilar la solución y realizar las pruebas unitarias accediendo en la barra superior a "Prueba/Explorador de pruebas/Ejecutar todas las pruebas". 

 Para realizar estas pruebas xUnit, se crea una base de datos en memoria y se rellena su información con datos de prueba.


## Estructura del proyecto  
 Este proyecto posee tres carpetas diferentes para facilitar su comprensión. Estas carpetas son las siguientes:
 * VehiculoUnitTests. Contiene una clase de prueba por cada tipo de operación que se va a probar dentro de los vehículos de la empresa (GET, POST, PATCH y DELETE) y los diferentes casos de prueba que se tiene en ellos.
 * PedidoUnitTests. Contiene una clase de prueba por cada tipo de operación que se va a probar para los pedidos que existen de la empresa(GET, POST, PATCH y DELETE) y los diferentes casos de prueba que se tiene en ellos.
 * Utils. Contiene las clases auxiliares que se han utilizado para la creación de la base de datos en memoria y la inicialización de los datos empleados para las pruebas.


### Control de versiones
Para el control de versiones del proyecto se ha utilizado constantemente GIT.

# Proyecto de Prueba Técnica: Asignación de Saldos a Gestores de Cobros

## Descripción
Este proyecto implementa un algoritmo que asigna saldos de cuentas de clientes a un grupo de gestores de cobros de manera justa. Los saldos son asignados uno por uno a cada gestor, siguiendo un proceso iterativo hasta que todos los saldos han sido distribuidos.

## Requisitos Técnicos
- Lenguaje de Programación: .NET (C#)
- Base de Datos: SQL Server
- Técnicas Utilizadas: T-SQL, Entity Framework Core

## Requerimientos
- Ordenar los saldos en orden descendente.
- Asignar los saldos uno por uno a cada gestor.
- Repetir el proceso hasta que todos los saldos estén asignados.
- El número de iteraciones debe ser igual a la división redondeada hacia arriba del número de saldos por el número de gestores.

## Estructura de la Base de Datos
El proyecto utiliza una base de datos llamada DBTest, que contiene las siguientes tablas:

### Tablas
#### Gestores

- Id (INT, PRIMARY KEY): Identificador único del gestor.
- Nombre (NVARCHAR(50)): Nombre del gestor.

#### Saldos

- Id (INT, PRIMARY KEY, IDENTITY): Identificador único del saldo.
- Monto (DECIMAL(10, 2)): Monto del saldo.

### Procedimiento Almacenado
El procedimiento almacenado **AsignarMontos** distribuye los saldos a los gestores utilizando un cursor y una tabla temporal. Cada saldo se asigna a un gestor de manera cíclica.

## Uso de la API

La API tiene un endpoint que permite obtener las asignaciones de saldos a los gestores:

GET /Assignment
- Descripción: Obtiene todas las asignaciones de saldos a los gestores.
- Respuesta: Retorna un array con las asignaciones, incluyendo el GestorId y el SaldoMonto.

### Models

La clase AsodbContext hereda de DbContext y se encarga de gestionar la conexión a la base de datos. Se define un conjunto de entidades (DbSet<Assignment>) que representan la colección de asignaciones en la base de datos.

- Constructor: Acepta un objeto DbContextOptions<AsodbContext> que permite configurar opciones para el contexto.
- DbSet: Asignaciones es la propiedad que permite interactuar con la tabla de asignaciones en la base de datos, facilitando las operaciones de CRUD (Crear, Leer, Actualizar, Eliminar).
   
La clase Assignment representa una asignación de saldo a un gestor y contiene las siguientes propiedades:

- Id: Clave primaria única para cada asignación (marcada con el atributo [Key]).
- GestorId: Identificador del gestor al que se le asigna un saldo, validado para ser un valor positivo mediante el atributo [Range].
- SaldoMonto: Monto del saldo asignado, validado para ser mayor que cero mediante el atributo [Range].

### Controller

El AssignmentController es responsable de manejar las solicitudes HTTP relacionadas con las asignaciones. Incluye las siguientes características:

**Atributos**
- [ApiController] para habilitar la validación automática de modelos.
- [Route("[controller]")] que establece la ruta base para las solicitudes relacionadas con asignaciones.
- Inyección de Dependencias: El constructor recibe un objeto AsodbContext, que se utiliza para acceder a la base de datos.

**Método GetAsignaciones**

- [HttpGet]: Este método responde a las solicitudes GET en la ruta correspondiente.
- Ejecución de Procedimiento Almacenado: Utiliza FromSqlRaw para ejecutar el procedimiento almacenado AsignarMontos y obtener la lista de asignaciones.
- Manejo de Errores: Implementa un bloque try-catch para gestionar excepciones y devuelve un mensaje de error con código de estado 500 en caso de fallos.


### Middleware
El proyecto incluye un middleware SignOutMiddleware que captura excepciones no manejadas y registra errores en el log, devolviendo una respuesta genérica al cliente en caso de error.

Y en la Web de la Api ejecutamos el unico GET:
![image](https://github.com/user-attachments/assets/7badb4bd-429a-4b42-9c88-8dd7b388ca53)



En el Response body vemos el resultado.
![image](https://github.com/user-attachments/assets/b4cb0c9a-d691-4bd8-919f-b2cf19ac20e1)

## Instalación

1- Clona este repositorio en tu máquina local:
  ```bash
  git clone https://github.com/cabonillamo/testAPICarlo_Bonilla.git
   ```
2- Asegúrate de tener instalado .NET SDK y SQL Server.

3- Configura la cadena de conexión en el archivo appsettings.json:
  ```bash
"ConnectionStrings": {
  "DefaultConnection": "Server=TU_SERVER;Database=DBTest;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true"
},
   ```

## Conclusiones
Este proyecto demuestra la capacidad de implementar un algoritmo de asignación utilizando .NET y T-SQL, mostrando un manejo efectivo de bases de datos y desarrollo de API.

## Author
By [Carlo Bonilla](https://github.com/cabonillamo).


# Service.Cliente
Un ejemplo de un servicio desarrollado en .Net se describe a continuacion las caracteristicas

Se implemento los siguientes patrones (UnitOfWork - CQRS)
Se emplea ademas el uso de MediatR. 
Para el manejo, mantenimiento y acceso a la base de datos, se emplea OMR ( EntityFramework )
Ademas se aplica Inyeccion de Dependencias (DI)
Tambien, se utiliza (FluentValidation), a fin de aplicar validaciones basicas a el INPUT de los request,utilizado en el controller ( ClienteCommandController )

El diseño empleado consta de las siguientes capas que se mencionan a continuación:

Capa Principal Servicio - (Presenter):

	- La misma contiene distribuidos los controladores, divididos o segmentados en 2, según patrón CQRS ( Command y Querys ).
	  
	  - ClienteCommandController
	  - ClienteQuerysController
	  
	- Tambien se encuentra alojado las clases, propias de configuraciones de ( App y Services ). La idea de realizarlo asi, fue de mantener segmentada, 
	  para mas entendimiento, las configuraciones requeridas, para esta Api Services. 
	  
	  - Extensiones :
		- IApplicationBuildExtension ( clase Statica )
		- IServiceCollectionExtensions ( clase Statica )
		- IInjectionsExtensions ( clase Statica )

Capa de aplicación:

Se encuentra toda la estructura que se podrá utilizar, dentro de las otras capas, si así fuese necesario.
Dentro de esta capa, están alojados dentro de directorios, para un mayor ordenamiemto y disponibles lo siguiente:

		- Configurations
		- DTO
		- Enums
		- Genérics
		- Helpers
		- Mappers (Se emplea Automapper)
		- Messages ( Se emplea archivos de recursos, para el contenido de los mensajes de validaciones y advertencias, que se puedan producir durante la ejecución del flujo )
		- Request ( Clase empelada, para los datos y estructura, que será empleada en el input de los endpoint )
		- Responses ( Clases creadas de tipo Genericas, para estandarizar, lo ma optimo posible, la salida a respuestas de las solicitudes de cada endponit )
		- UserHistorys (Contiene las acciones y operaciones que se implementan en el uso del patrón CQRS (Command Query Responsability Segregation)
			- Commands:
				- CreateCliente
					- CreateClienteHandler
					- CreateClienteValidator (FluentValidation)
					- CreateValidationsClienteHandler
				- DeleteCliente
					- DeleteClienteHandler
				- UpdateCliente
					- UpdateClienteHandler
					- UpdateClienteValidator (FluentValidation)
					- UpdateValidationsClienteHandler
			- Commons
				- LoggerHandler
			- Querys
				- GetAllClienteHandler
				- GetByEdadClienteHandler
				- GetByIdClienteHandler
				- GetByNameClienteHandler

Capa Domain: 
	
	- Se encuentra la entidad ( Entitys en este caso, para el cliente ) 
	- Las Interfaces vinculadas a los patrones mencionados 
		- IUnitOfWork ( Su interfaz ) 
		- IClienteCommandRepository ( Su interfaz repositoria ) 
		- IClienteQuersRepository ( Su interfaz repositoria )

Capa Infraestructura: 
	
	- Dentro de esta capa, se encuentra toda la estructura, que se empleará para la base de datos SQLServer. 
		- ClienteBuilder : ( Para la creación de la configuración de la tabla, que se creará en la DB ), con sus propiedades 
		- Context : ( El configurador de la conectividad a la base de datos, que se este utilizando, asignandole la ruta del ConectionString, como así también su correspondiente DBSet, para la entidad Cliente) 
		- Migrations : ( Los archivos, de migraciones, que se generan automáticamente cada vez que se ejecuta por consola PowerShell, los comandos de Add-Migration, Update-Database (Nombre Archivo Migracion) 
		- Repository: Las clases de servicios de repositorios, en donde se implementan las (Interfaces, creadas en la capa DOMAIN) 
		- UnitOfWork: La interfaz, donde se construye la configuración de las interfaces, menciona (IClienteCommandRepository u IClienteQuerysRepository)
		
Capa Testing:
 	- Resta el desarrollo de la capa agregada, para realiazar los UniTest.

  Nota: Como una simple aclaracion, es tan solo un simple ejemplo, de una forma de muchas existentes, para realizar este desarrollo. 
		Es tan solo a modo de ejemplo

  Saludos.! Gracias

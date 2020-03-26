# Users and Groups management (demo)
Next technologies and patterns where used
* .NET Core
* Angular 8 
* EF Core (for writing)
* Dapper (for reading)
* MediatR/CQRS/DDD
* Docker
This project uses Code First approach and generate DB and seed it with data on the first launch. However you can find snapshots of DB at DB folder.

### How to run
It's highly recommnded to use docker to run the app. See Docker section. Any way you can build it by your own using VS 2019.
Download or clone project to your local folder. Open and build solution ET.sln. VS should download all dependencies for backend and client side. 
However, in case any issues with frontend side please go to ET.Web\ClientApp\ and run "npm install", after run "ng build" to be sure that Angular compiles properly.

### Docker
In order to run application inside docker container follow this
1. docker-compose build
2. docker-compose up
3. open http://127.0.0.1:5002

### MSSQL 
 Open ET.Web\appsettings.json and edit "ConnectionString": "Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=IdentityDB;Integrated Security=SSPI;",
 Current DB, is not optimised, no indexes were used. However User.Name can be indexed for proper performance. 



### Unit Tests
Current solution does not contain Unit Tests or Integration Tests, however code is ready to be covered due to proper design, DI and CQRS patterns. 

### DomainEvents
You can find Domain Events for certain types of operaitons, but current implementation does not have Event Storage/ or connection to AMQP.
That is reserved for future versions. 

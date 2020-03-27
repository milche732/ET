# Users and Groups management (demo)
Next technologies and patterns where used
* .NET Core
* Angular 8 
* EF Core (for writing)
* Dapper (for reading)
* MediatR/CQRS/DDD
* Docker

 This project uses Code First approach and generates DB with data on the first launch. However you can find snapshots of DB at DB folder.

### How to run
It's highly recommnded to use docker to run the app. See Docker section. Any way you can build it by your own using VS 2019.
Download or clone project to your local folder. Open and build solution ET.sln. VS should download all dependencies for backend and client side. 
However, in case any issues with frontend side please go to ET.Web\ClientApp\ and run "npm install", after run "ng build" to be sure that Angular compiles properly.

### Docker
This process builds and runs app inside docker container, you need to have only docker on your local PC, everythin rest will be downloaded inside the docker.
In order to run application inside docker follow this
1. clone project and navigate to the root
2. run "docker-compose build"
3. run "docker-compose up"
4. open http://127.0.0.1:5002

### MSSQL 
 Open ET.Web\appsettings.json and edit "ConnectionString": "Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=IdentityDB;Integrated Security=SSPI;",
 Current DB, is not optimised, no indexes were used. On;y User.Name was indexed. 



### Unit Tests
Current solution does not contain Unit Tests or Integration Tests, however code is ready to be covered due to proper design, DI and CQRS patterns. 

### DomainEvents
You can find Domain Events for certain types of operaitons, but current implementation does not have Event Storage/ or connection to AMQP.
That is reserved for future versions. 

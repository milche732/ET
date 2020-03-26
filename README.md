## This is a demo project: Users and Groups management.
Next technologies and patterns where used
* .NET Core
* Angular 8 
* EF Core (for writing)
* Dapper (for reading)
* CQRS/DDD

This project uses Code First approach and generate DB and seed it wiht data on the first launch. However you can find snapshots of DB at DB folder.

### How to run
Download or clone project to your local folder. Open and build solution ET.sln. VS should download all dependencies for backend and client side. 
However, in case any issues with frontend side please go to ET.Web\ClientApp\ and run "npm install", after run "ng build" to be sure that Angular compiles properly.

### MSSQL 
 Open ET.Web\appsettings.json and edit "ConnectionString": "Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=IdentityDB;Integrated Security=SSPI;",



### Unit Tests
Current solution does not contain Unit Tests or Integration Tests, however code is ready to be covered due to proper design, DI and CQRS patterns. 

### DomainEvents
You can find Domain Events for certain types of operaitons, but current implementation does not have Event Storage/ or connection to AMQP.
That is reserved for future versions. 

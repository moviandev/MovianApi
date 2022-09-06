# MovianApi
To start the application run 
```cli
cd src/Movian.Api
```
and then
```cli
dotnet watch run
```
after doing so, try opening the swagger URL https://localhost:7023/swagger/index.html

The database is a SQLite and it already is installed in the application there's no need to install or run migrations. 
To test it first register yoursef into the app api/v1/sign-in, and with the generated token authenticate yourself into swagger ui, please notice that this application
was made based on the course of Desenvolvedor.io, i tried to apply the concepts learnt during the course. 

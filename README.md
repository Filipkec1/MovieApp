# MovieApp

### Add Migration
```
dotnet ef migrations add "NameOfYourMigration" --startup-project "src\MovieApp.Web\MovieApp.Web.csproj" --project "src\MovieApp.Infrastructure\MovieApp.Infrastructure.csproj" -v
```

### Update Database
```
dotnet ef database update --startup-project "src\MovieApp.Web\MovieApp.Web.csproj" --project "src\MovieApp.Infrastructure\MovieApp.Infrastructure.csproj" -v
```
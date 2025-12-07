## How to Run

1. Set connection string in `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=EventManagerDb;Trusted_Connection=True;"
}
```

2. Apply migrations:
```bash
dotnet ef database update
```

3. Run the app:
```bash
dotnet run
```

https://localhost:7004/swagger/
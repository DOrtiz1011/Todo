# Todo App

![My Todos](image.png)

## Objective

Build a small to-do task management API and frontend.

### Backend

- Designed with .NET 10
- SQLLite database made with Entity Framework migrations
- Repository layer to communicate with Entity Framework
- Service layer DTOs to keep the controller clean and models contained to the repository layer
- FluentValidation used in the service layer to validate requests
- GLobal error handling with middleware
- Documentation and testing with Swagger
- Unit tests with xUnit

### Frontend

- Designed with React
- Axios for API calls
- Todos are listed in a table
- Separate components for the table and form
- Form is modal

## How to Run the App

### Run the Backend

```powershell
# Run powershell at the root of the repositpry

dotnet restore;            # Get NuGet packages
dotnet build;              # build the backend
cd .\Todo.Api\;            # Change to the Todo.Api directory
dotnet ef database update; # Setup the database
dotnet run;                # Run backend
```

### Run the React Frontend

```powershell
# Run powershell at the root of the repositpry

cd .\Todo.Api\Frontend\todoapp\; # goto the frontend\todoapp directory
npm install;                     # install node moduels
npm run dev;                     # run the frontend
```

The app will launch with test data created.

## Testing

### Run Unit Tests

```powershell
# Run powershell at the root of the repositpry

cd .\Todo.Tests\; # goto the Todo.Tests directory
dotnet test;      # run the unit tests
```

[LocalHost URL for Frontend](http://localhost:5173/)

### Test API

Once the backend is running, you can test the API with [Swagger](https://localhost:7103/swagger/index.html)

## React Commands

Stop and restart the React app

```powershell
taskkill /f /im node.exe; npm install; npm run dev
```

## Database Commands

Add migration

```powershell
dotnet ef migrations add MIGRATION_NAME
```

Apply Migrations

```powershell
dotnet ef database update
```

## Future Enhancements

### Backend

- Add a table, repository, and controller for notes on a task. `TableBase.cs` is uesed to define the columns that all tables require. It is also used in `IRepository.cs` in a generic `where` clause to ensure that all tables inherit from `TableBase.cs` when implementing the interface.
- Implement tempral tables for audit logging. Logging should be by design and not full dependant on the engineer wrting logging code.
- Add the ablity to assign a user to a task. Add a table, repository, and controller for users.

### Frontend

- Sortable columns
- Pagination
- Store to keep track of data and not fully reload table on every action.

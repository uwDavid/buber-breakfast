# Api Workflow

Initiating Project

```
dotnet sln -o BuberBreakfast
cd BuberBreakfast
dotnet new classlib -o BuberBreakfast.Contracts
dotnet new webapi -o BuberBreakfast
```

Add Reference to the Sln file

```
dotnet sln add BuberBreakfast BuberBreakfast.contracts
```

Add reference for Project

```
dotnet add BuberBreakfast reference BuberBreakfast.Contracts
```

## Error Handling

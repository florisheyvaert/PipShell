# PipShell
A shell written for executing Pip commands.

## Installation
Install package through NuGet (https://www.nuget.org/packages/PipShell/).
```
Install-Package PipShell
```

## Usage
### Inject dependencies
Inject the depencies with following code on your IServiceCollection:
```csharp
services.AddPipShell();
```

### Usage
Inject 'IPip' interface in your class, this interfaces has following methods:
```csharp
    public interface IPip
    {
        Task<List<Package>> Get(CancellationToken cancellationToken = default);
        Task<Package> Get(string name, CancellationToken cancellationToken = default);
        Task<Package> Update(string name, CancellationToken cancellationToken = default);
        Task<Package> Uninstall(string name, CancellationToken cancellationToken = default);
        Task<Package> Install(string name, CancellationToken cancellationToken = default);
    }
```

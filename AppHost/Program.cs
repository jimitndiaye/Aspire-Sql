var builder = DistributedApplication.CreateBuilder(args);

var dbServer = builder.AddSqlServer("sql")
    .WithDataVolume()
    .WithLifetime(ContainerLifetime.Persistent);
var db = dbServer.AddDatabase("db");

builder.AddProject<Projects.Api>("api")
    .WithExternalHttpEndpoints()
    .WithReference(db)
    .WaitFor(db)
    .WithOtlpExporter();
builder.Build().Run();

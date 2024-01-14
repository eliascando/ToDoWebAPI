using ToDoWebAPI.Config;
using Infraestructure.Persistence.Context;
using Infraestructure.Security.JWT;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add JwtBearer to the container.
builder.Services.AddJwtAuthentication(builder.Configuration);

// Add services to the container.
builder.Services.AddDependencyInjection(builder.Configuration);

var app = builder.Build();


// Actions to perform on the database
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<DBContext>();
    var migrate = builder.Configuration["Database:Migrate"] == "true" ? true : false;
    var drop = builder.Configuration["Database:Drop"] == "true" ? true : false;
    
    if (drop)
    {
        Debug.WriteLine("Dropping database...");
        context.Database.EnsureDeleted();
    }// Drop database
    if (migrate)
    {
        Debug.WriteLine("Migrating database...");
        context.Database.Migrate();
    }// Migrate database
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    Debug.WriteLine("Development environment");
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    Debug.WriteLine("Production environment");
}

app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers().RequireAuthorization();

app.Run();

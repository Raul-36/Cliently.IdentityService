using System.Reflection.Metadata;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(AssemblyReference).Assembly));

// Add AutoMapper
builder.Services.AddAutoMapper(typeof(AssemblyReference).Assembly);

// // Configure PostgreSQL DbContext
// builder.Services.AddDbContext<IdentityEFPostgreContext>(options =>
//     options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// using var scope = builder.Services.BuildServiceProvider().CreateScope();
// {
//     var dbContext = scope.ServiceProvider.GetRequiredService<IdentityEFPostgreContext>();
//     if (dbContext.Database.GetPendingMigrations().Any())
//     {
        
//         dbContext.Database.Migrate();
//     }
// }
// // Register Repositories

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
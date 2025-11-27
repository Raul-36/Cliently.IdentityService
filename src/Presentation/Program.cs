using System.Text;
using Application;
using Application.Identity.Services.Base;
using Application.Roles.Services.Base;
using Application.Tokens.Services.Base;
using Application.UserRoles.Services.Base;
using Application.Users.Services.Base;
using Infrastructure.Data;
using Infrastructure.Identity.Services;
using Infrastructure.Roles.Entities;
using Infrastructure.Roles.Services;
using Infrastructure.Tokens.Options;
using Infrastructure.Tokens.Services;
using Cliently.IdentityService.Infrastructure.Messaging.Options; 
using Infrastructure.UserRoles.Services;
using Infrastructure.Users.Entities;
using Infrastructure.Users.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Presentation.Data;
using Presentation.Extensions;
using Presentation.Options;
using Cliently.IdentityService.Infrastructure.Messaging.Services.Base;
using Cliently.IdentityService.Infrastructure.Messaging.Services;
using Infrastructure.Users.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.InitSwagger();
builder.Services.InitAuth(builder.Configuration);   


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();


builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(AssemblyReference).Assembly));

builder.Services.AddAutoMapper(
    typeof(AssemblyReference).Assembly,
    typeof(Infrastructure.Users.Mappings.UsersMappingProfile).Assembly
);

builder.Services.Configure<FirstUsersOptions>(builder.Configuration.GetSection("FirstUsers"));
builder.Services.Configure<RolesOptions>(builder.Configuration.GetSection("Roles"));
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("JwtSettings"));
builder.Services.Configure<RabbitMQOptions>(builder.Configuration.GetSection("RabbitMQ"));
builder.Services.Configure<UserQueuesOptions>(builder.Configuration.GetSection("UserQueues"));

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);
builder.Services.AddIdentityCore<ApplicationUser>()
    .AddRoles<ApplicationRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ISignInService, SignInService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IUserRoleService, UserRoleService>();
builder.Services.AddScoped<ITokenGeneratorService, TokenGeneratorService>();
builder.Services.AddScoped<IProducer, RabbitMqProducer>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

using var scope = app.Services.CreateScope();
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    if (dbContext.Database.GetPendingMigrations().Any())
    {

        dbContext.Database.Migrate();
    }
}
using (var seedScope = app.Services.CreateScope())
{
    var serviceProvider = seedScope.ServiceProvider;
    var roleService = serviceProvider.GetRequiredService<IRoleService>();
    await RoleSeeder.SeedRoles(serviceProvider);

    var userService = serviceProvider.GetRequiredService<IUserService>();
    await FirstUserSeeder.SeedUsers(serviceProvider);

}
app.Run();
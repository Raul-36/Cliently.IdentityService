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
using Infrastructure.UserRoles.Services;
using Infrastructure.Users.Entities;
using Infrastructure.Users.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Presentation.Data;
using Presentation.Extensions;
using Presentation.Options;

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

// Configure Options
builder.Services.Configure<FirstUsersOptions>(builder.Configuration.GetSection("FirstUsers"));
builder.Services.Configure<RolesOptions>(builder.Configuration.GetSection("Roles"));
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("JwtSettings"));

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentityCore<ApplicationUser>()
    .AddRoles<ApplicationRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

using var scope = builder.Services.BuildServiceProvider().CreateScope();
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    if (dbContext.Database.GetPendingMigrations().Any())
    {

        dbContext.Database.Migrate();
    }
}
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ISignInService, SignInService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IUserRoleService, UserRoleService>();
builder.Services.AddScoped<ITokenGeneratorService, TokenGeneratorService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

using (var seedScope = app.Services.CreateScope())
{
    var serviceProvider = seedScope.ServiceProvider;

    var userService = serviceProvider.GetRequiredService<IUserService>();
    if ((await userService.GetAllUsersAsync()).Value!.Count() == 0)
        await FirstUserSeeder.SeedUsers(serviceProvider);

    var roleService = serviceProvider.GetRequiredService<IRoleService>();
    if ((await roleService.GetRoleByNameAsync("Admin")) is null)
        await RoleSeeder.SeedRoles(serviceProvider);
}
app.Run();
using System.Text;
using HRMS.Application;
using HRMS.Domain.Entities;
using HRMS.Persistence;
using HRMS.Persistence.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using HRMS.API.Settings;
using HRMS.Application.Abstractions;
using HRMS.API.Services;
using Microsoft.OpenApi.Models;
using HRMS.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPersistenceService();
builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<HRMSDbContext>()
    .AddDefaultTokenProviders();


builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(HRMS.Application.AssemblyReference).Assembly);
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "HRMS API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT de�erini girin. (�rn: sadece token; Swagger otomatik 'Bearer ' ekler)"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme{
                Reference = new OpenApiReference{ Type = ReferenceType.SecurityScheme, Id = "Bearer" }
            },
            Array.Empty<string>()
        }
    });
});
//HRManager i�in konfig�rasyon
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new()
        {
            //Gelen token'daki do�rulanmas� gerekenler
            ValidateAudience = true, //Olu�turulacak token de�erini kimlerin/hangi sitelerin kullanabilece�ini kontrol eder. -> www.example.com
            ValidateIssuer = true, //Olu�turulacak token de�erini kimin da��tt���n� kontrol eder. -> www.myapi.com
            ValidateLifetime = true, //Olu�turulan token de�erinin ge�erlilik s�resini kontrol eder.
            ValidateIssuerSigningKey = true, //�retilecek token de�erinin uygulamam�za ait bir de�er oldu�unu ifade eden security key'in do�rulu�unu kontrol eder.

            ValidAudience = builder.Configuration["Token:Audience"], //Token de�erini kullanacak olan sitenin adresi
            ValidIssuer = builder.Configuration["Token:Issuer"], //Token de�erini �reten uygulaman�n adresi
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),
            ClockSkew = TimeSpan.Zero, //Token'�n ge�erlilik s�resinin bitiminden sonra 5 dakikal�k bir tolerans s�resi eklenir. Bunu s�f�rl�yoruz.
        };
    });
builder.Services.AddHttpContextAccessor();

static async Task SeedRolesAsync(IServiceProvider serviceProvider)
{
    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    string[] roles = { "User", "Admin", "HRManager" };

    foreach (var role in roles)
        if (!await roleManager.RoleExistsAsync(role))
            await roleManager.CreateAsync(new IdentityRole(role));
}

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await SeedRolesAsync(services);
}

app.Run();

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
        Description = "JWT deðerini girin. (Örn: sadece token; Swagger otomatik 'Bearer ' ekler)"
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
//HRManager için konfigürasyon
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
            //Gelen token'daki doðrulanmasý gerekenler
            ValidateAudience = true, //Oluþturulacak token deðerini kimlerin/hangi sitelerin kullanabileceðini kontrol eder. -> www.example.com
            ValidateIssuer = true, //Oluþturulacak token deðerini kimin daðýttýðýný kontrol eder. -> www.myapi.com
            ValidateLifetime = true, //Oluþturulan token deðerinin geçerlilik süresini kontrol eder.
            ValidateIssuerSigningKey = true, //Üretilecek token deðerinin uygulamamýza ait bir deðer olduðunu ifade eden security key'in doðruluðunu kontrol eder.

            ValidAudience = builder.Configuration["Token:Audience"], //Token deðerini kullanacak olan sitenin adresi
            ValidIssuer = builder.Configuration["Token:Issuer"], //Token deðerini üreten uygulamanýn adresi
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),
            ClockSkew = TimeSpan.Zero, //Token'ýn geçerlilik süresinin bitiminden sonra 5 dakikalýk bir tolerans süresi eklenir. Bunu sýfýrlýyoruz.
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

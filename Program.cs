using System.Security.Cryptography.Xml;
using System.Text;
using System.Text.Json.Serialization;
using ApiChikPet.DTOs.Mappings;
using ApiChikPet.Loggin;
using ApiChikPet.Context;
using ApiChikPet.Filters;
using ApiChikPet.Models;
using ApiChikPet.Observer;
using ApiChikPet.Service;
using ApiChikPet.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NuGet.Common;
using NuGet.Protocol;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("Admin",
        policy =>
        {
            policy.SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost")
                                .AllowAnyMethod()
                                .AllowAnyHeader()
                                .WithExposedHeaders("X-Pagination");
        });

    options.AddPolicy("Client",
        policy =>
        {
            policy.WithOrigins("http://www.contoso.com")
                                .AllowAnyHeader()
                                .AllowAnyMethod();
        });
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ApiChikPet", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = @"JWT Authorization header using the Bearer scheme.
                    Enter 'Bearer'[space].Example: \'Bearer 12345abcdef\'",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                          {
                              Reference = new OpenApiReference
                              {
                                  Type = ReferenceType.SecurityScheme,
                                  Id = "Bearer"
                              }
                          },
                         new string[] {}
                    }
                });
});
builder.Services.AddControllers().AddJsonOptions(options =>
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles
);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();




string mySqlConnection = builder.Configuration.GetConnectionString("DefaultConnection");

var mappingConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MappingProfile());
});

IMapper mapper = mappingConfig.CreateMapper();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(mySqlConnection,
    ServerVersion.AutoDetect(mySqlConnection)));

var environment = builder.Environment;
var config = builder.Configuration;

builder.Services.AddSingleton<ITokenService>(new TokenService());
builder.Services.AddSingleton<ISaveFile>(new SaveFile(environment));
builder.Services.AddSingleton<IEmailSender>(new EmailSender(config));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = builder.Configuration["TokenConfigurantion:Issuer"],
        ValidAudience = builder.Configuration["TokenConfigurantion:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});


builder.Services.AddAuthorization();

builder.Services.AddScoped<ApiLogginFilter>();

builder.Services.AddIdentity<UserModel, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders()
    .AddErrorDescriber<IdentityPortugueseMessages>();

builder.Logging.AddProvider(new CustomLoggerProvider(new CustomLoggerProviderConfiguration
{
    LogLevel = Microsoft.Extensions.Logging.LogLevel.Information
}));

builder.Services.Configure<IdentityOptions>(options =>
{
    options.User.RequireUniqueEmail = true;
    options.SignIn.RequireConfirmedEmail = true;
});

builder.Services.AddSingleton(mapper);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();


app.Run();
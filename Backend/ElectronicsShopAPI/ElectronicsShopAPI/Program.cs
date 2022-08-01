using ElectronicsShopAPI.Data;
using ElectronicsShopAPI.Data.Configurations;
using ElectronicsShopAPI.IRepository;
using ElectronicsShopAPI.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// System Constants
// KEY for JWT
// Using Environment Variable
/*
var key = Environment.GetEnvironmentVariable("KEY");
if (key is null)
    throw new Exception(message: "KEY is not defined in environment variables");
*/
//Using appsettings.json
var key = builder.Configuration["JwtSettings:Key"];

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Configure extra services
// Serilog
builder.Host.UseSerilog();

// CORS
builder.Services.AddCors(options => {
    options.AddPolicy("AllowAll",
        b => b.AllowAnyHeader()
            .AllowAnyOrigin()
            .AllowAnyMethod());
});

// DatabaseContext
var connectionString = builder.Configuration.GetConnectionString("ElectronicsShopSqlConnectionString");
builder.Services.AddDbContext<ElectronicsShopDbContext>(options => {
    options.UseSqlServer(connectionString);
});

// AutoMapper
builder.Services.AddAutoMapper(typeof(MapperInitializer));

// UnitOfWork
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// IRepository
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IDiscountTypeRepository, DiscountTypeRepository>();
builder.Services.AddScoped<IDiscountRepository, DiscountRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Identity Services
builder.Services.AddIdentityCore<User>()
    .AddRoles<IdentityRole>()
    .AddTokenProvider<DataProtectorTokenProvider<User>>("ElectronicsShopAPI")
    .AddEntityFrameworkStores<ElectronicsShopDbContext>()
    .AddDefaultTokenProviders();

// Authentication
builder.Services.AddAuthentication(options => {
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options => {
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero,
        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
        ValidAudience = builder.Configuration["JwtSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
    };
});

// End of extra non-default services

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Add extra Services to app
app.UseCors("AllowAll");


// End of extra Services

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

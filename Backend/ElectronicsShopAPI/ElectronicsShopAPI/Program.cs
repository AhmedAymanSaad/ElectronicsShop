using ElectronicsShopAPI.Data.Configurations;
using ElectronicsShopAPI.IRepository;
using ElectronicsShopAPI.Repository;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

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
builder.Services.AddDbContext<ElectronicsShopAPI.Data.ElectronicsShopDbContext>(options => {
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

app.UseAuthorization();

app.MapControllers();

app.Run();

using Microsoft.EntityFrameworkCore;
using WebApi7.Datos;
using WebApi7.Mapper;
using WebApi7.Repositorio;
using WebApi7.Repositorio.IRepositorio;
using WebApi7.Services;

var builder = WebApplication.CreateBuilder(args);

// Registrar HttpClient
builder.Services.AddHttpClient();
// Registrar el servicio de API externa
builder.Services.AddScoped<ExternalApiServices>();

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(option => 
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddAutoMapper(typeof(MappingConfig));
builder.Services.AddScoped<IVillaRepositorio, VillaRepositorio>();

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

using Microsoft.EntityFrameworkCore;
using SchoolSystem.Core.Interfaces;
using SchoolSystem.Domain.Entities;
using SchoolSystem.Infrastructure.Data;
using SchoolSystem.Infrastructure.Mappings;
using SchoolSystem.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(op =>
{
    op.UseSqlServer(builder.Configuration.GetConnectionString("CadenaSQL"));
});

builder.Services.AddScoped<IRepository<Asistencia>, AsistenciaRepository>();
builder.Services.AddScoped<IAsistenciaRepository, AsistenciaRepository>();
builder.Services.AddScoped<IRepository<Calificacion>, CalificacionRepository>();
builder.Services.AddScoped<IRepository<Curso>, CursoRepository>();
builder.Services.AddScoped<ICursoRepository, CursoRepository>();
builder.Services.AddScoped<IRepository<EstadoAsistencia>, EstadoAsistenciaRepository>();
builder.Services.AddScoped<IRepository<Estudiante>, EstudianteRepository>();
builder.Services.AddScoped<IRepository<Materia>, MateriaRepository>();
builder.Services.AddScoped<IMateriaRepository, MateriaRepository>();

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

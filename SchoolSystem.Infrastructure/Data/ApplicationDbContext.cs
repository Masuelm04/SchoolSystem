using Microsoft.EntityFrameworkCore;
using SchoolSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSystem.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }

        public DbSet<Asistencia> Asistencias { get; set; }
        public DbSet<Calificacion> Calificaciones { get; set; }
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<CursoMateria> CursosMaterias { get; set; }
        public DbSet<EstadoAsistencia> EstadosAsistencia { get; set; }
        public DbSet<Estudiante> Estudiantes { get; set; }
        public DbSet<Materia> Materias { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
      => optionsBuilder.UseSqlServer("Server=ACERN5-MASUEL\\Masuel; Database=SchoolSystemDB; Integrated Security=true; TrustServerCertificate=True");
    }
}

using Microsoft.EntityFrameworkCore;
using SchoolSystem.Core.DTOs.Asistencia;
using SchoolSystem.Core.Interfaces;
using SchoolSystem.Domain.Entities;
using SchoolSystem.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSystem.Infrastructure.Repositories
{
    public class AsistenciaRepository : RepositoryBase<Asistencia>, IAsistenciaRepository
    {
        private readonly ApplicationDbContext _context;
        public AsistenciaRepository(ApplicationDbContext context) : base(context) 
        { 
            _context = context;
        }

        public async Task<IEnumerable<Asistencia>> ObtenerHistorialPorFechaAsync(DateTime fecha)
        {
            var historial = await _context.Asistencias
                                  .Where(a => a.Fecha.Date == fecha.Date && a.Eliminado == false)
                                  .Include(a => a.Estudiante) 
                                  .Include(a => a.Estado)
                                  .Include(a => a.Estudiante.Curso)
                                  .ToListAsync();

            return historial;
        }
    }
}

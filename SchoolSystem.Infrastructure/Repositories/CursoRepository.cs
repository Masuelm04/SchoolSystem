using Microsoft.EntityFrameworkCore;
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
    public class CursoRepository : RepositoryBase<Curso>, ICursoRepository
    {
        private readonly ApplicationDbContext _context;
        public CursoRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task AsignarMateriaAsync(int idCurso, int idMateria)
        {
            if (!await CursoTieneMateriaAsync(idCurso, idMateria))
            {
                var cursoMateria = new CursoMateria
                {
                    IdCurso = idCurso,
                    IdMateria = idMateria
                };

                _context.CursosMaterias.Add(cursoMateria);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> CursoTieneMateriaAsync(int idCurso, int idMateria)
        {
            return await _context.CursosMaterias
            .AnyAsync(cm => cm.IdCurso == idCurso && cm.IdMateria == idMateria);
        }

        public async Task EliminarAsignacionMateriaAsync(int idCurso, int idMateria)
        {
            var cursoMateria = await _context.CursosMaterias
            .FirstOrDefaultAsync(cm => cm.IdCurso == idCurso && cm.IdMateria == idMateria);

            if (cursoMateria != null)
            {
                _context.CursosMaterias.Remove(cursoMateria);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Curso>> GetCursosConMateriasAsync()
        {
            return await _context.Cursos
                .Include(c => c.CursoMaterias)
                    .ThenInclude(cm => cm.Materia)
                .Where(c => c.Eliminado == false)
                .ToListAsync();
        }

        public async Task<IEnumerable<Materia>> ObtenerMateriasPorCursoAsync(int idCurso)
        {
            var curso = await _context.Cursos
                .Include(c => c.CursoMaterias)
                .ThenInclude(cm => cm.Materia)
                .FirstOrDefaultAsync(c => c.Id == idCurso && !c.Eliminado);

            return curso?.CursoMaterias.Select(cm => cm.Materia).ToList();
        }
    }
}

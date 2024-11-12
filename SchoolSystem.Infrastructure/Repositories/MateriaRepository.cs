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
    public class MateriaRepository : RepositoryBase<Materia>, IMateriaRepository
    {
        private readonly ApplicationDbContext _context;
        public MateriaRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task AsignarCursoAsync(int idMateria, int idCurso)
        {
            if (!await MateriaEstaEnCursoAsync(idMateria, idCurso))
            {
                var cursoMateria = new CursoMateria
                {
                    IdMateria = idMateria,
                    IdCurso = idCurso
                };

                _context.CursosMaterias.Add(cursoMateria);
                await _context.SaveChangesAsync();
            }
        }

        public async Task EliminarAsignacionCursoAsync(int idMateria, int idCurso)
        {
            var cursoMateria = await _context.CursosMaterias
            .FirstOrDefaultAsync(cm => cm.IdMateria == idMateria && cm.IdCurso == idCurso);

            if (cursoMateria != null)
            {
                _context.CursosMaterias.Remove(cursoMateria);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Materia>> GetMateriasConCursosAsync()
        {
            return await _context.Materias
            .Include(m => m.CursoMaterias)
            .ThenInclude(cm => cm.Curso)
            .Where(m => !m.Eliminado)
            .ToListAsync();
        }

        public async Task<bool> MateriaEstaEnCursoAsync(int idMateria, int idCurso)
        {
            return await _context.CursosMaterias
            .AnyAsync(cm => cm.IdMateria == idMateria && cm.IdCurso == idCurso);
        }

        public async Task<IEnumerable<Curso>> ObtenerCursosPorMateriaAsync(int idMateria)
        {
            var materia = await _context.Materias
            .Include(m => m.CursoMaterias)
            .ThenInclude(cm => cm.Curso)
            .FirstOrDefaultAsync(m => m.Id == idMateria && !m.Eliminado);

            return materia?.CursoMaterias.Select(cm => cm.Curso).ToList();
        }
    }
}

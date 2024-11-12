using SchoolSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSystem.Core.Interfaces
{
    public interface ICursoRepository
    {
        Task<IEnumerable<Materia>> ObtenerMateriasPorCursoAsync(int idCurso);
        Task<IEnumerable<Curso>> GetCursosConMateriasAsync();
        Task<bool> CursoTieneMateriaAsync(int idCurso, int idMateria);
        Task AsignarMateriaAsync(int idCurso, int idMateria);
        Task EliminarAsignacionMateriaAsync(int idCurso, int idMateria);
    }
}

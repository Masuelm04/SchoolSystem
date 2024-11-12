using SchoolSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSystem.Core.Interfaces
{
    public interface IMateriaRepository
    {
        Task<IEnumerable<Curso>> ObtenerCursosPorMateriaAsync(int idMateria);
        Task<IEnumerable<Materia>> GetMateriasConCursosAsync();
        Task<bool> MateriaEstaEnCursoAsync(int idMateria, int idCurso);
        Task AsignarCursoAsync(int idMateria, int idCurso);
        Task EliminarAsignacionCursoAsync(int idMateria, int idCurso);
    }
}

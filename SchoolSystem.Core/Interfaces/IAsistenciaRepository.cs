using SchoolSystem.Core.DTOs.Asistencia;
using SchoolSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSystem.Core.Interfaces
{
    public interface IAsistenciaRepository
    {
        Task<IEnumerable<Asistencia>> ObtenerHistorialPorFechaAsync(DateTime fecha);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSystem.Core.DTOs.Asistencia
{
    public class ModAsistenciaDTO
    {
        public DateTime Fecha { get; set; } = DateTime.Now;
        public int IdEstudiante { get; set; }
        public int IdEstadoAsistencia { get; set; }
    }
}

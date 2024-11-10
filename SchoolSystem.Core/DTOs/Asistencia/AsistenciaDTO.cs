using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSystem.Core.DTOs.Asistencia
{
    public class AsistenciaDTO
    {
        public int Id { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}")]
        public DateTime Fecha { get; set; } = DateTime.Now;
        public int IdEstudiante { get; set; }
        public string NombreEstudiante { get; set; }
        public int IdEstadoAsistencia { get; set; }
        public string NombreEstadoAsistencia { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}")]
        public DateTime FechaRegistro { get; set; }
    }
}

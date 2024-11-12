using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSystem.Core.DTOs.Estudiante
{
    public class EstudianteAsistenciaDTO
    {
        public int Id { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}")]
        public DateTime Fecha { get; set; }
        public int IdEstadoAsistencia { get; set; }
        public string NombreEstadoAsistencia { get; set; }
    }
}

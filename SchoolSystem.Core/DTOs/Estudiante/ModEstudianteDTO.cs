using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSystem.Core.DTOs.Estudiante
{
    public class ModEstudianteDTO
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime FechaNacimiento { get; set; }
        public string Direccion { get; set; }
        public int IdCurso { get; set; }
    }
}

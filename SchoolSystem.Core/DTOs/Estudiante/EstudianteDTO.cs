using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSystem.Core.DTOs.Estudiante
{
    public class EstudianteDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El nombre del estudiante es obligatorio")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El apellido es obligatorio")]
        public string Apellido { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime FechaNacimiento { get; set; }
        [Required(ErrorMessage = "La direccion del estudiante es obligatoria")]
        public string Direccion { get; set; }
        public int IdCurso { get; set; }
        public string NombreCurso { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}")]
        public DateTime FechaRegistro { get; set; }
    }
}

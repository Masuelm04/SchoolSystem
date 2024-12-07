using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSystem.Core.DTOs.Calificacion
{
    public class CalificacionDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "La nota es obligatoria")]
        public int Nota { get; set; } 
        public string Literal
        {
            get
            {
                if (Nota >= 90) return "A";
                if (Nota >= 80) return "B";
                if (Nota >= 70) return "C";
                return "F";
            }
        }
        public int IdEstudiante { get; set; }
        public string NombreEstudiante { get; set; }
        public int IdMateria { get; set; }
        public string NombreMateria { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}")]
        public DateTime FechaRegistro { get; set; }
    }
}

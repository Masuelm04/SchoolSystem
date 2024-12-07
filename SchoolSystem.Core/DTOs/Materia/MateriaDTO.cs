using SchoolSystem.Core.DTOs.Curso;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSystem.Core.DTOs.Materia
{
    public class MateriaDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El nombre de la materia es obligatorio")]
        [StringLength(30)]
        public string Nombre { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}")]
        public DateTime FechaRegistro { get; set; }
    }
}

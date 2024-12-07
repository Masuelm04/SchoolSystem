using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSystem.Core.DTOs.EstadoAsistencia
{
    public class EstadoAsistenciaDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El nombre del estado de asistencia es obligatorio")]
        [StringLength(30)]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "La descripcion del estado de asistencia es obligatorio")]
        [StringLength(100)]
        public string Descripcion { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}")]
        public DateTime FechaRegistro { get; set; }
    }
}

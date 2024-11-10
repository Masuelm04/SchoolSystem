using SchoolSystem.Domain.ClaseBase;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSystem.Domain.Entities
{
    public class Asistencia : BaseEntity
    {
        public DateTime Fecha { get; set; }
        public int IdEstudiante { get; set; }
        [ForeignKey("IdEstudiante")]
        public Estudiante? Estudiante { get; set; }
        public int IdEstadoAsistencia { get; set; }
        [ForeignKey("IdEstadoAsistencia")]
        public EstadoAsistencia? Estado { get; set; }
    }
}

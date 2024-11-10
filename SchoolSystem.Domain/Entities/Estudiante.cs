using SchoolSystem.Domain.ClaseBase;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSystem.Domain.Entities
{
    public class Estudiante : BaseEntity
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Direccion { get; set; }
        public int IdCurso { get; set; }
        [ForeignKey("IdCurso")]
        public Curso? Curso { get; set; }
        [InverseProperty("Estudiante")]
        public List<Calificacion> Calificaciones { get; set; } = new List<Calificacion>();
        [InverseProperty("Estudiante")]
        public List<Asistencia> Asistencias { get; set; } = new List<Asistencia>();
    }
}

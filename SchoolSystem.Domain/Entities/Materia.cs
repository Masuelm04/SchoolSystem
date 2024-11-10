using SchoolSystem.Domain.ClaseBase;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSystem.Domain.Entities
{
    public class Materia : BaseEntity
    {
        public string Nombre { get; set; }
        [InverseProperty("Materia")]
        public List<Calificacion> Calificaciones { get; set; } = new List<Calificacion>();
        [InverseProperty("Materia")]
        public List<CursoMateria> CursoMaterias { get; set; } = new List<CursoMateria>();
    }
}

using SchoolSystem.Domain.ClaseBase;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSystem.Domain.Entities
{
    public class Curso : BaseEntity
    {
        public string Nombre { get; set; }
        [InverseProperty("Curso")]
        public List<Estudiante> Estudiantes { get; set; } = new List<Estudiante>();
        [InverseProperty("Curso")]
        public List<CursoMateria> CursoMaterias { get; set; } = new List<CursoMateria>();
    }
}

using SchoolSystem.Domain.ClaseBase;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSystem.Domain.Entities
{
    public class Calificacion : BaseEntity
    {
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
        [ForeignKey("IdEstudiante")]
        public Estudiante? Estudiante { get; set; }
        public int IdMateria { get; set; }
        [ForeignKey("IdMateria")]
        public Materia? Materia { get; set; }
    }
}

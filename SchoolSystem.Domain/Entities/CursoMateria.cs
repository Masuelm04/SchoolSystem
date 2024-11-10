using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSystem.Domain.Entities
{
    public class CursoMateria
    {
        [Key]
        public int Id { get; set; }
        public int IdCurso { get; set; }
        [ForeignKey("IdCurso")]
        public Curso? Curso { get; set; }

        public int IdMateria { get; set; }
        [ForeignKey("IdMateria")]
        public Materia? Materia { get; set; }
    }
}

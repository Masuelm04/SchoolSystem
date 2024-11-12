using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSystem.Core.DTOs.Estudiante
{
    public class EstudianteCalificacionDTO
    {
        public int Id { get; set; }
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
        public int IdMateria { get; set; }
        public string NombreMateria { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSystem.Core.DTOs.Curso
{
    public class ModCursoDTO
    {
        [StringLength(30)]
        public string Nombre { get; set; }
    }
}

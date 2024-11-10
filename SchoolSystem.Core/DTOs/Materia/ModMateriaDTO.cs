using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSystem.Core.DTOs.Materia
{
    public class ModMateriaDTO
    {
        [StringLength(30)]
        public string Nombre { get; set; }
    }
}

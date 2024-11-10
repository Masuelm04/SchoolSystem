using SchoolSystem.Domain.ClaseBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSystem.Domain.Entities
{
    public class EstadoAsistencia : BaseEntity
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
    }
}

using SchoolSystem.Domain.Entities;
using SchoolSystem.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSystem.Infrastructure.Repositories
{
    public class EstadoAsistenciaRepository : RepositoryBase<EstadoAsistencia>
    {
        private readonly ApplicationDbContext _context;
        public EstadoAsistenciaRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}

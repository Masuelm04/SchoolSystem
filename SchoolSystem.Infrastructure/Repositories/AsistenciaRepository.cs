using SchoolSystem.Domain.Entities;
using SchoolSystem.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSystem.Infrastructure.Repositories
{
    public class AsistenciaRepository : RepositoryBase<Asistencia>
    {
        private readonly ApplicationDbContext _context;
        public AsistenciaRepository(ApplicationDbContext context) : base(context) 
        { 
            _context = context;
        }
    }
}

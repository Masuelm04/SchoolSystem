using SchoolSystem.Domain.Entities;
using SchoolSystem.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSystem.Infrastructure.Repositories
{
    public class MateriaRepository : RepositoryBase<Materia>
    {
        private readonly ApplicationDbContext _context;
        public MateriaRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}

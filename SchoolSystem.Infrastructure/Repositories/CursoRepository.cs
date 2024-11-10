﻿using SchoolSystem.Domain.Entities;
using SchoolSystem.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSystem.Infrastructure.Repositories
{
    public class CursoRepository : RepositoryBase<Curso>
    {
        private readonly ApplicationDbContext _context;
        public CursoRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}

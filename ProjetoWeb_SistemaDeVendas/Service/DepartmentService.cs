using Microsoft.EntityFrameworkCore;
using ProjetoWeb_SistemaDeVendas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoWeb_SistemaDeVendas.Service
{
    public class DepartmentService
    {
        private readonly ProjetoWeb_SistemaDeVendasContext _context;

        public DepartmentService(ProjetoWeb_SistemaDeVendasContext context)
        {
            _context = context;
        }

        public async Task<List<Department>> FindAllAsync()
        {
            return await _context.Department.OrderBy(x => x.Name).ToListAsync();
        }
    }
}

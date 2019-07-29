using ProjetoWeb_SistemaDeVendas.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjetoWeb_SistemaDeVendas.Service
{
    public class DepartmentService
    {
        private readonly ProjetoWeb_SistemaDeVendasContext _context;

        public DepartmentService(ProjetoWeb_SistemaDeVendasContext context)
        {
            _context = context;
        }

        public List<Department> FindAll()
        {
            return _context.Department.OrderBy(x => x.Name).ToList();
        }
    }
}

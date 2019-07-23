using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjetoWeb_SistemaDeVendas.Models;

namespace ProjetoWeb_SistemaDeVendas.Controllers
{
    public class DepartmentsController : Controller
    {
        public IActionResult Index()
        {
            List<Department> departamentos = new List<Department>
            {
                new Department{ Id = 1, Name = "Eletronics"},
                new Department{ Id = 2, Name = "Fashion"}

            };
            return View(departamentos);
        }
    }
}
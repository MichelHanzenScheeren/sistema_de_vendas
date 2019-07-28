using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjetoWeb_SistemaDeVendas.Service;

namespace ProjetoWeb_SistemaDeVendas.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerService _sellerService;
        public SellersController(SellerService sellerService)
        {
            _sellerService = sellerService;
        }
        public IActionResult Index()
        {
            var sellerList = _sellerService.FindAll();
            return View(sellerList);
        }
    }
}
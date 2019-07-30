using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjetoWeb_SistemaDeVendas.Models;
using ProjetoWeb_SistemaDeVendas.Models.ViewModels;
using ProjetoWeb_SistemaDeVendas.Service;
using ProjetoWeb_SistemaDeVendas.Service.Exceptions;

namespace ProjetoWeb_SistemaDeVendas.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerService _sellerService;
        private readonly DepartmentService _departmentService;

        public SellersController(SellerService sellerService, DepartmentService departmentService)
        {
            _sellerService = sellerService;
            _departmentService = departmentService;
        }
        public IActionResult Index()
        {
            var sellerList = _sellerService.FindAll();
            return View(sellerList);
        }

        public IActionResult Create()
        {
            var departments = _departmentService.FindAll();
            var viewModel = new SellerFormViewModel{ Departments =  departments };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Seller seller)
        {
            _sellerService.Insert(seller);
            return RedirectToAction(nameof(Index)); 
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
                return RedirectToAction(nameof(Error), new { message = "ID NOT FOUND"});

            var seller = _sellerService.FindById(id.Value);
            if (seller == null)
                return RedirectToAction(nameof(Error), new { message = "SELLER NOT FOUND" });

            return View(seller);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _sellerService.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
                return RedirectToAction(nameof(Error), new { message = "ID NOT FOUND" });

            var seller = _sellerService.FindById(id.Value);
            if (seller == null)
                return RedirectToAction(nameof(Error), new { message = "SELLER NOT FOUND" });

            return View(seller);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
                return RedirectToAction(nameof(Error), new { message = "ID NOT FOUND" });

            var seller = _sellerService.FindById(id.Value);
            if (seller == null)
                return RedirectToAction(nameof(Error), new { message = "SELLER NOT FOUND" });

            var departments = _departmentService.FindAll();
            var viewModel = new SellerFormViewModel { Departments = departments, Seller = seller };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Seller seller)
        {
            if (id != seller.Id)
                return RedirectToAction(nameof(Error), new { message = "ID MISMATCH" });
            try
            {
                _sellerService.Update(seller);
                return RedirectToAction(nameof(Index));
            }
            catch (ApplicationException erro)
            {
                return RedirectToAction(nameof(Error), new { message = erro.Message });
            }         
        }

        public IActionResult Error(string message)
        {
            var viewModelError = new ErrorViewModel
            {
                Message = message, 
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };
            return View(viewModelError);
        }
    }

}
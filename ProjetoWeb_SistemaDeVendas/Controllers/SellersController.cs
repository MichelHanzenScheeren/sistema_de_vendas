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

        public async Task<IActionResult> Index()
        {
            var sellerList = await _sellerService.FindAllAsync();
            return View(sellerList);
        }

        public async Task<IActionResult> Create()
        {
            var departments = await _departmentService.FindAllAsync();
            var viewModel = new SellerFormViewModel{ Departments =  departments };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Seller seller)
        {
            if (!ModelState.IsValid)
                return View(new SellerFormViewModel { Seller = seller, Departments = await _departmentService.FindAllAsync() });

            await _sellerService.InsertAsync(seller);
            return RedirectToAction(nameof(Index)); 
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return RedirectToAction(nameof(Error), new { message = "ID NOT FOUND"});

            var seller = await _sellerService.FindByIdAsync(id.Value);
            if (seller == null)
                return RedirectToAction(nameof(Error), new { message = "SELLER NOT FOUND" });

            return View(seller);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _sellerService.RemoveAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (IntegrityException)
            {
                return RedirectToAction(nameof(Error), new { message = "NÃO É POSSÍVEL CONCLUIR O PEDIDO, POIS ELE VIOLA O PRINCÍPIO DE INTEGRIADE REFERENCIAL!!" });
            } 
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return RedirectToAction(nameof(Error), new { message = "ID NOT FOUND" });

            var seller = await _sellerService.FindByIdAsync(id.Value);
            if (seller == null)
                return RedirectToAction(nameof(Error), new { message = "SELLER NOT FOUND" });

            return View(seller);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return RedirectToAction(nameof(Error), new { message = "ID NOT FOUND" });

            var seller = await _sellerService.FindByIdAsync(id.Value);
            if (seller == null)
                return RedirectToAction(nameof(Error), new { message = "SELLER NOT FOUND" });

            var departments = await _departmentService.FindAllAsync();
            var viewModel = new SellerFormViewModel { Departments = departments, Seller = seller };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Seller seller)
        {
            if (!ModelState.IsValid)
                return View(new SellerFormViewModel { Seller = seller, Departments = await _departmentService.FindAllAsync() });

            if (id != seller.Id)
                return RedirectToAction(nameof(Error), new { message = "ID MISMATCH" });
            try
            {
                await _sellerService.UpdateAsync(seller);
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjetoWeb_SistemaDeVendas.Models;
using ProjetoWeb_SistemaDeVendas.Models.ViewModels;

namespace ProjetoWeb_SistemaDeVendas.Controllers
{
    public class SalesRecordsController : Controller
    {
        private readonly ProjetoWeb_SistemaDeVendasContext _context;

        public SalesRecordsController(ProjetoWeb_SistemaDeVendasContext context)
        {
            _context = context;
        }

        // GET: SalesRecords
        public async Task<IActionResult> Index()
        {
            var projetoWeb_SistemaDeVendasContext = _context.SalesRecord.Include(s => s.Seller).OrderByDescending(x => x.Date);
            return View(await projetoWeb_SistemaDeVendasContext.ToListAsync());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(DateTime? minDate, DateTime? maxDate)
        {
            var sales = from obj in _context.SalesRecord select obj;
            if (minDate.HasValue)
                sales = sales.Where(x => x.Date >= minDate.Value);
            if(maxDate.HasValue)
                sales = sales.Where(x => x.Date <= maxDate.Value);

            return View(await sales.Include(x => x.Seller).OrderByDescending(x => x.Date).ToListAsync());
        }

        // GET: SalesRecords/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salesRecord = await _context.SalesRecord
                .Include(s => s.Seller)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (salesRecord == null)
            {
                return NotFound();
            }

            return View(salesRecord);
        }

        // GET: SalesRecords/Create
        public async Task<IActionResult> Create()
        {
            ViewData["SellerId"] = new SelectList(_context.Seller, "Id", "Email");
            var viewModel = new SalesFormViewModel { Sellers = await _context.Seller.ToListAsync() };
            return View(viewModel);
        }

        // POST: SalesRecords/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SalesRecord salesRecord)
        {
            if (ModelState.IsValid)
            {
                _context.Add(salesRecord);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SellerId"] = new SelectList(_context.Seller, "Id", "Email", salesRecord.SellerId);
            return View(salesRecord);
        }

        // GET: SalesRecords/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salesRecord = await _context.SalesRecord.FindAsync(id);
            if (salesRecord == null)
            {
                return NotFound();
            }
            ViewData["SellerId"] = new SelectList(_context.Seller, "Id", "Email", salesRecord.SellerId);
            var viewModel = new SalesFormViewModel { Sellers = await _context.Seller.ToListAsync(), SalesRecord = salesRecord };
            return View(viewModel);
        }

        // POST: SalesRecords/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SalesRecord salesRecord)
        {
            if (id != salesRecord.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(salesRecord);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SalesRecordExists(salesRecord.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["SellerId"] = new SelectList(_context.Seller, "Id", "Email", salesRecord.SellerId);
            return View(salesRecord);
        }

        // GET: SalesRecords/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salesRecord = await _context.SalesRecord
                .Include(s => s.Seller)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (salesRecord == null)
            {
                return NotFound();
            }

            return View(salesRecord);
        }

        // POST: SalesRecords/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var salesRecord = await _context.SalesRecord.FindAsync(id);
            _context.SalesRecord.Remove(salesRecord);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SalesRecordExists(int id)
        {
            return _context.SalesRecord.Any(e => e.Id == id);
        }
    }
}

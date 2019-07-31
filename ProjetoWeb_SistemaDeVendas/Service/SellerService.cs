using Microsoft.EntityFrameworkCore;
using ProjetoWeb_SistemaDeVendas.Models;
using ProjetoWeb_SistemaDeVendas.Service.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoWeb_SistemaDeVendas.Service
{
    public class SellerService
    {
        private readonly ProjetoWeb_SistemaDeVendasContext _context;

        public SellerService(ProjetoWeb_SistemaDeVendasContext context)
        {
            _context = context;
        }

        public async Task InsertAsync(Seller seller)
        {
            _context.Add(seller);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Seller>> FindAllAsync()
        {
            return await _context.Seller.ToListAsync();
        }

        public async Task<Seller> FindByIdAsync(int id)
        {
            return await _context.Seller.Include(x => x.Department).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task RemoveAsync(int id)
        {
            try
            {
                var seller = await _context.Seller.FindAsync(id);
                _context.Seller.Remove(seller);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException erro)
            {
                throw new IntegrityException(erro.Message);
            }
            
        }

        public async Task UpdateAsync(Seller seller)
        {
            bool any = await _context.Seller.AnyAsync(x => x.Id == seller.Id);
            if (!any)
                throw new NotFoundException("ID NOT FOUND!");

            try
            {
                _context.Update(seller);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
            
        }
    }
}

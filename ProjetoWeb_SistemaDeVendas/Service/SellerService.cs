using ProjetoWeb_SistemaDeVendas.Models;
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

        public List<Seller> FindAll()
        {
            return _context.Seller.ToList();
        }

        public void Insert(Seller seller)
        {
            _context.Add(seller);
            _context.SaveChanges();
        }
    }
}

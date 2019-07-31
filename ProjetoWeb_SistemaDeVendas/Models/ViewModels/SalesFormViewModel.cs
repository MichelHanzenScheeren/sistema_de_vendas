using ProjetoWeb_SistemaDeVendas.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoWeb_SistemaDeVendas.Models.ViewModels
{
    public class SalesFormViewModel
    {
        public ICollection<Seller> Sellers { get; set; }
        public SalesRecord SalesRecord { get; set; }
        
    }
}

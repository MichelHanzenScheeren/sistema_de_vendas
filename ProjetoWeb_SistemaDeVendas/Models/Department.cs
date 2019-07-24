using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjetoWeb_SistemaDeVendas.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Seller> Sellers { get; set; }

        public Department()
        {
            Sellers = new List<Seller>();
        }

        public Department(int id, string name) : this()
        {
            Id = id;
            Name = name;
        }

        public void AddSeller(Seller seller)
        {
            Sellers.Add(seller);
        }

        public double TotalSales(DateTime init, DateTime final)
        {
            return Sellers.Sum(x => x.TotalSales(init, final));
        }
    }
}

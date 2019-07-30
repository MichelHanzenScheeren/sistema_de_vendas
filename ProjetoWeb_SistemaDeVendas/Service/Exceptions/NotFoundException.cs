using System;

namespace ProjetoWeb_SistemaDeVendas.Service.Exceptions
{
    public class NotFoundException : ApplicationException
    {
        public NotFoundException(string msg) : base(msg)
        {
        }
    }
}

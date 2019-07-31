using System;

namespace ProjetoWeb_SistemaDeVendas.Service.Exceptions
{
    public class IntegrityException : ApplicationException
    {
        public IntegrityException(string message) : base(message)
        {
        }
    }
}

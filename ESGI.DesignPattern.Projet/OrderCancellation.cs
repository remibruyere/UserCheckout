using System;

namespace ESGI.DesignPattern.Projet
{
    public class OrderCancelledException : ApplicationException
    {
        public OrderCancelledException(Product product)
            : base(product.Name)
        {
        }
    }
}

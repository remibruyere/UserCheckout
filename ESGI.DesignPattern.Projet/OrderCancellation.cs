using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

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

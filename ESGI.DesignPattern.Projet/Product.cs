using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ESGI.DesignPattern.Projet
{
    public class Product
    {

        public string Name { get; private set; }

        public Product(string name) => this.Name = name;

        public override string ToString()
        {
            return Name;
        }
    }
}

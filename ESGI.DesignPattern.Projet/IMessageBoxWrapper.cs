using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace ESGI.DesignPattern.Projet
{
    public interface IMessageBoxWrapper
    {
        MessageBoxResult Show(string message);
    }
}

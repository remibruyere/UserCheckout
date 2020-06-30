using System;
using System.Collections.Generic;
using System.Text;
using PresentationFake;

namespace ESGI.DesignPattern.Projet
{
    public interface IMessageBoxWrapper
    {
        MessageBoxResult Show(string message);
    }
}

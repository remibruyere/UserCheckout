using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace ESGI.DesignPattern.Projet
{
    class MessageBoxWrapper
    {
        public MessageBoxResult Show(string message)
        {
            return MessageBox.Show(message,
                "Choose Option",
                 MessageBoxButton.YesNo);
        }
    }
}

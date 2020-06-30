using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Windows;

namespace ESGI.DesignPattern.Projet
{
    public class UserConfirmation
    {
        public bool Accepted { get; private set; }

        public UserConfirmation(string message)
        {
            var result = MessageBox.Show(
                 message,
                 "Choose Option",
                 0,
                 0);

            this.Accepted = result == MessageBoxResult.Yes;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using PresentationFake;

namespace ESGI.DesignPattern.Projet
{
    class MessageBoxWrapper
    {
        public MessageBoxResult Show(string message)
        {
            return MessageBox.Show(message,
                "Choose Option",
                 MessageBoxButtons.YESNO);
        }
    }
}

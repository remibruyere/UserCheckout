using PresentationFake;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Windows;

namespace ESGI.DesignPattern.Projet
{
    public class UserConfirmation
    {
        private readonly string message;
        private readonly IMessageBoxWrapper messageBoxWrapper;

        public bool Accepted { get; private set; }

        public UserConfirmation(string message, IMessageBoxWrapper messageBoxWrapper)
        {
            this.message = message;
            this.messageBoxWrapper = messageBoxWrapper;
            this.Accepted = false;
        }

        public void AskConfirmation()
        {
            var result = messageBoxWrapper.Show(message);

            Console.WriteLine(result);

            this.Accepted = result == MessageBoxResult.YES;
        }
    }
}

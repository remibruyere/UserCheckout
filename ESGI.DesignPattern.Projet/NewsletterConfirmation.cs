using System;
using System.Collections.Generic;
using System.Text;
using PresentationFake;

namespace ESGI.DesignPattern.Projet
{
    public class NewsletterConfirmation : IUserConfirmation
    {
        private readonly string message;
        private readonly IMessageBoxWrapper messageBoxWrapper;

        public bool Accepted { get; private set; }

        public NewsletterConfirmation(Product product, IMessageBoxWrapper messageBoxWrapper)
        {
            this.message = "Subscribe to our product " + product.Name + " newsletter?";
            this.messageBoxWrapper = messageBoxWrapper;
            this.Accepted = false;
        }

        public void AskConfirmation()
        {
            var result = messageBoxWrapper.Show(message);

            this.Accepted = result == MessageBoxResult.YES;
        }
    }
}

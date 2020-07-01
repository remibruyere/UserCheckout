using System;
using System.Collections.Generic;
using System.Text;

namespace ESGI.DesignPattern.Projet
{
    public class Checkout
    {
        private readonly Product product;

        private readonly IEmailService emailService;

        private readonly IUserConfirmation newsLetterSubscribed;

        private readonly IUserConfirmation termsAndConditionsAccepted;

        public Checkout(Product product, IEmailService emailService, IUserConfirmation newsletterConfirmation, IUserConfirmation termsAndConditionsConfirmation)
        {
            this.product = product;
            this.emailService = emailService;
            this.newsLetterSubscribed = newsletterConfirmation;
            this.termsAndConditionsAccepted = termsAndConditionsConfirmation;
        }

        public virtual void ConfirmOrder()
        {
            newsLetterSubscribed.AskConfirmation();
            termsAndConditionsAccepted.AskConfirmation();
            if (!termsAndConditionsAccepted.Accepted)
            {
                throw new OrderCancelledException(product);
            }
            if (newsLetterSubscribed.Accepted)
            {
                emailService.SubscribeUserFor(product);
            }
        }
    }
}

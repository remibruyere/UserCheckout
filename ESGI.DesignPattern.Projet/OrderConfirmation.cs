namespace ESGI.DesignPattern.Projet
{
    public class OrderConfirmation
    {
        private readonly NewsletterConfirmation newsletterConfirmation;
        private readonly TermsAndConditionsConfirmation termsAndConditionsConfirmation;

        public OrderConfirmation(NewsletterConfirmation newsletterConfirmation, TermsAndConditionsConfirmation termsAndConditionsConfirmation)
        {
            this.newsletterConfirmation = newsletterConfirmation;
            this.termsAndConditionsConfirmation = termsAndConditionsConfirmation;
        }

        public void ConfirmTermsAndConditions(Product product)
        {
            termsAndConditionsConfirmation.AskConfirmation();
            if (!termsAndConditionsConfirmation.Accepted)
            {
                throw new OrderCancelledException(product);
            }
        }

        public void ConfirmNewsletter(Product product, IEmailService emailService)
        {
            newsletterConfirmation.AskConfirmation();
            if (newsletterConfirmation.Accepted)
            {
                emailService.SubscribeUserFor(product);
            }
        }
    }
}

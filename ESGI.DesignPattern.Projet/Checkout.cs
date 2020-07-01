namespace ESGI.DesignPattern.Projet
{
    public class Checkout
    {
        private readonly Product product;

        private readonly IEmailService emailService;

        private readonly OrderConfirmation orderConfirmation;

        public Checkout(Product product, IEmailService emailService, OrderConfirmation orderConfirmation)
        {
            this.product = product;
            this.emailService = emailService;
            this.orderConfirmation = orderConfirmation;
        }

        public virtual void ConfirmOrder()
        {
            orderConfirmation.ConfirmTermsAndConditions(product);
            orderConfirmation.ConfirmNewsletter(product, emailService);
        }
    }
}

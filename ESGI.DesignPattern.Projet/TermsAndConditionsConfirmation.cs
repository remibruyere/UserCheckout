using PresentationFake;

namespace ESGI.DesignPattern.Projet
{
    public class TermsAndConditionsConfirmation : IUserConfirmation
    {
        private readonly string message;
        private readonly IMessageBoxWrapper messageBoxWrapper;

        public bool Accepted { get; private set; }

        public TermsAndConditionsConfirmation(Product product, IMessageBoxWrapper messageBoxWrapper)
        {
            this.message = "Accept our terms and conditions?\n(Mandatory to place order for " + product.Name + ")";
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

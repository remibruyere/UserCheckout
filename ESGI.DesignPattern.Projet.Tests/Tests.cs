using Moq;
using PresentationFake;
using Xunit;

namespace ESGI.DesignPattern.Projet.Tests
{
    public class Tests
    {
        [Fact]
        public void Checkout_subscribed_to_newsletter_and_accepted_terms_conditions()
        {
            Product product = new Product("test_product");

            Mock<IEmailService> mockEmailService = new Mock<IEmailService>();
            Mock<IMessageBoxWrapper> mockMessageBoxWrapper = new Mock<IMessageBoxWrapper>();
            mockMessageBoxWrapper.Setup(x => x.Show(It.IsAny<string>())).Returns(MessageBoxResult.YES);

            IUserConfirmation newsletterConfirmation = UserConfirmationFactory.Create(SubjectUserConfirmation.NewsLetterSubscription, product, mockMessageBoxWrapper.Object);
            IUserConfirmation termsAndConditionsConfirmation = UserConfirmationFactory.Create(SubjectUserConfirmation.TermsAndConditions, product, mockMessageBoxWrapper.Object);

            Checkout checkout = new Checkout(
                product,
                mockEmailService.Object,
                newsletterConfirmation,
                termsAndConditionsConfirmation);

            checkout.ConfirmOrder();

            mockEmailService.Verify(m => m.SubscribeUserFor(product));
        }

        [Fact]
        public void Checkout_not_subscribed_to_newsletter()
        {
            Product product = new Product("test_product");

            Mock<IEmailService> mockEmailService = new Mock<IEmailService>();
            Mock<IMessageBoxWrapper> mockMessageBoxWrapper = new Mock<IMessageBoxWrapper>();

            mockMessageBoxWrapper.Setup(x => x.Show(It.IsAny<string>())).Returns(MessageBoxResult.YES);
            mockMessageBoxWrapper.Setup(x => x.Show("Subscribe to our product " + product.Name + " newsletter?")).Returns(MessageBoxResult.NO);

            IUserConfirmation newsletterConfirmation = UserConfirmationFactory.Create(SubjectUserConfirmation.NewsLetterSubscription, product, mockMessageBoxWrapper.Object);
            IUserConfirmation termsAndConditionsConfirmation = UserConfirmationFactory.Create(SubjectUserConfirmation.TermsAndConditions, product, mockMessageBoxWrapper.Object);

            Checkout checkout = new Checkout(
                product,
                mockEmailService.Object,
                newsletterConfirmation,
                termsAndConditionsConfirmation);

            mockEmailService.Verify(m => m.SubscribeUserFor(product), Times.Never());
        }

        [Fact]
        public void Checkout_terms_condition_not_accepted()
        {
            Product product = new Product("test_product");

            Mock<IEmailService> mockEmailService = new Mock<IEmailService>();
            Mock<IMessageBoxWrapper> mockMessageBoxWrapper = new Mock<IMessageBoxWrapper>();

            mockMessageBoxWrapper.Setup(x => x.Show(It.IsAny<string>())).Returns(MessageBoxResult.YES);
            mockMessageBoxWrapper.Setup(x => x.Show("Accept our terms and conditions?\n(Mandatory to place order for " + product.Name + ")")).Returns(MessageBoxResult.NO);

            IUserConfirmation newsletterConfirmation = UserConfirmationFactory.Create(SubjectUserConfirmation.NewsLetterSubscription, product, mockMessageBoxWrapper.Object);
            IUserConfirmation termsAndConditionsConfirmation = UserConfirmationFactory.Create(SubjectUserConfirmation.TermsAndConditions, product, mockMessageBoxWrapper.Object);

            Checkout checkout = new Checkout(
                product,
                mockEmailService.Object,
                newsletterConfirmation,
                termsAndConditionsConfirmation);

            Assert.Throws<OrderCancelledException>(() =>
            {
                checkout.ConfirmOrder();
            });

            mockEmailService.Verify(m => m.SubscribeUserFor(product), Times.Never());
        }

        [Fact]
        public void Checkout_terms_condition_and_subcribe_not_accepted()
        {
            Product product = new Product("test_product");

            Mock<IEmailService> mockEmailService = new Mock<IEmailService>();
            Mock<IMessageBoxWrapper> mockMessageBoxWrapper = new Mock<IMessageBoxWrapper>();

            mockMessageBoxWrapper.Setup(x => x.Show(It.IsAny<string>())).Returns(MessageBoxResult.NO);

            IUserConfirmation newsletterConfirmation = UserConfirmationFactory.Create(SubjectUserConfirmation.NewsLetterSubscription, product, mockMessageBoxWrapper.Object);
            IUserConfirmation termsAndConditionsConfirmation = UserConfirmationFactory.Create(SubjectUserConfirmation.TermsAndConditions, product, mockMessageBoxWrapper.Object);

            Checkout checkout = new Checkout(
                product,
                mockEmailService.Object,
                newsletterConfirmation,
                termsAndConditionsConfirmation);

            Assert.Throws<OrderCancelledException>(() =>
            {
                checkout.ConfirmOrder();
            });

            mockEmailService.Verify(m => m.SubscribeUserFor(product), Times.Never());
        }
    }
}


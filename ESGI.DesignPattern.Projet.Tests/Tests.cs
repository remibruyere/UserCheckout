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

            NewsletterConfirmation newsletterConfirmation = (NewsletterConfirmation)UserConfirmationFactory.Create(SubjectUserConfirmation.NewsLetterSubscription, product, mockMessageBoxWrapper.Object);
            TermsAndConditionsConfirmation termsAndConditionsConfirmation = (TermsAndConditionsConfirmation)UserConfirmationFactory.Create(SubjectUserConfirmation.TermsAndConditions, product, mockMessageBoxWrapper.Object);

            OrderConfirmation orderConfirmation = new OrderConfirmation(newsletterConfirmation, termsAndConditionsConfirmation);

            Checkout checkout = new Checkout(
                product,
                mockEmailService.Object,
                orderConfirmation);

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

            NewsletterConfirmation newsletterConfirmation = (NewsletterConfirmation)UserConfirmationFactory.Create(SubjectUserConfirmation.NewsLetterSubscription, product, mockMessageBoxWrapper.Object);
            TermsAndConditionsConfirmation termsAndConditionsConfirmation = (TermsAndConditionsConfirmation)UserConfirmationFactory.Create(SubjectUserConfirmation.TermsAndConditions, product, mockMessageBoxWrapper.Object);

            OrderConfirmation orderConfirmation = new OrderConfirmation(newsletterConfirmation, termsAndConditionsConfirmation);

            Checkout checkout = new Checkout(
                product,
                mockEmailService.Object,
                orderConfirmation);

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

            NewsletterConfirmation newsletterConfirmation = (NewsletterConfirmation)UserConfirmationFactory.Create(SubjectUserConfirmation.NewsLetterSubscription, product, mockMessageBoxWrapper.Object);
            TermsAndConditionsConfirmation termsAndConditionsConfirmation = (TermsAndConditionsConfirmation)UserConfirmationFactory.Create(SubjectUserConfirmation.TermsAndConditions, product, mockMessageBoxWrapper.Object);

            OrderConfirmation orderConfirmation = new OrderConfirmation(newsletterConfirmation, termsAndConditionsConfirmation);

            Checkout checkout = new Checkout(
                product,
                mockEmailService.Object,
                orderConfirmation);

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

            NewsletterConfirmation newsletterConfirmation = (NewsletterConfirmation)UserConfirmationFactory.Create(SubjectUserConfirmation.NewsLetterSubscription, product, mockMessageBoxWrapper.Object);
            TermsAndConditionsConfirmation termsAndConditionsConfirmation = (TermsAndConditionsConfirmation)UserConfirmationFactory.Create(SubjectUserConfirmation.TermsAndConditions, product, mockMessageBoxWrapper.Object);

            OrderConfirmation orderConfirmation = new OrderConfirmation(newsletterConfirmation, termsAndConditionsConfirmation);

            Checkout checkout = new Checkout(
                product,
                mockEmailService.Object,
                orderConfirmation);

            Assert.Throws<OrderCancelledException>(() =>
            {
                checkout.ConfirmOrder();
            });

            mockEmailService.Verify(m => m.SubscribeUserFor(product), Times.Never());
        }
    }
}


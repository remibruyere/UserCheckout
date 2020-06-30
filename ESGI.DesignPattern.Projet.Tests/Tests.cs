using System;
using Xunit;
using Moq;
using PresentationFake;

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

            Checkout checkout = new Checkout(product, mockEmailService.Object, mockMessageBoxWrapper.Object);

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

            Checkout checkout = new Checkout(product, mockEmailService.Object, mockMessageBoxWrapper.Object);

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

            Checkout checkout = new Checkout(product, mockEmailService.Object, mockMessageBoxWrapper.Object);

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

            Checkout checkout = new Checkout(product, mockEmailService.Object, mockMessageBoxWrapper.Object);

            Assert.Throws<OrderCancelledException>(() =>
            {
                checkout.ConfirmOrder();
            });

            mockEmailService.Verify(m => m.SubscribeUserFor(product), Times.Never());
        }
    }
}


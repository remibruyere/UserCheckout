using System;
using Xunit;
using Moq;
using System.Windows;

namespace ESGI.DesignPattern.Projet.Tests
{
    public class Tests
    {
        [Fact]
        public void Checkout()
        {
            Product product = new Product("test_product");

            Mock<IEmailService> mockEmailService = new Mock<IEmailService>();
            Mock<IMessageBoxWrapper> mockMessageBoxWrapper = new Mock<IMessageBoxWrapper>();
            mockMessageBoxWrapper.Setup(x => x.Show(It.IsAny<string>())).Returns(MessageBoxResult.OK);

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
            mockMessageBoxWrapper.Setup(x => x.Show(It.IsAny<string>())).Returns(MessageBoxResult.No);

            Checkout checkout = new Checkout(product, mockEmailService.Object, mockMessageBoxWrapper.Object);

           var response = Assert.Throws<OrderCancelledException>(() =>
            {
                checkout.ConfirmOrder();
            });



        }

        //User should accept terms and contions

        //User maybe accepted to receive newletter
    }
}


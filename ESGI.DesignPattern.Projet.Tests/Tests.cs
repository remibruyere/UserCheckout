using System;
using Xunit;
using Moq;

namespace ESGI.DesignPattern.Projet.Tests
{
    public class Tests
    {
        [Fact]
        public void Checkout()
        {
            Product product = new Product("test_product");

            Mock<IEmailService> mockEmailService = new Mock<IEmailService>();

            Checkout checkout = new Checkout(product, mockEmailService.Object);

            checkout.ConfirmOrder();

            mockEmailService.Verify(m => m.SubscribeUserFor(product));
        }

        //User should accept terms and contions

        //User maybe accepted to receive newletter
    }
}


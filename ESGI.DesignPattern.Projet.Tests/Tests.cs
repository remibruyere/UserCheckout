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

            Mock<IEmailService> mock = new Mock<IEmailService>();

            //UserConfirmation userAccepted = new UserConfirmation("LOL");

        }

        //User should accept terms and contions

        //User maybe accepted to receive newletter
    }
}


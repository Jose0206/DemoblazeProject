using Microsoft.VisualStudio.TestTools.UnitTesting;
using DemoblazeProject.CommonFunctions;
using System;
using System.Linq;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;
using DemoblazeProject.CommonFramework;
using DemoblazeProject.DemoObjects;
using FluentAssertions;
using DemoblazeProject.Framework.CommonFunctions;
using DemoblazeProject.DemoObjects.Components;

namespace DemoblazeProject.Tests
{
    [TestClass]
    public class OrderTests : DemoblazeBaseClass
    {
        [DeploymentItem("chromedriver.exe")]
        [TestCategory("Order Tests")]
        [TestMethod]
        public void PlaceOrder_E2ETest()
        {
            var Home = new HomePage(DefaultWait);
            var SignUp = new SignUpPage(DefaultWait);
            var Login = new LoginPage(DefaultWait);
            var Product = new ProductPage(DefaultWait);
            var Cart = new CartPage(DefaultWait);
            var Order = new PlaceOrderPage(DefaultWait);
            var randomguid = Guid.NewGuid();
            var shortguid = randomguid.ToString().Substring(5, 7);
            var username = "Jose" + shortguid;
            var password = "secret" + shortguid;
            var product = "Samsung galaxy s6";
            Home.productStoreLogo.Displayed.Should().BeTrue();
            Home.selectOption("Sign up");
            SignUp.SignUp(username, password);
            Home.selectOption("Log in");
            Login.SignIn(username, password);
            Home.validateOptionDisplayed("Welcome " + username).Should().BeTrue();
            Home.selectProduct(product);
            Product.productNameLabel.Text.Should().Contain(product);
            Product.addProductToCart("Product added.");
            Home.selectOption("Cart");
            Cart.productIncart(product).Should().BeTrue();
            Cart.placeOrderButton.SafeJsClick();
            Order.placeOrderLabel.Displayed.Should().BeTrue();
            Order.placeOrder("Jose", "USA", "Miami", "4242424242424242", "12", "24");
            WebElementExtensions.WaitForSpinningWheel();
            Order.confirmationModal.Displayed.Should().BeTrue();
            Home.selectOption("Logout");
            Home.validateOptionDisplayed("Log in");
        }


        [TestCategory("Order Tests")]
        [TestMethod]
        public void PlaceOrder_withEmptyValues()
        {
            var Home = new HomePage(DefaultWait);
            var SignUp = new SignUpPage(DefaultWait);
            var Login = new LoginPage(DefaultWait);
            var Product = new ProductPage(DefaultWait);
            var Cart = new CartPage(DefaultWait);
            var Order = new PlaceOrderPage(DefaultWait);
            var Modal = new ModalWindow(DefaultWait);
            var randomguid = Guid.NewGuid();
            var shortguid = randomguid.ToString().Substring(5, 7);
            var username = "Jose" + shortguid;
            var password = "secret" + shortguid;
            var product = "Samsung galaxy s6";
            Home.productStoreLogo.Displayed.Should().BeTrue();
            Home.selectOption("Sign up");
            SignUp.SignUp(username, password);
            Home.selectOption("Log in");
            Login.SignIn(username, password);
            Home.validateOptionDisplayed("Welcome " + username).Should().BeTrue();
            Home.selectProduct(product);
            Product.productNameLabel.Text.Should().Contain(product);
            Product.addProductToCart("Product added.");
            Home.selectOption("Cart");
            Cart.productIncart(product).Should().BeTrue();
            Cart.placeOrderButton.SafeJsClick();
            Order.placeOrderLabel.Displayed.Should().BeTrue();
            Order.placeOrder("", "", "", "", "", "");
            Modal.validateAlertResult("Please fill out Name and Creditcard.").Should().BeTrue();
        }


        [TestCategory("Order Tests")]
        [TestMethod]
        public void PlaceOrder_withEmptyCart()//I was expectin to be failed but it's allowing complete the order with empty cart
        {
            var Home = new HomePage(DefaultWait);
            var SignUp = new SignUpPage(DefaultWait);
            var Login = new LoginPage(DefaultWait);
            var Product = new ProductPage(DefaultWait);
            var Cart = new CartPage(DefaultWait);
            var Order = new PlaceOrderPage(DefaultWait);
            var randomguid = Guid.NewGuid();
            var shortguid = randomguid.ToString().Substring(5, 7);
            var username = "Jose" + shortguid;
            var password = "secret" + shortguid;
            var product = "Samsung galaxy s6";
            Home.productStoreLogo.Displayed.Should().BeTrue();
            Home.selectOption("Sign up");
            SignUp.SignUp(username, password);
            Home.selectOption("Log in");
            Login.SignIn(username, password);
            Home.validateOptionDisplayed("Welcome " + username).Should().BeTrue();
            Home.selectOption("Cart");
            Cart.placeOrderButton.SafeJsClick();
            Order.placeOrderLabel.Displayed.Should().BeTrue();
            Order.placeOrder("Jose", "USA", "Miami", "4242424242424242", "12", "24");
            WebElementExtensions.WaitForSpinningWheel();
            Order.confirmationModal.Displayed.Should().BeFalse();
            Home.selectOption("Logout");
            Home.validateOptionDisplayed("Log in");
        }
    }
}

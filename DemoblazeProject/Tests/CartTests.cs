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

namespace DemoblazeProject.Tests
{
    [TestClass]
    public class CartTests : DemoblazeBaseClass
    {
        [DeploymentItem("chromedriver.exe")]
        [TestCategory("Cart Tests")]
        [TestMethod]
        public void AddProduct()
        {
            var Home = new HomePage(DefaultWait);
            var SignUp = new SignUpPage(DefaultWait);
            var Login = new LoginPage(DefaultWait);
            var Product = new ProductPage(DefaultWait);
            var randomguid = Guid.NewGuid();
            var shortguid = randomguid.ToString().Substring(5, 7);
            var username = "Jose" + shortguid;
            var password = "secret" + shortguid;
            List<string> products = new List<string>();
            products.Add("Samsung galaxy s6");
            products.Add("Nokia lumia 1520");
            Home.productStoreLogo.Displayed.Should().BeTrue();
            Home.selectOption("Sign up");
            SignUp.SignUp(username, password);
            Home.selectOption("Log in");
            Login.SignIn(username, password);
            WebElementExtensions.WaitForSpinningWheel();
            Home.validateOptionDisplayed("Welcome " + username).Should().BeTrue();
            foreach (var product in products)
            {
               Home.selectProduct(product);
               Product.productNameLabel.Text.Should().Contain(product);
               Product.addProductToCartAndValidateInfoInCart().Should().BeTrue();
               Home.selectOption("Home");
            }
        }


        [TestCategory("Cart Tests")]
        [TestMethod]
        public void AddMultiplesProducts()
        {
            var Home = new HomePage(DefaultWait);
            var SignUp = new SignUpPage(DefaultWait);
            var Login = new LoginPage(DefaultWait);
            var Product = new ProductPage(DefaultWait);
            var randomguid = Guid.NewGuid();
            var shortguid = randomguid.ToString().Substring(5, 7);
            var username = "Jose" + shortguid;
            var password = "secret" + shortguid;
            List<string> products = new List<string>();
            products.Add("Samsung galaxy s6");
            products.Add("Nokia lumia 1520");
            products.Add("Iphone 6 32gb");
            Home.productStoreLogo.Displayed.Should().BeTrue();
            Home.selectOption("Sign up");
            SignUp.SignUp(username, password);
            Home.selectOption("Log in");
            Login.SignIn(username, password);
            WebElementExtensions.WaitForSpinningWheel();
            Home.validateOptionDisplayed("Welcome " + username).Should().BeTrue();
            foreach (var product in products)
            {
                Home.selectProduct(product);
                Product.productNameLabel.Text.Should().Contain(product);
                Product.addProductToCartAndValidateInfoInCart().Should().BeTrue();
                Home.selectOption("Home");
            }
            Home.selectOption("Cart");
        }


        [TestCategory("Cart Tests")]
        [TestMethod]
        public void AddProducts_And_ValidateTotal()
        {
            var Home = new HomePage(DefaultWait);
            var SignUp = new SignUpPage(DefaultWait);
            var Login = new LoginPage(DefaultWait);
            var Product = new ProductPage(DefaultWait);
            var randomguid = Guid.NewGuid();
            var shortguid = randomguid.ToString().Substring(5, 7);
            var username = "Jose" + shortguid;
            var password = "secret" + shortguid;
            List<string> products = new List<string>();
            products.Add("Samsung galaxy s6");
            products.Add("Nokia lumia 1520");
            products.Add("Iphone 6 32gb");
            Home.productStoreLogo.Displayed.Should().BeTrue();
            Home.selectOption("Sign up");
            SignUp.SignUp(username, password);
            Home.selectOption("Log in");
            Login.SignIn(username, password);
            WebElementExtensions.WaitForSpinningWheel();
            Home.validateOptionDisplayed("Welcome " + username).Should().BeTrue();
            foreach (var product in products)
            {
                Home.selectProduct(product);
                Product.productNameLabel.Text.Should().Contain(product);
                Product.addProductToCartAndValidateInfoInCart().Should().BeTrue();
                Home.selectOption("Home");
            }
            Home.selectOption("Cart");
            Home.cartPage.validateCartTotal().Should().BeTrue();
        }


        [TestCategory("Cart Tests")]
        [TestMethod]
        public void DeleteProduct()
        {
            var Home = new HomePage(DefaultWait);
            var SignUp = new SignUpPage(DefaultWait);
            var Login = new LoginPage(DefaultWait);
            var Product = new ProductPage(DefaultWait);
            var Cart = new CartPage(DefaultWait);
            Product = new ProductPage(DefaultWait);
            var randomguid = Guid.NewGuid();
            var shortguid = randomguid.ToString().Substring(5, 7);
            var username = "Jose" + shortguid;
            var password = "secret" + shortguid;
            List<string> products = new List<string>();
            products.Add("Samsung galaxy s6");
            products.Add("Nokia lumia 1520");
            Home.productStoreLogo.Displayed.Should().BeTrue();
            Home.selectOption("Sign up");
            SignUp.SignUp(username, password);
            Home.selectOption("Log in");
            Login.SignIn(username, password);
            WebElementExtensions.WaitForSpinningWheel();
            Home.validateOptionDisplayed("Welcome " + username).Should().BeTrue();
            foreach (var product in products)
            {
                Home.selectProduct(product);
                Product.productNameLabel.Text.Should().Contain(product);
                Product.addProductToCartAndValidateInfoInCart().Should().BeTrue();
                Home.selectOption("Home");
            }
            Home.selectOption("Cart");
            Cart.deleteProduct("Samsung galaxy s6").Should().BeTrue();
            WebElementExtensions.WaitForSpinningWheel();
        }


    }
}

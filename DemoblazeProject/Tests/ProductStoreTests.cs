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
    public class ProductStoreTests : DemoblazeBaseClass
    {
        [DeploymentItem("chromedriver.exe")]
        [TestCategory("ProductStore Tests")]
        [TestMethod]
        public void validateProductsAreDisplayed()
        {
            var Home = new HomePage(DefaultWait);
            var SignUp = new SignUpPage(DefaultWait);
            var Login = new LoginPage(DefaultWait);
            var randomguid = Guid.NewGuid();
            var shortguid = randomguid.ToString().Substring(5, 7);
            var username = "Jose" + shortguid;
            var password = "secret" + shortguid;
            Home.productStoreLogo.Displayed.Should().BeTrue();
            Home.selectOption("Sign up");
            SignUp.SignUp(username, password);
            Home.selectOption("Log in");
            Login.SignIn(username, password);
            WebElementExtensions.WaitForSpinningWheel();
            Home.validateOptionDisplayed("Welcome " + username).Should().BeTrue();
            Home.validateProductsDisplayed().Should().BeTrue();
            Home.selectOption("Logout");
            Home.validateOptionDisplayed("Log in");
        }


        [TestCategory("ProductStore Tests")]
        [TestMethod]
        public void validateExistingCategories()
        {
            var Home = new HomePage(DefaultWait);
            var SignUp = new SignUpPage(DefaultWait);
            var Login = new LoginPage(DefaultWait);
            var randomguid = Guid.NewGuid();
            var shortguid = randomguid.ToString().Substring(5, 7);
            var username = "Jose" + shortguid;
            var password = "secret" + shortguid;
            List<string> categories = new List<string>();
            categories.Add("Phones");
            categories.Add("Laptops");
            categories.Add("Monitors");
            Home.productStoreLogo.Displayed.Should().BeTrue();
            Home.selectOption("Sign up");
            SignUp.SignUp(username, password);
            Home.selectOption("Log in");
            Login.SignIn(username, password);
            WebElementExtensions.WaitForSpinningWheel();
            Home.validateOptionDisplayed("Welcome " + username).Should().BeTrue();
            Home.validateExistingCategories(categories);
            Home.selectOption("Logout");
            Home.validateOptionDisplayed("Log in");
        }


        [TestCategory("ProductStore Tests")]
        [TestMethod]
        public void validateProductsPagination()
        {
            var Home = new HomePage(DefaultWait);
            var SignUp = new SignUpPage(DefaultWait);
            var Login = new LoginPage(DefaultWait);
            var randomguid = Guid.NewGuid();
            var shortguid = randomguid.ToString().Substring(5, 7);
            var username = "Jose" + shortguid;
            var password = "secret" + shortguid;
            Home.productStoreLogo.Displayed.Should().BeTrue();
            Home.selectOption("Sign up");
            SignUp.SignUp(username, password);
            Home.selectOption("Log in");
            Login.SignIn(username, password);
            WebElementExtensions.WaitForSpinningWheel();
            Home.validateOptionDisplayed("Welcome " + username).Should().BeTrue();
            Home.validateProductsPagination().Should().BeTrue();
            Home.selectOption("Logout");
            Home.validateOptionDisplayed("Log in");
        }
    }
}

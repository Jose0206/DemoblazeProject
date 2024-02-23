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
    public class LoginTests : DemoblazeBaseClass
    {
        [DeploymentItem("chromedriver.exe")]
        [TestCategory("Login Tests")]
        [TestMethod]
        public void Create_User_and_Login()
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
            Home.validateOptionDisplayed("Welcome " + username).Should().BeTrue();
        }


        [TestCategory("Login Tests")]
        [TestMethod]
        public void CreateUser_and_LoginUsingEmptyUser()
        {
            var Home = new HomePage(DefaultWait);
            var SignUp = new SignUpPage(DefaultWait);
            var Login = new LoginPage(DefaultWait);
            var Modal = new ModalWindow(DefaultWait);
            var randomguid = Guid.NewGuid();
            var shortguid = randomguid.ToString().Substring(5, 7);
            var username = "Jose" + shortguid;
            var password = "secret" + shortguid;
            Home.productStoreLogo.Displayed.Should().BeTrue();
            Home.selectOption("Sign up");
            SignUp.SignUp(username, password);
            Home.selectOption("Log in");
            Login.SignIn("", password, "Please fill out Username and Password.").Should().BeTrue();
            WebElementExtensions.WaitForSpinningWheel();
        }


        [TestCategory("Login Tests")]
        [TestMethod]
        public void CreateUser_and_LoginUsingEmptyPassword()
        {
            var Home = new HomePage(DefaultWait);
            var SignUp = new SignUpPage(DefaultWait);
            var Login = new LoginPage(DefaultWait);
            var Modal = new ModalWindow(DefaultWait);
            var randomguid = Guid.NewGuid();
            var shortguid = randomguid.ToString().Substring(5, 7);
            var username = "Jose" + shortguid;
            var password = "secret" + shortguid;
            Home.productStoreLogo.Displayed.Should().BeTrue();
            Home.selectOption("Sign up");
            SignUp.SignUp(username, password);
            Home.selectOption("Log in");
            Login.SignIn(username, "", "Please fill out Username and Password.").Should().BeTrue();
            WebElementExtensions.WaitForSpinningWheel();
        }
    }
}

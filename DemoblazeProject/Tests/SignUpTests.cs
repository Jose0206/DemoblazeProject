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
    public class SignUpTests : DemoblazeBaseClass
    {
        [DeploymentItem("chromedriver.exe")]
        [TestCategory("SignUp Tests")]
        [TestMethod]
        public void Create_User()
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
        }


        [TestCategory("SignUp Tests")]
        [TestMethod]
        public void CreateUser_With_EmptyValues()
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
            SignUp.SignUp("", "", "Please fill out Username and Password.").Should().BeTrue();
        }


        [TestCategory("SignUp Tests")]
        [TestMethod]
        public void CreateUser_With_EmptyUserName()
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
            SignUp.SignUp("", password, "Please fill out Username and Password.").Should().BeTrue();
        }


        [TestCategory("SignUp Tests")]
        [TestMethod]
        public void CreateUser_With_EmptyPassword()
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
            SignUp.SignUp(username, "", "Please fill out Username and Password.").Should().BeTrue();
        }


        [TestCategory("SignUp Tests")]
        [TestMethod]
        public void CreateUser_With_MoreThan50Characters() //I was expecting to failed but the page doesn't have any validaton for the maximun of characteres
        {
            var Home = new HomePage(DefaultWait);
            var SignUp = new SignUpPage(DefaultWait);
            var Login = new LoginPage(DefaultWait);
            var Modal = new ModalWindow(DefaultWait);
            var randomguid = Guid.NewGuid();
            var shortguid = randomguid.ToString().Substring(5, 7);
            var username = "If you want to create robust, browser-based regression " +
                           "automation suites and tests, scale and distribute scripts " +
                           "across many environments, then you want to use Selenium WebDriver, " +
                           "a collection of language specific bindings to drive a browser - the " +
                           "way it is meant to be drive" + shortguid;
            var password = "If you want to create robust, browser-based regression " +
                           "automation suites and tests, scale and distribute scripts " +
                           "across many environments, then you want to use Selenium WebDriver, " +
                           "a collection of language specific bindings to drive a browser - the " +
                           "way it is meant to be drive";
            Home.productStoreLogo.Displayed.Should().BeTrue();
            Home.selectOption("Sign up");
            SignUp.SignUp(username, password);
            Home.selectOption("Log in");
            Login.SignIn(username, password);
            Home.validateOptionDisplayed("Welcome " + username).Should().BeFalse();
        }
    }
}

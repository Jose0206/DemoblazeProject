using DemoblazeProject.CommonFramework;
using DemoblazeProject.DemoObjects;
using DemoblazeProject.Framework.CommonFunctions;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoblazeProject.Tests
{
    [TestClass]
    public class ContactTests : DemoblazeBaseClass
    {
        [DeploymentItem("chromedriver.exe")]
        [TestCategory("ProductStore Tests")]
        [TestMethod]
        public void SendContactMessage()
        {
            var Home = new HomePage(DefaultWait);
            var SignUp = new SignUpPage(DefaultWait);
            var Login = new LoginPage(DefaultWait);
            var Contact = new ContactPage(DefaultWait);
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
            Home.selectOption("Contact");
            Contact.SendMessage("jose@test.com", "Jose", "QA Test").Should().BeTrue();
        }


        [TestCategory("ProductStore Tests")]
        [TestMethod]//I was expecting this test to fail, the validation for Empty form is missing
        public void SendEmptyMessage()
        {
            var Home = new HomePage(DefaultWait);
            var SignUp = new SignUpPage(DefaultWait);
            var Login = new LoginPage(DefaultWait);
            var Contact = new ContactPage(DefaultWait);
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
            Home.selectOption("Contact");
            Contact.SendMessage("", "", "").Should().BeFalse();
        }
    }
}

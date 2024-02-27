using DemoblazeProject.CommonFunctions;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DemoblazeProject.Framework.CommonFunctions;
using SeleniumExtras.WaitHelpers;
using FluentAssertions;

namespace DemoblazeProject.DemoObjects
{
    public class ContactPage
    {
        private int _defaultWait;

        public ContactPage(int DefaultWait)
        {
            this._defaultWait = DefaultWait;
        }

        public IWebElement ContactEmailInput
        {
            get
            {
                var wait = new WebDriverWait(Driver.Instance, TimeSpan.FromSeconds(_defaultWait));
                return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("recipient-email")));
            }
        }

        public IWebElement ContactNameInput
        {
            get
            {
                var wait = new WebDriverWait(Driver.Instance, TimeSpan.FromSeconds(_defaultWait));
                return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("recipient-name")));
            }
        }

        public IWebElement MessageInput
        {
            get
            {
                var wait = new WebDriverWait(Driver.Instance, TimeSpan.FromSeconds(_defaultWait));
                return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("message-text")));
            }
        }

        public IWebElement SendMessageButton
        {
            get
            {
                var wait = new WebDriverWait(Driver.Instance, TimeSpan.FromSeconds(_defaultWait));
                return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//button[contains(text(),'Send message')]")));
            }
        }

        public bool SendMessage(string email, string name, string message)
        {
            ContactEmailInput.SendKeys(email);
            ContactNameInput.SendKeys(name);
            MessageInput.SendKeys(message);
            SendMessageButton.SafeJsClick();
            try
            {
                ExpectedConditions.AlertIsPresent();
                if (Driver.Instance.SwitchTo().Alert().Text.Contains("Thanks for the message!!"))
                {
                    WebElementExtensions.WaitForSpinningWheel();
                    return true;
                }
            }
            catch (Exception e) { }
            return false;
        }
    }
}

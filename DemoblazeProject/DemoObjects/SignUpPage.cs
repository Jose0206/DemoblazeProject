using System;
using System.Collections.Generic;
using System.Text;
using DemoblazeProject.CommonFunctions;
using DemoblazeProject.Framework.CommonFunctions;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;


namespace DemoblazeProject.DemoObjects
{
    public class SignUpPage
    {
        private int _defaultWait;

        public SignUpPage(int DefaultWait)
        {
            this._defaultWait = DefaultWait;
        }

        public IWebElement UserName
        {
            get
            {
                var wait = new WebDriverWait(Driver.Instance, TimeSpan.FromSeconds(_defaultWait));
                return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("sign-username")));
            }
        }

        public IWebElement Password
        {
            get
            {
                var wait = new WebDriverWait(Driver.Instance, TimeSpan.FromSeconds(_defaultWait));
                return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("sign-password")));
            }
        }

        public IWebElement SignInButton
        {
            get
            {
                var wait = new WebDriverWait(Driver.Instance, TimeSpan.FromSeconds(_defaultWait));
                return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//button[contains(@class, 'btn btn-primary') and contains(text(),'Sign up')]")));
            }
        }

        public bool SignUp(string userName, string password, string? textresult = null)
        {
            UserName.SendKeys(userName);
            Password.SendKeys(password);
            SignInButton.Click();
            try
            {
                ExpectedConditions.AlertIsPresent();
                IAlert alert = Driver.Instance.SwitchTo().Alert();
                string text = alert.Text;
                if (text.Contains(textresult))
                {
                    return true;
                }
            }
            catch (Exception e) { }
            return false;
        }
    }
}
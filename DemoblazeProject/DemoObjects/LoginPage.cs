﻿using System;
using System.Collections.Generic;
using System.Text;
using DemoblazeProject.CommonFunctions;
using DemoblazeProject.Framework.CommonFunctions;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;


namespace DemoblazeProject.DemoObjects
{
    public class LoginPage
    {
        private int _defaultWait;

        public LoginPage(int DefaultWait)
        {
            this._defaultWait = DefaultWait;
        }

        public IWebElement UserName
        {
            get
            {
                var wait = new WebDriverWait(Driver.Instance, TimeSpan.FromSeconds(_defaultWait));
                return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("loginusername")));
            }
        }

        public IWebElement Password
        {
            get
            {
                var wait = new WebDriverWait(Driver.Instance, TimeSpan.FromSeconds(_defaultWait));
                return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("loginpassword")));
            }
        }

        public IWebElement SignInButton
        {
            get
            {
                var wait = new WebDriverWait(Driver.Instance, TimeSpan.FromSeconds(_defaultWait));
                return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//button[contains(@class, 'btn btn-primary') and contains(text(),'Log in')]")));
            }
        }

        public bool SignIn(string userName, string password, string textresult = null)
        {
            UserName.SendKeys(userName);
            //WebElementExtensions.WaitForSpinningWheel();
            Password.SendKeys(password);
            WebElementExtensions.WaitForSpinningWheel();
            SignInButton.SafeJsClick();
            try
            {
                ExpectedConditions.AlertIsPresent();
                if (Driver.Instance.SwitchTo().Alert().Text.Contains(textresult))
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
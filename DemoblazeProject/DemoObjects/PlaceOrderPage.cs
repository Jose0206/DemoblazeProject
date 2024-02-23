using DemoblazeProject.CommonFunctions;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DemoblazeProject.Framework.CommonFunctions;
using FluentAssertions;

namespace DemoblazeProject.DemoObjects
{
    internal class PlaceOrderPage
    {

        private int _defaultWait;

        public PlaceOrderPage(int DefaultWait)
        {
            this._defaultWait = DefaultWait;
        }

        public IWebElement placeOrderLabel
        {
            get
            {
                var wait = new WebDriverWait(Driver.Instance, TimeSpan.FromSeconds(_defaultWait));
                return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("orderModalLabel")));
            }
        }

        public IWebElement nameInput
        {
            get
            {
                var wait = new WebDriverWait(Driver.Instance, TimeSpan.FromSeconds(_defaultWait));
                return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("name")));
            }
        }

        public IWebElement countryInput
        {
            get
            {
                var wait = new WebDriverWait(Driver.Instance, TimeSpan.FromSeconds(_defaultWait));
                return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("country")));
            }
        }

        public IWebElement cityInput
        {
            get
            {
                var wait = new WebDriverWait(Driver.Instance, TimeSpan.FromSeconds(_defaultWait));
                return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("city")));
            }
        }

        public IWebElement creditCardInput
        {
            get
            {
                var wait = new WebDriverWait(Driver.Instance, TimeSpan.FromSeconds(_defaultWait));
                return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("card")));
            }
        }

        public IWebElement monthInput
        {
            get
            {
                var wait = new WebDriverWait(Driver.Instance, TimeSpan.FromSeconds(_defaultWait));
                return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("month")));
            }
        }

        public IWebElement yearInput
        {
            get
            {
                var wait = new WebDriverWait(Driver.Instance, TimeSpan.FromSeconds(_defaultWait));
                return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("year")));
            }
        }

        public IWebElement purchaseButton
        {
            get
            {
                var wait = new WebDriverWait(Driver.Instance, TimeSpan.FromSeconds(_defaultWait));
                return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//button[contains(@class,'btn btn-primary') and contains(text(),'Purchase')]")));
            }
        }

        public IWebElement confirmationModal
        {
            get
            {
                var wait = new WebDriverWait(Driver.Instance, TimeSpan.FromSeconds(_defaultWait));
                return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//div[contains(@class,'sweet-alert  showSweetAlert visible')]//h2")));
            }
        }


        public void placeOrder(string name, string country, string city, string creditCard, string month, string year)
        {
            nameInput.SendKeys(name);
            countryInput.SendKeys(country);
            cityInput.SendKeys(city);
            creditCardInput.SendKeys(creditCard);
            monthInput.SendKeys(month);
            yearInput.SendKeys(year);
            purchaseButton.SafeJsClick();
        }
    }
}

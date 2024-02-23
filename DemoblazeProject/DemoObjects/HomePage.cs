using DemoblazeProject.CommonFunctions;
using DemoblazeProject.Framework.CommonFunctions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoblazeProject.DemoObjects
{
    public class HomePage
    {
        private int _defaultWait;

        public HomePage(int DefaultWait)
        {
            this._defaultWait = DefaultWait;
        }

        public IWebElement productStoreLogo
        {
            get
            {
                var wait = new WebDriverWait(Driver.Instance, TimeSpan.FromSeconds(_defaultWait));
                return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//a[contains(@class,'navbar-brand') and contains(text(),'PRODUCT STORE')]")));
            }
        }

        public IWebElement MainMenu
        {
            get
            {
                var wait = new WebDriverWait(Driver.Instance, TimeSpan.FromSeconds(_defaultWait));
                return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//div[contains(@class,'navbar-collapse') and contains(@id,'navbarExample')]")));
            }
        }

        public IWebElement productsTable
        {
            get
            {
                var wait = new WebDriverWait(Driver.Instance, TimeSpan.FromSeconds(_defaultWait));
                return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//div[contains(@class,'row') and contains(@id,'tbodyid')]")));
            }
        }

        public IWebElement nextButton
        {
            get
            {
                var wait = new WebDriverWait(Driver.Instance, TimeSpan.FromSeconds(_defaultWait));
                return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//button[contains(@id,'next2')]")));
            }
        }

        public bool selectOption(string option)
        {
            var tabs = MainMenu.FindElements(By.TagName("a"));
            foreach (var tab in tabs)
            {
                if (tab.Text.Contains(option))
                {
                    tab.SafeJsClick();
                    WebElementExtensions.WaitForSpinningWheel();
                    return true;
                }
            }
            return false;
        }

        public bool validateOptionDisplayed(string option)
        {
            var tabs = MainMenu.FindElements(By.TagName("a"));
            foreach (var tab in tabs)
            {
                if (tab.Text.Contains(option))
                {
                    return true;
                }
            }
            return false;
        }

        public bool selectProduct(string product)
        {
            var  items = productsTable.FindElements(By.TagName("a"));
            foreach (var item in items)
            {
                if (item.Text.Contains(product))
                {
                    item.SafeJsClick();
                    WebElementExtensions.WaitForSpinningWheel();
                    return true;
                }
            }
            return false;
        }

        public bool validateProductsDisplayed()
        {
            var items = productsTable.FindElements(By.TagName("a"));

                if (items.Count != 0)
                {
                    return true;
                }
            return false;
        }

        public bool validateProductsPagination()
        {
            var items = productsTable.FindElements(By.TagName("a"));

            if (items.Count == 9)
            {
                nextButton.SafeJsClick();
                WebElementExtensions.WaitForSpinningWheel();
            }
            return false;
        }
    }
}

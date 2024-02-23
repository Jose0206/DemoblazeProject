using DemoblazeProject.CommonFunctions;
using DemoblazeProject.Framework.CommonFunctions;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoblazeProject.DemoObjects
{
    internal class CartPage
    {
        private int _defaultWait;

        public CartPage(int DefaultWait)
        {
            this._defaultWait = DefaultWait;
        }

        public IWebElement placeOrderButton
        {
            get
            {
                var wait = new WebDriverWait(Driver.Instance, TimeSpan.FromSeconds(_defaultWait));
                return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//button[contains(@class,'btn btn-success') and contains(text(),'Place Order')]")));
            }
        }

        public IWebElement cartProductTable
        {
            get
            {
                var wait = new WebDriverWait(Driver.Instance, TimeSpan.FromSeconds(_defaultWait));
                return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//tbody[contains(@id,'tbodyid')]")));
            }
        }

        public IWebElement deleteButton
        {
            get
            {
                var wait = new WebDriverWait(Driver.Instance, TimeSpan.FromSeconds(_defaultWait));
                return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//a[contains(text(),'Delete')]")));
            }
        }

        public bool productIncart(string product)
        {
            WebElementExtensions.WaitForSpinningWheel();
            var items = cartProductTable.FindElements(By.TagName("td"));
            foreach (var item in items)
            {
                if (item.Text.Contains(product))
                {
                    return true;
                }
            }
            return false;
        }

        public bool deleteProduct()
        {
            
            deleteButton.SafeJsClick();
            WebElementExtensions.WaitForSpinningWheel();
            var items = cartProductTable.FindElements(By.TagName("td"));
            if (items.Count == 0)
            {
                return true;
            }
            return false;
        }
    }
}

using DemoblazeProject.CommonFunctions;
using DemoblazeProject.Framework.CommonFunctions;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;

namespace DemoblazeProject.DemoObjects
{
    public class CartPage
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

        public IWebElement orderTotalField
        {
            get
            {
                var wait = new WebDriverWait(Driver.Instance, TimeSpan.FromSeconds(_defaultWait));
                return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("totalp")));
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

        public bool validateCartTotal()
        {
            var columnTitles = Driver.Instance.FindElements(By.XPath("//tr//th"));
            int i = 0;
            foreach(var columnTitle in columnTitles)
            {
                if (columnTitle.Text.Contains("Price") == false)
                {
                    i++;
                }
            }
            var prices = Driver.Instance.FindElements(By.XPath("//tr//td["+ i +"]"));
            int total = 0;
            int orderTotal = int.Parse(orderTotalField.Text);
            foreach (var price in prices)
            {
                
                var newValue = price.Text;
                int value = int.Parse(newValue);
                total += value;
                if (total == orderTotal) {
                    return true;
                }
            }
            return false;
        }

        public bool deleteProduct(string product)
        {
            WebElementExtensions.WaitForSpinningWheel();
            var items = cartProductTable.FindElements(By.TagName("td"));
            var productSelected = Driver.Instance.FindElement(By.XPath("//tr//td[contains(text(),'" + product + "')]/following-sibling::td//a"));
            foreach (var item in items)
            {
                if (item.Text.Contains(product))
                {
                    productSelected.SafeJsClick();
                    validateCartTotal().Should().BeTrue();
                    return true;
                }
            }
            return false;
        }
    }
}

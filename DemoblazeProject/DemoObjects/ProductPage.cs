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

namespace DemoblazeProject.DemoObjects
{
    internal class ProductPage
    {
        private int _defaultWait;

        public ProductPage(int DefaultWait)
        {
            this._defaultWait = DefaultWait;
        }

        public IWebElement productNameLabel
        {
            get
            {
                var wait = new WebDriverWait(Driver.Instance, TimeSpan.FromSeconds(_defaultWait));
                return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//h2[contains(@class,'name')]")));
            }
        }

        public IWebElement productPriceLabel
        {
            get
            {
                var wait = new WebDriverWait(Driver.Instance, TimeSpan.FromSeconds(_defaultWait));
                return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//h3[contains(@class,'price-container')]")));
            }
        }

        public IWebElement productImage
        {
            get
            {
                var wait = new WebDriverWait(Driver.Instance, TimeSpan.FromSeconds(_defaultWait));
                return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//img[contains(@src,'imgs/galaxy_s6.jpg')]")));
            }
        }

        public IWebElement AddToCartButton
        {
            get
            {
                var wait = new WebDriverWait(Driver.Instance, TimeSpan.FromSeconds(_defaultWait));
                return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//a[contains(@class,'btn btn-success btn-lg') and contains(text(),'Add to cart')]")));
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

        public bool addProductToCart(string message)
        {
            AddToCartButton.SafeJsClick();
            try
            {
                ExpectedConditions.AlertIsPresent();
                IAlert alert = Driver.Instance.SwitchTo().Alert();
                string text = alert.Text;
                if(message == text)
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                //exception handling
            }
            return false;
        }

        public bool addProductToCartAndValidateInfoInCart()
        {
            productImage.LocateElement();
            var productNameDetail = productNameLabel.Text;
            var productPriceDetail = productPriceLabel.Text.Split()[0];
            AddToCartButton.SafeJsClick();
            homePage.selectOption("Cart");
            WebElementExtensions.WaitForSpinningWheel();
            var items = cartProductTable.FindElements(By.TagName("td"));
            foreach (var item in items)
            {
                if (item.Text.Contains(productNameDetail))
                {
                    foreach (var item2 in items)
                    {
                        if (productPriceDetail.Contains(item2.Text))
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        public HomePage homePage 
        { 
            get
            {
                return new HomePage(_defaultWait);
            } 
        }
    }
}

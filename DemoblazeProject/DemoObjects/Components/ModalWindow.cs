using FluentAssertions;
using DemoblazeProject.CommonFunctions;
using DemoblazeProject.Framework.CommonFunctions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;
using SeleniumExtras.WaitHelpers;

namespace DemoblazeProject.DemoObjects.Components
{
    public class ModalWindow
    {
        private int _defaultWait;
        public ModalWindow(int DefaultWait)
        {
            this._defaultWait = DefaultWait;
        }

        public ConfirmationDialog ConfirmationDialog
        {
            get
            {
                WebElementExtensions.WaitForSpinningWheel();
                var wait = new WebDriverWait(Driver.Instance, TimeSpan.FromSeconds(_defaultWait));
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("confirmDialog")));
                return new ConfirmationDialog(_defaultWait);
            }
        }

        public InformationDialog InformationDialog
        {
            get
            {
                WebElementExtensions.WaitForSpinningWheel();
                var wait = new WebDriverWait(Driver.Instance, TimeSpan.FromSeconds(_defaultWait));
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("informationDialog")));
                return new InformationDialog(_defaultWait);
            }
        }

        public IWebElement Modal
        {
            get
            {
                    WebElementExtensions.WaitForSpinningWheel();
                    var wait = new WebDriverWait(Driver.Instance, TimeSpan.FromSeconds(_defaultWait));
                    return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//div[contains(@role,'dialog') and contains(@class, 'fade in modal')]")));
            }
        }

        public IWebElement ModalContent
        {
            get
            {
                var wait = new WebDriverWait(Driver.Instance, TimeSpan.FromSeconds(_defaultWait));
                return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(Modal.FindElement(By.ClassName("modal-content"))));
            }
        }

        public IWebElement ModalHeader
        {
            get
            {
                var wait = new WebDriverWait(Driver.Instance, TimeSpan.FromSeconds(_defaultWait));
                return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(ModalContent.FindElement(By.ClassName("modal-header"))));
            }
        }

        public IWebElement ModalBody
        {
            get
            {
                var wait = new WebDriverWait(Driver.Instance, TimeSpan.FromSeconds(_defaultWait));
                return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(ModalContent.FindElement(By.ClassName("modal-body"))));
            }
        }

        public IWebElement CloseModalButton
        {
            get
            {
                var wait = new WebDriverWait(Driver.Instance, TimeSpan.FromSeconds(_defaultWait));
                return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(ModalHeader.FindElement(By.CssSelector("div.close.closepop"))));
            }
        }

        public IWebElement OkButton
        {
            get
            {
                var wait = new WebDriverWait(Driver.Instance, TimeSpan.FromSeconds(_defaultWait));
                return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(ModalFooter.FindElement(By.Id("lnkConfirmSubmitC"))));
            }
        }

        public IWebElement ConfirmButton
        {
            get
            {
                var wait = new WebDriverWait(Driver.Instance, TimeSpan.FromSeconds(_defaultWait));
                return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(ModalFooter.FindElement(By.XPath("//button[contains(@mode,'dangerConfirmation')]"))));
            }
        }

        public IWebElement CancelButton
        {
            get
            {
                var wait = new WebDriverWait(Driver.Instance, TimeSpan.FromSeconds(_defaultWait));
                return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(ModalFooter.FindElement(By.Id("lnkConfirmCancelC"))));
            }
        }
        public IWebElement ModalFooter
        {
            get
            {
                var wait = new WebDriverWait(Driver.Instance, TimeSpan.FromSeconds(_defaultWait));
                return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(ModalContent.FindElement(By.ClassName("modal-footer"))));
            }
        }

        public IWebElement DownloadMessage
        {
            get
            {
                var wait = new WebDriverWait(Driver.Instance, TimeSpan.FromSeconds(_defaultWait));
                return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(ModalContent.FindElement(By.XPath("//a[contains(@id,'hrefLink') and contains(text(), 'here')]"))));
            }
        }

        public IWebElement FooterCloseButton
        {
            get
            {
                var wait = new WebDriverWait(Driver.Instance, TimeSpan.FromSeconds(_defaultWait));
                return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(ModalFooter.FindElement(By.Id("lnkCloseWindow"))));
            }
        }

        public IWebElement DownloadLink
        {
            get
            {
                var wait = new WebDriverWait(Driver.Instance, TimeSpan.FromSeconds(_defaultWait));
                return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.LinkText("here")));
            }
        }

        public bool VerifyInformationDialog(string Header, string Body)
        {
            ModalHeader.Text.ToLower().Should().Contain(Header);
            ModalBody.Text.ToLower().Should().Contain(Body);
            CloseModalButton.Click();
            return true;
        }

        public bool VerifyConfirmationDialog(string Header, string Body, string button)
        {
            ModalHeader.Text.ToLower().Should().Contain(Header);
            ModalBody.Text.ToLower().Should().Contain(Body);
            if (button == "ok")
            {
                OkButton.Click();
            }
            if (button == "cancel")
            {
                CancelButton.Click();
            }
            return true;
        }

        public void WaitForProgressBar()
        {
            var counter = 0;
            var progress = "";
            do
            {
                 progress = Driver.Instance.FindElement(By.ClassName("rpbLabel")).Text;
                WebElementExtensions.WaitInSeconds(1);
                counter = counter + 1;
                if (counter >= 120)
                {
                    break;
                }

            } while (progress != "100%" || counter < 120);

        }

        public bool validateAlertResult(string textresult)
        {
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

using FluentAssertions;
using DemoblazeProject.CommonFunctions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoblazeProject.DemoObjects.Components
{
    public class ConfirmationDialog
    {
        private int _defaultWait;
        public ConfirmationDialog(int DefaultWait)
        {
            this._defaultWait = DefaultWait;
        }

        public IWebElement ConfirmDialog
        {
            get
            {
                var wait = new WebDriverWait(Driver.Instance, TimeSpan.FromSeconds(_defaultWait));
                return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("confirmDialog")));
            }
        }

        public IWebElement ModalContent
        {
            get
            {
                var wait = new WebDriverWait(Driver.Instance, TimeSpan.FromSeconds(_defaultWait));
                return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(ConfirmDialog.FindElement(By.ClassName("modal-content"))));
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


        public IWebElement ModalFooter
        {
            get
            {
                var wait = new WebDriverWait(Driver.Instance, TimeSpan.FromSeconds(_defaultWait));
                return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(ModalContent.FindElement(By.ClassName("modal-footer"))));
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

        public IWebElement CancelButton
        {
            get
            {
                var wait = new WebDriverWait(Driver.Instance, TimeSpan.FromSeconds(_defaultWait));
                return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(ModalFooter.FindElement(By.Id("lnkConfirmCancelC"))));
            }
        }

        public bool VerifyConfirmationDialog(string Header, string Body, string button)
        {
            ModalHeader.Text.Should().Contain(Header);
            ModalBody.Text.Should().Contain(Body);
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
    }
}

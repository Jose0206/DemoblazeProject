using FluentAssertions;
using DemoblazeProject.CommonFunctions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoblazeProject.DemoObjects.Components
{
    public class InformationDialog
    {
        private int _defaultWait;
        public InformationDialog(int DefaultWait)
        {
            this._defaultWait = DefaultWait;
        }

        public IWebElement ModalInformationDialog
        {
            get
            {
                var wait = new WebDriverWait(Driver.Instance, TimeSpan.FromSeconds(_defaultWait));
                return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("informationDialog")));
            }
        }

        public IWebElement ModalContent
        {
            get
            {
                var wait = new WebDriverWait(Driver.Instance, TimeSpan.FromSeconds(_defaultWait));
                return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(ModalInformationDialog.FindElement(By.ClassName("modal-content"))));
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
    }
}

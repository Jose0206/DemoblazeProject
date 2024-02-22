using DemoblazeProject.CommonFramework.Utilities;
using DemoblazeProject.CommonFunctions;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.IO;
using System.Reflection;

namespace DemoblazeProject.Framework.CommonFunctions
{
    public static class WebElementExtensions
    {

        public static bool WaitForSpinningWheel()
        {
            var wait = new WebDriverWait(Driver.Instance, TimeSpan.FromSeconds(1));
            try
            {
                wait = new WebDriverWait(Driver.Instance, TimeSpan.FromSeconds(2));
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.ClassName("divProgress")));
            }
            catch
            {
            }
            wait = new WebDriverWait(Driver.Instance, TimeSpan.FromSeconds(30));
            return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.InvisibilityOfElementLocated(By.ClassName("divProgress")));
        }


        public static void WaitForCache(string seconds = null)
        {
            int s = Int32.Parse(seconds);
            if(seconds== null)
            {
                WaitInSeconds(180);
                Driver.Instance.Navigate().Refresh();
            }
            WaitInSeconds(s);
            Driver.Instance.Navigate().Refresh();
        }


        /// <summary>
        /// This will execute a click on the given element using Javascript
        /// </summary>
        /// <param name="element"></param>
        public static void SafeJsClick(this IWebElement webElement)
        {
            if (webElement != null)
            {
                var js = (IJavaScriptExecutor)Driver.Instance;
                js.ExecuteScript("arguments[0].click();", webElement);
            }
        }

        public static void RemoveAttribute(this IWebElement webElement, string attribute)
        {
            if (webElement != null)
            {
                var js = (IJavaScriptExecutor)Driver.Instance;
                js.ExecuteScript("arguments[0].removeAttribute('" + attribute + "')", webElement);
            }
        }

        public static void ActionClick(this IWebElement webElement)
        {
            var actions = new Actions(Driver.Instance);
            actions.MoveToElement(webElement)
            .Click()
            .Build()
            .Perform();
        }

        public static void ActionSafeJsClick(this IWebElement webElement)
        {
            var actions = new Actions(Driver.Instance);
            actions.MoveToElement(webElement)
            .Build()
            .Perform();
            webElement.SafeJsClick();
        }

        public static void LocateElement(this IWebElement webElement)
        {
            var actions = new Actions(Driver.Instance);
            actions.MoveToElement(webElement)
            .Build()
            .Perform();
        }

        public static void SetValueAttribute(this IWebElement webElement, string value)
        {
            var js = (IJavaScriptExecutor)Driver.Instance;
            js.ExecuteScript("arguments[0].value='" + value + "';", webElement);

        }

        public static void SetAttribute(this IWebElement webElement, string attribute, string value)
        {
            var js = (IJavaScriptExecutor)Driver.Instance;
            js.ExecuteScript("arguments[0]." + attribute + "='" + value + "';", webElement);

        }

        public static void EnableUploadOperations()
        {
            var uploadButtons = Driver.Instance.FindElements(By.XPath("//input[@type='file']"));
            foreach (var uploadButton in uploadButtons)
            {
                var js = (IJavaScriptExecutor)Driver.Instance;
                js.ExecuteScript("arguments[0].removeAttribute('class')", uploadButton);
            }
        }

        public static void UploadDocument(this IWebElement fileButton, string Document)
        {
            fileButton.SendKeys(GetFile(Document));
            WaitForSpinningWheel();
        }

        public static void SetTextValue(this IWebElement textbox, string value)
        {
            var wait = new WebDriverWait(Driver.Instance, TimeSpan.FromSeconds(30));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(textbox));
            var visible = textbox.Displayed;
            char clear = '\u0001'; // ASCII code 1 for Ctrl-A
            if (visible == true)
            {
                var actions = new Actions(Driver.Instance);
                actions.MoveToElement(textbox)
                .Click()
                .SendKeys(clear + value)
                .Build()
                .Perform();
            }
        }

        public static bool SearchInTable_click(this IWebElement tableObject, string value)
        {
            var rows = tableObject.FindElements(By.TagName("tr"));


            foreach (var row in rows)
            {
                if (row.Text.Contains(value))
                {
                    row.SafeJsClick();
                    return true;
                }
            }
            return false;
        }

        public static bool FindValueInTableWithAction(this IWebElement tableObject, string value, string action)
        {
            var wait = new WebDriverWait(Driver.Instance, TimeSpan.FromSeconds(30));
            var rows = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.VisibilityOfAllElementsLocatedBy(tableObject.FindElements(By.TagName("tr"))));
            var actions = new Actions(Driver.Instance);

            foreach (var row in rows)
            {
                if (row.Text.Contains(value))
                {
                    actions.MoveToElement(row.FindElement(By.ClassName("dropdown-toggle")))
                    .KeyDown(Keys.Down)
                    .KeyDown(Keys.Down)
                    .KeyDown(Keys.Down)
                    .Build()
                    .Perform();
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(row.FindElement(By.ClassName("dropdown-toggle")))).Click();
                    var menuOptions = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.VisibilityOfAllElementsLocatedBy(row.FindElements(By.TagName("li"))));
                    foreach (var menuOption in menuOptions)
                    {
                        actions.MoveToElement(menuOption)
                       .Build()
                       .Perform();
                        if (menuOption.Text.Contains(action))
                        {
                            menuOption.LocateElement();
                            menuOption.Click();
                            WaitForSpinningWheel();
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public static bool SelectCheckboxValueInTable(this IWebElement tableObject, string value)
        {
            var wait = new WebDriverWait(Driver.Instance, TimeSpan.FromSeconds(30));
            var rows = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.VisibilityOfAllElementsLocatedBy(tableObject.FindElements(By.TagName("tr"))));
            var actions = new Actions(Driver.Instance);

            foreach (var row in rows)
            {
                var rowText = row.Text;
                if (row.Text.Contains(value))
                {
                    var checkbox = row.FindElement(By.TagName("input"));
                    actions.MoveToElement(checkbox)
                    .Click()
                    .Build()
                    .Perform();
                    return true;
                }
            }
            return false;
        }

        public static bool UncheckCheckboxValueInTable(this IWebElement tableObject, string value)
        {
            var wait = new WebDriverWait(Driver.Instance, TimeSpan.FromSeconds(30));
            var rows = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.VisibilityOfAllElementsLocatedBy(tableObject.FindElements(By.TagName("tr"))));
            var actions = new Actions(Driver.Instance);

            foreach (var row in rows)
            {
                var rowText = row.Text;
                if (row.Text.Contains(value))
                {
                    var checkbox = row.FindElement(By.TagName("input"));
                    actions.MoveToElement(checkbox)
                    .Build()
                    .Perform();
                    checkbox.SafeJsClick();
                    WaitForSpinningWheel();
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Moves to the DropDown Option and Performs Click operation 
        /// </summary>
        /// <param name="dropdown">DropDown element</param>
        /// <param name="option">DropDown option value</param>
        public static void SelectFromDropdown(this IWebElement dropdown, string option)
        {
            var wait = new WebDriverWait(Driver.Instance, TimeSpan.FromSeconds(30));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(dropdown));
            var visible = dropdown.Displayed;
            if (visible == true)
            {
                var actions = new Actions(Driver.Instance);
                actions.MoveToElement(dropdown)
                .Click()
                //If option null then select first value from dropdown
                .SendKeys(option ?? Keys.Down + Keys.Down)
                .SendKeys(Keys.Enter)
                .Build().Perform();
            }
        }

        public static string ReturnRandomSelectedValue(this IWebElement dropdown, string option = null)
        {
            var wait = new WebDriverWait(Driver.Instance, TimeSpan.FromSeconds(30));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(dropdown));
            var visible = dropdown.Displayed;
            if (visible == true)
            {
                var actions = new Actions(Driver.Instance);
                actions.MoveToElement(dropdown)
                .Click()
                //If option null then select first value from dropdown
                .SendKeys(option ?? Keys.Down)
                .SendKeys(Keys.Enter)
                .Build().Perform();
            }
            return dropdown.GetAttribute("value");
        }


        /// <summary>
        /// Moves to the DropDown Option and Performs Click operation 
        /// </summary>
        /// <param name="dropdown">DropDown element</param>
        /// <param name="option">DropDown option value</param>
        public static void SelectFromDropdownByClick(this IWebElement dropdown, string option)
        {
            var wait = new WebDriverWait(Driver.Instance, TimeSpan.FromSeconds(30));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(dropdown));
            var visible = dropdown.Displayed;
            if (visible == true)
            {
                var actions = new Actions(Driver.Instance);
                actions.MoveToElement(dropdown)
                .Click()
                .Build().Perform();
                var dropdownOptions = dropdown.FindElements(By.TagName("option"));
                foreach (var dropdownOption in dropdownOptions)
                {
                    if (dropdownOption.Text.Contains(option))
                    {
                        actions = new Actions(Driver.Instance);
                        actions.MoveToElement(dropdownOption)
                        .Click()
                        .Build().Perform();
                    }
                }
            }
        }

        public static string GetFile(string fileName)
        {
            return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\TestFiles\\" + fileName;
        }

        public static void WaitInSeconds(int intSeconds)
        {
            Boolean blnNotDone = true;
            DateTime dtCountdownTo = DateTime.Now.AddSeconds(intSeconds);

            //dtCountdownTo.AddSeconds(intSeconds);

            while (blnNotDone)
            {
                if (DateTime.Now > dtCountdownTo)
                    blnNotDone = false;
            }

            blnNotDone = true;
        }

        public static bool ExpandPanel(string panelName)
        {
            var panels = Driver.Instance.FindElements(By.ClassName("panel-heading"));
            foreach (var panel in panels)
            {
                if (panel.Text.Contains(panelName))
                {
                    panel.FindElement(By.TagName("a")).SafeJsClick();
                    WaitForSpinningWheel();
                    return true;
                }

            }
            return false;
        }
    }
}

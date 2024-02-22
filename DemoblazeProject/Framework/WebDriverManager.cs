using DemoblazeProject.CommonFramework.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoblazeProject.CommonFunctions
{
    public class Driver
    {
        public static IWebDriver Instance { get; set; }

        /// <summary>
        /// this method returns the current instance of the selenium web driver. if there is not a current instance
        /// it will create a new instance and pass that back.
        /// </summary>
        /// <returns></returns>
        private static IWebDriver GetDriver(string browserType, string driverDirectory, bool isHeadLess, string mobileEmulator)
        {
            switch (browserType.ToLower())
            {

                case "chrome":
                    var options = new ChromeOptions();
                    options.AddArguments("chrome.switches", "disable-infobars", "enable-automation");
                    options.AddArguments("disable-infobars");
                    options.AddUserProfilePreference("credentials_enable_service", false);
                    options.AddUserProfilePreference("profile.password_manager_enabled", false);
                    options.AddArguments("ignore-certificate-errors");
                    options.AddArguments("disable-web-security");
                    options.AddArguments("allow-insecure-localhost");
                    options.AddArguments("enable-automation");
                    if (isHeadLess)
                    {
                        options.AddArguments("headless", "window-size=1920,1080");
                    }
                    Instance = new ChromeDriver(driverDirectory, options);
                    break;
            }

            return Instance;
        }

       

        public static void Initialize(string browserType, string driverDirectory, bool isHeadLess, string mobileEmulator = "")
        {
            var appSettings = new AppSettings();
            Instance = GetDriver(browserType, driverDirectory, isHeadLess, mobileEmulator);
            Instance.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(appSettings.DefaultWaitTime);
        }
    }
}

using DemoblazeProject.CommonFramework.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;
using DemoblazeProject.CommonFunctions;
using DemoblazeProject.DemoObjects;

namespace DemoblazeProject.CommonFramework
{
    [TestClass]
    public class DemoblazeBaseClass
    {


        private static string cmEnv;
        public static int DefaultWait;
        public const string UserTypeKey = "UserType";
        public const string testPath = "TestUrlPath";

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }
        private TestContext testContextInstance;

        [AssemblyInitialize]
        public static void AssemblyInitialization(TestContext testContext)
        {
            new LaunchSettings();

            cmEnv = Environment.GetEnvironmentVariable("ENVIRONMENT");
            Configuration = new ConfigurationBuilder()
                            .SetBasePath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
                            .AddJsonFile($"config.{cmEnv}.json", optional: true)
                            .AddJsonFile("config.local.json", optional: true)
                            .AddEnvironmentVariables()
                            .Build();
            var appSettings = new AppSettings();
            Configuration.GetSection("AppSettings").Bind(appSettings);

            UserAccountSettingList = new Dictionary<string, UserAccountSettings>(StringComparer.OrdinalIgnoreCase);
            Configuration.GetSection("UserAccountSettings").Bind(UserAccountSettingList);

            AppSettings = appSettings;
            DefaultWait = appSettings.DefaultWaitTime;
        }


        public static IConfiguration Configuration { get; private set; }
        public static AppSettings AppSettings { get; set; }
        public static Dictionary<string, UserAccountSettings> UserAccountSettingList { set; get; }

        [TestInitialize]
        public void Init()
        {

            var Login = new LoginPage(AppSettings.DefaultWaitTime);
            var driverDirectory = string.IsNullOrEmpty(AppSettings.DriverDirectory)
               ? Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)
               : AppSettings.DriverDirectory;

            //Get Test User credentials  
            var userType = TestContext.Properties[UserTypeKey]?.ToString() ?? string.Empty;
            UserAccountSettingList.TryGetValue(userType, out var testUser);
            var userName = testUser?.UserName ?? AppSettings.UserName;
            var password = testUser?.Password ?? AppSettings.Password;

            Driver.Initialize(AppSettings.Browser, driverDirectory, AppSettings.IsHeadLess);
            var url = new UriBuilder(AppSettings.TestURL)
            {
                Path = TestContext.Properties[testPath]?.ToString() ?? string.Empty
            };
            Driver.Instance.Navigate().GoToUrl(AppSettings.TestURL);
            Driver.Instance.Manage().Window.Maximize();
        }

        //[TestCleanup]
        public void Cleanup()
        {
            try
            {
                Driver.Instance.Quit();
            }
            catch 
            {
                Driver.Instance.Close();
            }
            Driver.Instance.Quit();
        }
    }
}

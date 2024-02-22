using System;
using System.Collections.Generic;
using System.Text;

namespace DemoblazeProject.CommonFramework.Utilities
{
    public class AppSettings
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string TestURL { get; set; }
        public string Browser { get; set; }
        public bool IsHeadLess { get; set; }
        public string DriverDirectory { get; set; }
        public int DefaultWaitTime { get; set; }
    }
}


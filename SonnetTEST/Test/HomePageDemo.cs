using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TelerikFramework;
using System.Configuration;

namespace Test
{
    public class HomePageDemo:TelerikBase
    {
        public void NavigateToCreateVendor()
        {
            NavigateToURL(ConfigurationManager.AppSettings["ManageVendors"]);
        }
    }
}

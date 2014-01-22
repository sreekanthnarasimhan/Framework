using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TelerikFramework;
using System.Configuration;


namespace Test
{
    public static class PageDemo
    {
        private static VendorPageTestDemo1 _oVendorPage = new VendorPageTestDemo1();
        private static HomePageDemo _oHomePage = new HomePageDemo();
        public static void InitializeTestData()
        {
            _oVendorPage.TestDataSource.CSVFile(ConfigurationManager.AppSettings["VendorTestDataFilePath"]);
            //_oVendorPage.TestDataSource.SetFilterTo("Req1");
        }
       
          public static VendorPageDemo VendorPage
        {
            get
            {
                return (_oVendorPage);
            }
        }
          public static HomePageDemo HomePage
          {
              get
              {
                  return _oHomePage;
              }
          }
    }
}


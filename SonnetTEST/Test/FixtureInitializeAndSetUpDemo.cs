using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TelerikFramework;
using SFOA_Automation.pages;
using System.Configuration;

namespace SFOA_Automation
{
    public class FixtureInitializeAndSetUpDemo : TelerikBase
    {
        #region TestInitialize methods
        public void TestInitialize()
        {
            if (IsBrowserClosed)
            {
                base.SetUpTestEnvironment();
                PageDemo.InitializeTestData();
            }
        }
        #endregion
        #region TestCleanUp methods
        public void TestCleanUp_General(string _errFileName)
        {
            string errFileName = GetScreenSnapshot(_errFileName);
        }
        #endregion

        #region ClassCleanUp Method
        public static void ClassCleanUp_General()
        {
            CloseBrowser();
        }
        #endregion


    }
}

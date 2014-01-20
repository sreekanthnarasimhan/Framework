using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeleniumWebDriver2;

namespace Test
{   
    [TestClass]
    public class SeleniumKendoUiTest
    {
        public Microsoft.VisualStudio.TestTools.UnitTesting.TestContext testContextInstance;
        public static SeleniumBase browser = new SeleniumBase();
        /// <summary>
        /// Gets or sets the test context which provides
        /// information about and functionality for the current test run.
        /// </summary>
        public Microsoft.VisualStudio.TestTools.UnitTesting.TestContext TestContext
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

        [ClassInitialize]
        public static void SetUp(TestContext testContext)
        {


        }

        [TestCleanup]
        public void TestCleanup()
        {
            if (testContextInstance.CurrentTestOutcome != UnitTestOutcome.Passed)
            {
                testContextInstance.AddResultFile(browser.GetScreenSnapshot(testContextInstance.TestName));
                ClassCleanup();
            }
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            browser.CloseBrowser();

        }
        [TestMethod]
        [Priority(2)]
        public void BasicComboBoxSelenium()
        {
            browser.LaunchFirefox("http://demos.kendoui.com/web/combobox/index.html");
             
            SeleniumElement searchCriteria = new SeleniumElement(browser);

            
            //To select color
            searchCriteria.ExecuteJS("$(\"span[aria-controls='fabric_listbox']\").click()");
            searchCriteria.ExecuteJS("$(\"ul#fabric_listbox > li \").filter(function(){return this.innerHTML == 'Cotton/Polyester';}).click()");
            

        }
        [TestMethod]
        [Priority(1)]
        public void BasicDropDownSelenium()
        {
            browser.NavigateToURL("http://demos.kendoui.com/web/numerictextbox/index.html");

            SeleniumElement searchCriteria = new SeleniumElement(browser);

            //To input text
            searchCriteria.ExecuteJS("$(\"span[aria-controls='fabric_listbox']\").click()");
            searchCriteria.ExecuteJS("$(\"ul#fabric_listbox > li \").filter(function(){return this.innerHTML == 'Cotton/Polyester';}).click()");


        }

    }
}

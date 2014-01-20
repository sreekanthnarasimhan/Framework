using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeleniumWebDriver2;

namespace Test
{
    [TestClass]
    public class CrystalSkiSeleniumTest
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
        [TestInitialize]
        public void TestInitialize()
        {
            if (browser.IsBrowserClosed())
                browser.LaunchFirefox("http://www.crystalski.co.uk/");
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
        public void SearchPageSelenium()
        {
            

            SeleniumElement searchCriteria = new SeleniumElement(browser);
            searchCriteria.TestDataSource.CSVFile(@"C:\Documents and Settings\vishal.dd\My Documents\Visual Studio 2010\Projects\SonnetTEST\TestData\Crystalkey_TestData.csv");
            searchCriteria.TestDataSource.SetDataRow(0);
            searchCriteria.TestDataSource.EnableTestData();

            //To perform search 
            searchCriteria.FindElement(LocateBy.Name, "calendarPanel:dayOfMonth").SetValue("startdate");
            searchCriteria.FindElement(LocateBy.Name, "calendarPanel:monthYear").SetValue("MonthYear");
            searchCriteria.FindElement(LocateBy.Name, "nights").SetValue("duration");
            searchCriteria.FindElement(LocateBy.Name, "countriesAndResortGroups").SetValue("Destination");
            searchCriteria.FindElement(LocateBy.Name, "resorts").SetValue("Resort");
            searchCriteria.FindElement(LocateBy.Name, "accommodations").SetValue("accommodation");
            searchCriteria.FindElement(LocateBy.Name, "paxPanel:paxPanelForm:adults").SetValue("adults");
            searchCriteria.FindElement(LocateBy.Name, "searchButton").Click();

            //To search for a partular hotel
            searchCriteria.FindElements(LocateBy.CssSelector, "div.searchResultsList > div.insideSearchResultsLists > ul > li").FindChildElement("div.accommHeaderHolder > h2", "SelectResortatttype").Find("div.bookNow >  p  > a").Click();
            //To verify partly price
            searchCriteria.FindElement(LocateBy.CssSelector, "li.partyPrice > p > span.number").AssertEqual(AttributeBy.TextContent, "CheckParyPrice", "expected not equal to actual");

        }
        

    }
}

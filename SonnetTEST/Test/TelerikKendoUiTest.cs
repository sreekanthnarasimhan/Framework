using Microsoft.VisualStudio.TestTools.UnitTesting;
using TelerikFramework;
using ArtOfTest.WebAii.ObjectModel;
using ArtOfTest.WebAii.TestTemplates;
using ArtOfTest.WebAii.Controls.HtmlControls;
using Telerik.TestingFramework.Controls.KendoUI;
using SonnetTESTLib;

namespace Test
{
    [TestClass]
    public class TelerikKendoUiTest
    {
        public Microsoft.VisualStudio.TestTools.UnitTesting.TestContext testContextInstance;
        public static TelerikBase browser = new TelerikBase();

        
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
            if (browser.IsBrowserClosed)
                browser.LaunchInternetExplorer("http://demos.kendoui.com/web/dropdownlist/index.html");
        }

        [TestCleanup]
        public void TestCleanup()
        {
            if (testContextInstance.CurrentTestOutcome != UnitTestOutcome.Passed)
            {
                string errFileName = browser.GetScreenSnapshot(testContextInstance.TestName);
                testContextInstance.AddResultFile(errFileName);
                ClassCleanup();
            }
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            browser.CloseBrowser();

        }

        
        [TestMethod]
       
        public void BasicDropDownTelerik()
        {
           // browser.LaunchInternetExplorer("http://demos.kendoui.com/web/dropdownlist/index.html");
            
            TelerikElements searchCriteria = new TelerikElements(browser);
            searchCriteria.TestDataSource.CSVFile(@"C:\Documents and Settings\vishal.dd\My Documents\Visual Studio 2010\Projects\SonnetTEST\TestData\TestData.csv");
            searchCriteria.TestDataSource.SetDataRow(0);
            searchCriteria.TestDataSource.EnableTestData();

            searchCriteria.FindElement<HtmlAnchor>(LocateBy.Content, "DropDownList").Click();
           
            //To select color
            searchCriteria.FindElement<HtmlControl>(LocateBy.Expression, "aria-activedescendant=color_option_selected").Click();
            //to Assert
            searchCriteria.FindElement<HtmlUnorderedList>(LocateBy.Id, "color_listbox").FindChildElement<HtmlListItem>(LocateBy.Content, "Grey").assertEqual(AttributeBy.Text, "visahl");

            searchCriteria.FindElement<HtmlUnorderedList>(LocateBy.Id, "color_listbox").FindChildElement<HtmlListItem>(LocateBy.Content, "Grey").Click();
            

            //To select size
            searchCriteria.FindElement<HtmlSpan>(LocateBy.Attribute, "aria-activedescendant=size_option_selected").Click();
            searchCriteria.FindElement<HtmlUnorderedList>(LocateBy.Id, "size_listbox").SelectValueInList("size");

            

            //To click on button
            searchCriteria.FindElement<HtmlButton>(LocateBy.Content, "Customize").Click();

            }

        [TestMethod]
        
        public void TestBoxTelerik()
        {
          //  browser.NavigateToURL("http://demos.kendoui.com/web/numerictextbox/index.html");
          //  browser.LaunchInternetExplorer("http://demos.kendoui.com/web/numerictextbox/index.html");
            TelerikElements searchCriteria = new TelerikElements(browser);
            searchCriteria.TestDataSource.CSVFile(@"C:\Documents and Settings\vishal.dd\My Documents\Visual Studio 2010\Projects\SonnetTEST\TestData\TestData.csv");
            searchCriteria.TestDataSource.SetDataRow(0);
            searchCriteria.TestDataSource.EnableTestData();

            searchCriteria.FindElement<HtmlAnchor>(LocateBy.Content, "NumericTextBox").Click();  
            //To input text in text box
            searchCriteria.FindElements<HtmlInputControl>(LocateBy.Expression, "class=k-formatted-value k-input").IndexOf(1).MouseClick();
            searchCriteria.FindElement<HtmlInputControl>(LocateBy.Id, "percentage").SetValue("Percentage");

        }
        

    }
}

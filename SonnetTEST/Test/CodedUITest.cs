using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UITesting.HtmlControls;
using CodedUI;

namespace Test
{
    [CodedUITest]
    public class CodedUITest
    {
        public Microsoft.VisualStudio.TestTools.UnitTesting.TestContext testContextInstance;
        private static CodedUIWebBase browser = new CodedUIWebBase();

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
        public static void ClassInitialize(TestContext testContext)
        {


        }

        [TestInitialize]
        public void TestInitialize()
        {
            if (browser.isBrowserClosed)
                browser.InvokeApp("ie", "http://www.google.com");
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
            browser.CloseWindow();
        }

        [TestMethod]
        public void GoogleSearch()
        {
            CodedUIWebElements webPage = new CodedUIWebElements(browser);

            webPage.TestDataSource.CSVFile("e:\\work\\testdata.csv");
            webPage.TestDataSource.SetFilterTo("test1");

            webPage.FindElement<HtmlEdit>("Name", "q").SetValue("searchcriteria");
            webPage.FindElement<HtmlButton>("Name", "btnK").Click();
        }

        [TestMethod]
        public void GoogleSearch1()
        {
            CodedUIWebElements webPage = new CodedUIWebElements(browser);

            webPage.TestDataSource.CSVFile("e:\\work\\testdata.csv");
            webPage.TestDataSource.SetFilterTo("test2");
            webPage.FindElement<HtmlEdit>("Name", "q").SetValue("searchcriteria");
            webPage.FindElement<HtmlButton>("Name", "btnK").Click();
        }
    }
}
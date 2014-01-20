using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SFOA_Automation.pages;
using SFOA_Automation;
using TelerikFramework;
using ArtOfTest.WebAii.Controls.HtmlControls;
using Telerik.TestingFramework.Controls.KendoUI;


namespace Test
{
    [TestClass]
    public class VendorPageTestDemo1 : SFOA_Automation.FixtureInitializeAndSetUpDemo
    {
        #region TestContext Handling
        public Microsoft.VisualStudio.TestTools.UnitTesting.TestContext testContextInstance;

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
        #endregion
        [ClassInitialize]
        public static void SetUp1(TestContext testContext)
        {

        }
        [TestInitialize]
        public void TestInitialize1()
        {
            base.TestInitialize();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            if (testContextInstance.CurrentTestOutcome != UnitTestOutcome.Passed)
            {
                TestCleanUp_General(testContextInstance.TestName);
                testContextInstance.AddResultFile(testContextInstance.TestName);
                ClassCleanup();
            }
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            ClassCleanUp_General();

        }
        #region public Methods

        /// <summary>
        /// Create New Vendor
        /// </summary>
        [TestMethod]
        [Description("Create a New Vendor ")]
        [Priority(1)]
        public void CreateNewVendor()
        {
            PageDemo.VendorPage.TestDataSource.SetFilterTo("Req1");
            //PageDemo.VendorPage.ManageVendors.Click();
            PageDemo.HomePage.NavigateToCreateVendor();
            PageDemo.VendorPage.CreateNewVendors();
        }

        [TestMethod]
        [Description("Add Vendor information")]
        [Priority(1)]
        public void ProcessingTab()
        {
            PageDemo.VendorPage.TestDataSource.SetFilterTo("Req1");
            PageDemo.HomePage.NavigateToCreateVendor();
            PageDemo.VendorPage.AddVendorProcessingInfo();
        }
        [TestMethod]
        [Description("Add Vendor information")]
        [Priority(1)]
        public void InvoiceTab()
        {
            PageDemo.VendorPage.TestDataSource.SetFilterTo("Req1");
            PageDemo.HomePage.NavigateToCreateVendor();
            PageDemo.VendorPage.AddVendorInvoiceInfo();
        }

        #endregion
    }
}

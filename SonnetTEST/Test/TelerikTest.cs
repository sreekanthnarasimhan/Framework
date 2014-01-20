using Microsoft.VisualStudio.TestTools.UnitTesting;
using TelerikFramework;
using ArtOfTest.WebAii.Controls.HtmlControls;
using Telerik.TestingFramework.Controls.KendoUI;

namespace Test
{
    [TestClass]
    public class TelerikTest
    {
        TelerikBase wDriver = new TelerikBase();

        [TestMethod]
        public void GridEdit()
        {
            wDriver.LaunchInternetExplorer("http://demos.kendoui.com/web/grid/editing.html");
            TelerikElements searchCriteria = new TelerikElements(wDriver);
            searchCriteria.FindElement<KendoGrid>(LocateBy.Id, "grid").SelectCell(1, 2).Click();

        }

        [TestMethod]
        public void TestTelerik()
        {
            wDriver.LaunchChorme("http://google.co.in");

            TelerikElements searchCriteria = new TelerikElements(wDriver);
            searchCriteria.FindElement<HtmlInputControl>(LocateBy.Name, "q, btnG").SetValue("Telerik Testing Framework");

            string abc = searchCriteria.FindElement<HtmlInputControl>(LocateBy.Name, "q").GetValue<string>(AttributeBy.Name);
            int txtsize = searchCriteria.FindElement<HtmlInputControl>(LocateBy.Name, "q").GetValue<int>(AttributeBy.Size);
            string abc1 = searchCriteria.FindElement<HtmlInputControl>(LocateBy.Name, "q").GetValue<string>(AttributeBy.Class);

            HtmlInputControl searchTextBox = searchCriteria.FindElement<HtmlInputControl>(LocateBy.Name, "q").GetElement;
            HtmlButton searchButton = searchCriteria.FindElement<HtmlButton>(LocateBy.Name, "btnG").GetElement;

            searchCriteria.FindElement<HtmlButton>(LocateBy.Name, "btnG").Click();
        }
    }
}

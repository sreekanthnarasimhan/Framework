using Microsoft.VisualStudio.TestTools.UnitTesting;
using TelerikFramework;
using ArtOfTest.WebAii.ObjectModel;
using ArtOfTest.WebAii.TestTemplates;

namespace Test
{
    [TestClass]
    public class KendoUITest
    {
        TelerikBase wDriver = new TelerikBase();

        [TestMethod]
        public void BasicDropDown()
        {
            wDriver.LaunchChorme("http://demos.kendoui.com/web/dropdownlist/index.html");

            //TelerikElement SelectCapColor1 = new TelerikElement(wDriver);
            //SelectCapColor1.SelectValue(LocateBy.Id, "color_listbox", "Black");                  

            //TelerikElement SelectCapColor = new TelerikElement(wDriver);
            //SelectCapColor.FindWebElement(LocateBy.Id, "color_listbox").SelectVisibleText("Grey");

            //TelerikElement SelectCapSize = new TelerikElement(wDriver);
            //SelectCapColor.FindWebElement(LocateBy.Id, "size_listbox").SelectVisibleText("L - 7 1/8\"");

            //TelerikElement ClickCustomize = new TelerikElement(wDriver);
            //ClickCustomize.FindWebElement(LocateBy.Id, "get").Click(); 
        }

    }
}

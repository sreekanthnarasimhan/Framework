using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeleniumWebDriver2;

namespace Test
{
    [TestClass]
    public class SeleniumTest
    {
        SeleniumBase wDriver = new SeleniumBase();

        [TestMethod]
        public void MyTest()
        {
            wDriver.LaunchFirefox("http://google.co.in");

            SeleniumElement searchCriteria = new SeleniumElement(wDriver);
            searchCriteria.FindElement(LocateBy.Name, "q").SetValue("selenium webdriver");

            SeleniumElement search = new SeleniumElement(wDriver);
            searchCriteria.FindElement(LocateBy.Name, "btnG").Click();
        }
    }
}

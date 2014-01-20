using System;
using OpenQA.Selenium;
using System.Collections.Generic;
using SonnetTESTLib;

namespace SeleniumWebDriver2
{
    public class SeleniumElement
    {
        private IWebElement _webElement = null;
        private IList<IWebElement> _webElements = null;
        private IWebDriver _webDriver = null;
        private IJavaScriptExecutor _javaScriptExecutor;
        private DataSource _dataSource = new DataSource();

        public SeleniumElement(SeleniumBase drv)
        {
            _webDriver = drv.GetInstance;
            _javaScriptExecutor = drv.GetJavaScriptExecutor;
        }

        public SeleniumAction FindElement(LocateBy type, string locator)
        {
            _webElement = _webDriver.FindElement(GetLocator(type, locator));

            return new SeleniumAction(_webElement, _dataSource);
        }

        public SeleniumAction FindElement(String tagname, String attributeName, String attributeValue)
        {
            string jQueryText = "$( \"" + tagname + "[" + attributeName + "='" + attributeValue + "']\").get(0);";

            return new SeleniumAction(_javaScriptExecutor.ExecuteScript(jQueryText) as IWebElement, _dataSource);
        }

        public void ExecuteJS(String JQueryLocator)
        {
            _javaScriptExecutor.ExecuteScript(JQueryLocator);
        }

        public SeleniumAction FindElements(LocateBy type, string locator)
        {
            _webElements = _webDriver.FindElements(GetLocator(type, locator));

            return new SeleniumAction(_webElements, _dataSource);
        }
       

        private By GetLocator(LocateBy type, String locate)
        {
            By locator = null;

            if (LocateBy.Id.Equals(type))
                locator = By.Id(locate);

            else if (LocateBy.Name.Equals(type))
                locator = By.Name(locate);

            else if (LocateBy.CssSelector.Equals(type))
                locator = By.CssSelector(locate);

            else if (LocateBy.ClassName.Equals(type))
                locator = By.ClassName(locate);

            else if (LocateBy.XPath.Equals(type))
                locator = By.XPath(locate);

            else if (LocateBy.LinkText.Equals(type))
                locator = By.LinkText(locate);

            else if (LocateBy.PartialLinkText.Equals(type))
                locator = By.PartialLinkText(locate);

            else if (LocateBy.TagName.Equals(type))
                locator = By.TagName(locate);

            return locator;
        }

        public DataSource TestDataSource
        {
            get
            {
                return _dataSource;
            }
        }
    }

    public enum LocateBy
    {
        Id,
        Name,
        XPath,
        CssSelector,
        ClassName,
        LinkText,
        PartialLinkText,
        TagName
    }
}
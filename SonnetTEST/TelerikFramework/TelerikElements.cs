using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.ObjectModel;
using ArtOfTest.WebAii.TestTemplates;
using System.Collections.Generic;
using System.Collections;
using SonnetTESTLib;
using System.Configuration;

namespace TelerikFramework
{
    public class TelerikElements : BaseTest
    {
        private Manager _Manager;
        private object _WebElement;
        private DataSource _dataSource = new DataSource();
        
        //@vishal
        public TelerikElements()
        {
            _Manager = TelerikBase._Manager;
        }

        public TelerikActions<TestControl> FindElements<TestControl>(LocateBy locateType, string locator) where TestControl : ArtOfTest.WebAii.Controls.Control, new()
        {
            IList WebElements = null;

            if (LocateBy.Expression.Equals(locateType))
                WebElements = _Manager.ActiveBrowser.Find.AllByExpression<TestControl>(locator);

            else if (LocateBy.Xpath.Equals(locateType))
                WebElements = _Manager.ActiveBrowser.Find.AllByXPath<TestControl>(locator);

            else if (LocateBy.Content.Equals(locateType))
                WebElements = _Manager.ActiveBrowser.Find.AllByContent<TestControl>(locator);

            else if (LocateBy.Attribute.Equals(locateType))
                WebElements = _Manager.ActiveBrowser.Find.AllByAttributes<TestControl>(locator);

            else if (LocateBy.TagName.Equals(locateType))
                WebElements = _Manager.ActiveBrowser.Find.AllByTagName<TestControl>(locator);

            // THROW EXCEPTION IF NONE OFTHE CONDITION MATCHES

            return new TelerikActions<TestControl>(_Manager, _dataSource, WebElements);
        }

        public TelerikActions<TestControl> FindElement<TestControl>(LocateBy locateType, string locator) where TestControl : ArtOfTest.WebAii.Controls.Control, new()
        {
            TestControl WebElement = new TestControl();

            if (LocateBy.Id.Equals(locateType))
                WebElement = _Manager.ActiveBrowser.Find.ById<TestControl>(locator);

            else if (LocateBy.Name.Equals(locateType))
                WebElement = _Manager.ActiveBrowser.Find.ByName<TestControl>(locator);

            else if (LocateBy.Expression.Equals(locateType))
                WebElement = _Manager.ActiveBrowser.Find.ByExpression<TestControl>(locator);

            else if (LocateBy.Xpath.Equals(locateType))
                WebElement = _Manager.ActiveBrowser.Find.ByXPath<TestControl>(locator);

            else if (LocateBy.Content.Equals(locateType))
                WebElement = _Manager.ActiveBrowser.Find.ByContent<TestControl>(locator);

            else if (LocateBy.Attribute.Equals(locateType))
                WebElement = _Manager.ActiveBrowser.Find.ByAttributes<TestControl>(locator);

            // THROW EXCEPTION IF NONE OFTHE CONDITION MATCHES

            // LINQ not implemented....

            return new TelerikActions<TestControl>(_Manager, _dataSource, WebElement);
        }

        public TelerikActions<TestControl> FindElement<TestControl>(LocateBy locateType, string[] locators) where TestControl : ArtOfTest.WebAii.Controls.Control, new()
        {
            TestControl WebElement = new TestControl();

            string locator = "";
            for (int iParam = 0; iParam < locators.Length; iParam++)
            {
                locator = locator + locators[iParam];

                if (iParam < locators.Length - 1)
                    locator = locator + ", ";
            }

            WebElement = _Manager.ActiveBrowser.Find.ByAttributes<TestControl>(locator);
            // THROW EXCEPTION IF NONE OFTHE CONDITION MATCHES

            return new TelerikActions<TestControl>(_Manager, _dataSource, WebElement);
        }

        //refer - http://www.telerik.com/automated-testing-tools/support/documentation/user-guide/write-tests-in-code/intermediate-topics/element-identification/finding-page-elements.aspx#FindParam
        public TelerikActions<TestControl> FindElement<TestControl>(LocateBy locateType, string TagName, int index) where TestControl : ArtOfTest.WebAii.Controls.Control, new()
        {
            TestControl WebElement = new TestControl();
            WebElement = _Manager.ActiveBrowser.Find.ByTagIndex<TestControl>(TagName, index);

            return new TelerikActions<TestControl>(_Manager, _dataSource, WebElement);
        }

        //refer - http://www.telerik.com/automated-testing-tools/support/documentation/user-guide/write-tests-in-code/intermediate-topics/element-identification/finding-page-elements.aspx#FindParam
        public TelerikActions<TestControl> FindCustomElement<TestControl>(LocateBy locateType, string locator) where TestControl : ArtOfTest.WebAii.Controls.Control, new()
        {
            TestControl WebElement = _Manager.ActiveBrowser.Find.ByCustom<TestControl>(delegate(TestControl e)
                {
                    if (LocateBy.Id.Equals(locateType) && (e.BaseElement.IdAttributeValue == locator))
                        return true;

                    else if (LocateBy.Name.Equals(locateType) && (e.BaseElement.NameAttributeValue == locator))
                        return true;

                    else
                        return false;
                });

            return new TelerikActions<TestControl>(_Manager, _dataSource, WebElement);
        }

        public TelerikActions<TestControl> FindCustomElements<TestControl>(LocateBy locateType, string locator) where TestControl : ArtOfTest.WebAii.Controls.Control, new()
        {
            IList<TestControl> WebElements = _Manager.ActiveBrowser.Find.AllByCustom<TestControl>(delegate(TestControl e)
            {
                if (LocateBy.Id.Equals(locateType) && (e.BaseElement.IdAttributeValue == locator))
                    return true;

                else if (LocateBy.Name.Equals(locateType) && (e.BaseElement.NameAttributeValue == locator))
                    return true;

                else
                    return false;
            });

            return new TelerikActions<TestControl>(_Manager, _dataSource, WebElements);
        }

        // After performing a mouse click you may need to explicitly wait  
        // for the browser to be ready. You very rarely would need to
        // call this function when using the Actions methods.
        public void WaitUntilReady()
        {
            _Manager.ActiveBrowser.WaitUntilReady();
            //_Manager.ActiveBrowser.Frames.WaitAllUntilReady(); //It waits for all the frames to get loaded
            
        }

        public void WaitForControlRefresh()
        {
            _Manager.ActiveBrowser.WaitForAjax(int.Parse(ConfigurationManager.AppSettings["ITimeToWaitForElement"]));
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
        Attribute,
        Content,
        Custom,
        Expression,
        Id,
        Name,
        TagName,
        Xpath
    }
}

using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using SonnetTESTLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SeleniumWebDriver2
{
    public class SeleniumAction
    {
        private IWebElement _webElement = null;
        private IList<IWebElement> _webElements = null;
        private string _jQueryText = null;
        private DataSource _dataSource;

        public SeleniumAction(IWebElement _wElement, DataSource dataSource)
        {
            _webElement = _wElement;
            _dataSource = dataSource;
        }

        public SeleniumAction(IList<IWebElement> wElements, DataSource dataSource)
        {
            _webElements = wElements;
            _dataSource = dataSource;
        }

        public SeleniumAction IndexOf(int elementIndex)
        {
            return new SeleniumAction(_webElements.ElementAt(elementIndex), _dataSource);
        }

        public void Click()
        {
            _webElement.Click();
        }

        public void SetValue(string inputValue)
        {
            if (_dataSource.isTestDataEnabled)
                inputValue = _dataSource.GetValue(inputValue.ToLower());

            _webElement.SendKeys(inputValue);
        }
        //@vishal
        public string GetValue(AttributeBy propertyName)
        {
            if (AttributeBy.TextContent.Equals(propertyName))
            {
                return (_webElement.Text);
            }
            else
            {
                return (_webElement.GetAttribute(propertyName.ToString()));
            }
        }
        public SeleniumAction FindChildElement(string CSSLocator, string criteria)
        {
            _webElement = null;

            if (_dataSource.isTestDataEnabled)
            {
                criteria = _dataSource.GetValue(criteria);
            }

            foreach (IWebElement webElement in _webElements)
            {
                if (webElement.FindElement(By.CssSelector(CSSLocator)).Text.Trim().Equals(criteria))
                {
                    _webElement = webElement;
                    break;
                }
            }

            return new SeleniumAction(_webElement, _dataSource);
        }

        public SeleniumAction Find(string childElementLocator)
        {
            _webElement = _webElement.FindElement(By.CssSelector(childElementLocator));

            return new SeleniumAction(_webElement, _dataSource);
        }
        public void AssertEqualCollectionCount(int expectedCount)
        {
            Assert.AreEqual(expectedCount, _webElements.Count());
        }
        //@vishal: added the following method
        public void AssertEqual(AttributeBy attributeName, string expectedValue, string validationMessage)
        {
            string attributeValue = null;

            if (AttributeBy.TextContent.Equals(attributeName))
            {
                attributeValue = _webElement.Text;
            }
            else
            {
                attributeValue = _webElement.GetAttribute(attributeName.ToString().ToLower());
            }
            if (_dataSource.isTestDataEnabled)
            {
                expectedValue = _dataSource.GetValue(expectedValue);
            }
            Assert.AreEqual(expectedValue, attributeValue, validationMessage);

        }//@vishal
        public void AssertNotEqual(AttributeBy attributeName, string expectedValue, string validationMessage)
        {
            string attributeValue = null;
            if (AttributeBy.TextContent.Equals(attributeName))
            {
                attributeValue = _webElement.Text;
            }
            else
            {
                attributeValue = _webElement.GetAttribute(attributeName.ToString().ToLower());
            }
            if (_dataSource.isTestDataEnabled)
            {
                expectedValue = _dataSource.GetValue(expectedValue);
            }
            Assert.AreNotEqual<string>(expectedValue, attributeValue, validationMessage);
        }

        public void AssertIsTrue(AttributeBy attributeName, string validationMessage)
        {
            bool attributeValue = bool.Parse(_webElement.GetAttribute(attributeName.ToString().ToLower()));

            Assert.IsTrue(attributeValue, validationMessage);
        }

        public void AssertIsFalse(AttributeBy attributeName, string validationMessage)
        {
            bool attributeValue = bool.Parse(_webElement.GetAttribute(attributeName.ToString().ToLower()));

            Assert.IsFalse(attributeValue, validationMessage);
        }

        public void AssertIsNull(string validationMessage)
        {
            Assert.IsNull(_webElement, validationMessage);
        }

        public void AssertIsNotNull(string validationMessage)
        {
            Assert.IsNotNull(_webElement, validationMessage);
        }
    }

    public enum AttributeBy
    {
        Class,
        Checked,
        Disabled,
        Href,
        Id,
        TextContent,//@vishal
        Name,
        Options,
        Selected,
        SelectedIndex,
        SelectedOption,
        Size,
        TabIndex,
        Text,
        Title,
        Value,
    }
}

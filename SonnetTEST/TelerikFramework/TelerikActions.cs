using ArtOfTest.Common.UnitTesting;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;
using SonnetTESTLib;
using System;
using System.Collections;
using Telerik.TestingFramework.Controls.KendoUI;

namespace TelerikFramework
{
    public class TelerikActions<TestControl> where TestControl : ArtOfTest.WebAii.Controls.Control, new()
    {
        private Manager _Manager;
        private object _WebElement;
        private IList _WebElements;
        private DataSource _dataSource;

        public TelerikActions(Manager Mgr, DataSource dataSrc, object wElement)
        {
            _Manager = Mgr;
            _WebElement = wElement;
            _dataSource = dataSrc;
        }

        public TelerikActions(Manager Mgr, DataSource dataSrc, IList wElements)
        {
            _Manager = Mgr;
            _WebElements = wElements;
            _dataSource = dataSrc;
        }

        public int GetCollectionCount
        {
            get
            {
                return _WebElements.Count;
            }
        }

        public TelerikActions<TestControl> SelectCell(int rowNumber, int colNumber)
        {
            KendoGrid kendoUIElement = (KendoGrid)_WebElement;
            KendoGridDataItem dataItem = kendoUIElement.DataItems[rowNumber];

            return new TelerikActions<TestControl>(_Manager, _dataSource, dataItem.Cells[colNumber]);
        }

        public TelerikActions<TestControl> IndexOf(int elementIndex)
        {  
            return new TelerikActions<TestControl>(_Manager, _dataSource, _WebElements[elementIndex]);//@vishal
        }

        /// <summary>
        /// To set the value
        /// </summary>
        /// <param name="inputValue">
        /// The value to be entered by the user
        /// </param>
        public void SetValue(string inputValue)
        {
            HtmlInputControl webElement = (HtmlInputControl)_WebElement;
            webElement.Wait.ForExists();

            if (_dataSource.isTestDataEnabled)
                inputValue = _dataSource.GetValue(inputValue.ToLower());

            webElement.SetValue("value", inputValue);
        }

        public void Click()
        {
            HtmlControl webControl = (HtmlControl)_WebElement;
            webControl.Wait.ForExists();
            webControl.Click();
        }

        public void MouseClick()
        {
            HtmlControl webControl = (HtmlControl)_WebElement;
            webControl.Wait.ForExists();
            webControl.MouseClick();
        }
        public void Click(bool CloseWindow)
        {
            HtmlControl webControl = (HtmlControl)_WebElement;

            webControl.Wait.ForExists();
            webControl.Click(CloseWindow);
        }

        //click on child element
        public void Click<ChildControlType>(string childElement) where ChildControlType : ArtOfTest.WebAii.Controls.Control, new()
        {
            object ChildControl = null;

            HtmlControl webControl = (HtmlControl)_WebElement;
            ChildControl = webControl.Find.ByContent<ChildControlType>(childElement);

            HtmlControl childWebElement = (HtmlControl)ChildControl;
            childWebElement.Click();
        }

        public TelerikActions<TestControl> FindChildElement<TestChildControl>(LocateBy locateType, string locator) where TestChildControl : ArtOfTest.WebAii.Controls.Control, new()
        {
            TestChildControl WebElement = new TestChildControl();
            HtmlControl ChildElement = (HtmlControl)_WebElement;

            if (LocateBy.Id.Equals(locateType))
                WebElement = ChildElement.Find.ById<TestChildControl>(locator);

            else if (LocateBy.Name.Equals(locateType))
                WebElement = ChildElement.Find.ByName<TestChildControl>(locator);

            else if (LocateBy.Expression.Equals(locateType))
                WebElement = ChildElement.Find.ByExpression<TestChildControl>(locator);

            else if (LocateBy.Attribute.Equals(locateType))
                WebElement = ChildElement.Find.ByAttributes<TestChildControl>(locator);

            else if (LocateBy.Content.Equals(locateType))
                WebElement = ChildElement.Find.ByContent<TestChildControl>(locator);

            return new TelerikActions<TestControl>(_Manager, _dataSource, WebElement);
        }

        public TestControl GetElement
        {
            get
            {
                object ContolType = new TestControl();

                //Type abc = typeof(TestControl);
                //ContolType = abc.GetType();

                //if (abc == typeof(HtmlInputControl))
                //    ContolType = new HtmlInputControl();

                return (TestControl)ContolType;
            }
        }

        public void SelectListValue(string userSelection)
        {
            HtmlControl ParentEleemnt = (HtmlControl)_WebElement;

            if (_dataSource.isTestDataEnabled)
                userSelection = _dataSource.GetValue(userSelection.ToLower());

            HtmlListItem userSelect = ParentEleemnt.Find.ByContent<HtmlListItem>(userSelection);
            userSelect.Wait.ForExists();
            userSelect.Click();
        }

        public void SelectValue(int index)
        {
            HtmlSelect ParentElement = (HtmlSelect)_WebElement;
            ParentElement.Wait.ForExists();
            ParentElement.SelectByIndex(index);
        }

        public void SelectValue(SelectBy select, string valueSelect)
        {
            HtmlSelect ParentElement = (HtmlSelect)_WebElement;
            ParentElement.Wait.ForExists();

            if (_dataSource.isTestDataEnabled)
                valueSelect = _dataSource.GetValue(valueSelect.ToLower());

            if (SelectBy.Text.Equals(select))
                ParentElement.SelectByText(valueSelect);

            else if (SelectBy.Value.Equals(select))
                ParentElement.SelectByValue(valueSelect);

            else if (SelectBy.PartialText.Equals(select))
                ParentElement.SelectByPartialText(valueSelect, true);

            else if (SelectBy.PartialValue.Equals(select))
                ParentElement.SelectByPartialValue(valueSelect, true);
        }

        //Remarks: For usage please refer: http://www.telerik.com/automated-testing-tools/support/documentation/user-guide/write-tests-in-code/intermediate-topics/html-control-suite/html-actions.aspx
        public void MultipleSelect(params HtmlOption[] options)
        {
            HtmlSelect ParentElement = (HtmlSelect)_WebElement;
            ParentElement.Wait.ForExists();
            ParentElement.MultiSelect(options);
        }

        public void MultipleSelectByIndex(params int[] index)
        {
            HtmlSelect ParentElement = (HtmlSelect)_WebElement;
            ParentElement.Wait.ForExists();
            ParentElement.MultiSelectByIndex(index);
        }

        public void MultipleSelectByText(params string[] text)
        {
            HtmlSelect ParentElement = (HtmlSelect)_WebElement;
            ParentElement.Wait.ForExists();
            ParentElement.MultiSelectByText(text);
        }

        public void MultipleSelectByValue(params string[] value)
        {
            HtmlSelect ParentElement = (HtmlSelect)_WebElement;
            ParentElement.Wait.ForExists();
            ParentElement.MultiSelectByText(value);
        }

        public TestControl SelectCell(int index)
        {
            TestControl webElement = (TestControl)_WebElements[index];

            return webElement;
        }

        public dataType GetValue<dataType>(AttributeBy attributeName)
        {
            dataType attributeValue;

            HtmlControl webElement = (HtmlControl)_WebElement;
            attributeValue = webElement.GetValue<dataType>(attributeName.ToString().ToLower());

            return attributeValue;
        }

        public string GetTextContent()
        {
            HtmlContainerControl webElement = (HtmlContainerControl)_WebElement;
            return webElement.TextContent;
        }

        public void AssertEqualCollectionCount(int expectedCount)
        {
            Assert.AreEqual(expectedCount, GetCollectionCount);
        }

        
        public void AssertNotEqual<dataType>(AttributeBy attributeName, dataType expectedValue, string validationMessage)
        {
            dataType attributeValue;

            HtmlControl webElement = (HtmlControl)_WebElement;
            attributeValue = webElement.GetValue<dataType>(attributeName.ToString().ToLower());

            Assert.AreNotEqual<dataType>(expectedValue, attributeValue, validationMessage);
        }

        public void AssertIsTrue(AttributeBy attributeName, string validationMessage)
        {
            bool attributeValue;

            HtmlControl webElement = (HtmlControl)_WebElement;
            attributeValue = webElement.GetValue<bool>(attributeName.ToString().ToLower());

            Assert.IsTrue(attributeValue, validationMessage);
        }

        public void AssertIsFalse(AttributeBy attributeName, string validationMessage)
        {
            bool attributeValue;

            HtmlControl webElement = (HtmlControl)_WebElement;
            attributeValue = webElement.GetValue<bool>(attributeName.ToString().ToLower());

            Assert.IsFalse(attributeValue, validationMessage);
        }

        public void AssertIsNull(string validationMessage)
        {
            HtmlControl webElement = (HtmlControl)_WebElement;
            Assert.IsNull(webElement, validationMessage);
        }

        public void AssertIsNotNull(string validationMessage)
        {
            HtmlControl webElement = (HtmlControl)_WebElement;
            Assert.IsNotNull(webElement, validationMessage);
        }
        //@vishal
        public void assertEqual(AttributeBy assertValue, string expectedValue)
        {
            object ContolType = new TestControl();

            Type type = typeof(TestControl);
            if (_dataSource.isTestDataEnabled)
                expectedValue = _dataSource.GetValue(expectedValue.ToLower());

            if (type.Equals(typeof(HtmlSelect)))
            {
                assertSelect(assertValue, expectedValue);
            }

            else if (type.Equals(typeof(HtmlContainerControl)) || type.Equals(typeof(HtmlListItem)) || type.Equals(typeof(HtmlInputText)) || type.Equals(typeof(HtmlControl))
                || type.Equals(typeof(HtmlDiv)) || type.Equals(typeof(HtmlForm)) || type.Equals(typeof(HtmlInputControl)) || type.Equals(typeof(HtmlInputText)) || type.Equals(typeof(HtmlOrderedList))
                || type.Equals(typeof(HtmlSpan)) || type.Equals(typeof(HtmlAnchor)) || type.Equals(typeof(HtmlOrderedList)) || type.Equals(typeof(HtmlUnorderedList)))
            {
                assertContainerControl(assertValue, expectedValue);
            }

            else if (type.Equals(typeof(HtmlTable)))
            {
                assertTableContent(assertValue, expectedValue);
            }
        }

        private void assertSelect(AttributeBy assertValue, string expectedValue)
        {
            HtmlSelect assertWebElement = (HtmlSelect)_WebElement;

            if (AttributeBy.Text.Equals(assertValue))

                //Asserts if the text of the currently selected item does not match the expected value.
                assertWebElement.AssertSelect().SelectedText(ArtOfTest.Common.StringCompareType.Exact, expectedValue);

            else if (AttributeBy.Value.Equals(assertValue))

                //Asserts if the value of the currently selected item does not match the expected value.
                assertWebElement.AssertSelect().SelectedValue(ArtOfTest.Common.StringCompareType.Exact, expectedValue);
        }

        private void assertContainerControl(AttributeBy assertValue, string expectedValue)
        {
            HtmlContainerControl assertWebElement = (HtmlContainerControl)_WebElement;

            if (AttributeBy.InnerText.Equals(assertValue))
                assertWebElement.AssertContent().InnerText(ArtOfTest.Common.StringCompareType.Contains, expectedValue);
            
            else if (AttributeBy.Text.Equals(assertValue))
                assertWebElement.AssertContent().TextContent(ArtOfTest.Common.StringCompareType.Exact, expectedValue);
        }

        private void assertTableContent(AttributeBy assertValue, string expectedValue)
        {
            HtmlTable assertWebElement = (HtmlTable)_WebElement;
         
            if (AttributeBy.TableInnerText.Equals(assertValue))
                assertWebElement.AssertTable().Contains(ArtOfTest.Common.StringCompareType.Exact, expectedValue);
        }
    }

    public enum AttributeBy
    {
        Class,
        Checked,
        Disabled,
        Href,
        Id,
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
        InnerText,
        TableInnerText

        //Param is not considered as it is handled differently using Attributes...
    }

    public enum SelectBy
    {
        Text,
        Value,
        PartialText,
        PartialValue
    }
}

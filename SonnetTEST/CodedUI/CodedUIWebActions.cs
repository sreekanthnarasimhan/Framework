using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UITesting.HtmlControls;
using SonnetTESTLib;
using System;

namespace CodedUI
{
    public class CodedUIWebActions
    {
        private UITestControl _htmlObject;
        private DataSource _dataSource;

        public CodedUIWebActions(UITestControl htmlObj, DataSource dataSource)
        {
            _htmlObject = htmlObj;
            _dataSource = dataSource;
        }

        public void SetValue(string inputValue)
        {
            _htmlObject.WaitForControlEnabled();

            if (_dataSource.isTestDataEnabled)
                inputValue = _dataSource.GetValue(inputValue.ToLower());

            if (_htmlObject.ControlType.Name.Equals("Edit"))
                _htmlObject.SetProperty(HtmlEdit.PropertyNames.Text, inputValue);

            else if (_htmlObject.ControlType.Name.Equals("ComboBox"))
                _htmlObject.SetProperty(HtmlComboBox.PropertyNames.SelectedItem, inputValue);

            else if (_htmlObject.ControlType.Name.Equals("TextArea"))
                _htmlObject.SetProperty(HtmlTextArea.PropertyNames.Text, inputValue);
        }

        public void Click()
        {
            _htmlObject.WaitForControlEnabled();
            Mouse.Click(_htmlObject);
        }

        public void Submit()
        {
            _htmlObject.WaitForControlEnabled();
            _htmlObject.SetProperty(HtmlEdit.PropertyNames.Text, "{Enter}");
        }

        /// <summary>
        /// select a value from the drop down
        /// </summary>
        /// <param name="inputValue">user selected value or key to extract data from external test data source</param>
        /// <remarks>if test data is enabled (i.e. <c>EnableTestData(), inputValue </c>, is considered as key to extract 
        /// data from external an external source</remarks>
        /// <example>browser.WebComboBox("name", "gender").SelectByValue("Male");</example>
        public void SelectByValue(string inputValue)
        {
            _htmlObject.WaitForControlEnabled();

            if (_dataSource.isTestDataEnabled)
                inputValue = _dataSource.GetValue(inputValue.ToLower());

            _htmlObject.SetProperty(HtmlComboBox.PropertyNames.SelectedItem, inputValue);
        }

        /// <summary>
        /// select a value using its position / index from the drop down
        /// </summary>
        /// <param name="indexValue">Position of the value in the drop down</param>
        /// <remarks>Test data is not considered...</remarks>
        /// <example>browser.WebComboBox("name", "gender").SelectByIndex(1);</example>
        public void SelectByIndex(int indexValue)
        {
            _htmlObject.WaitForControlEnabled();
            _htmlObject.SetProperty(HtmlComboBox.PropertyNames.SelectedIndex, indexValue);
        }

        /// <summary>
        /// Place mouse on an element
        /// </summary>
        /// <remarks>Selected element / object is highlighted by placing mouse over it.  It is helpful while placing on 
        /// the menu item to display sub menu items</remarks>
        /// <example>browser.WebLabel("name", "Travel").MouseOver();</example>
        public void MouseHover()
        {
            _htmlObject.WaitForControlEnabled();
            Mouse.Hover(_htmlObject);
        }

        public string GetValue(PropertyName pName)
        {
            return _htmlObject.GetProperty(pName.ToString()).ToString();

            //string retValue = "";
            //    retValue = _htmlObject.GetProperty(pName.ToString()).ToString();
            //    retValue = _htmlObject.GetProperty("TagName").ToString();
            //    retValue = _htmlObject.GetProperty("InnerText").ToString();
            //    retValue = _htmlObject.GetProperty("SelectedItem").ToString();
            //    retValue = _htmlObject.GetProperty("SelectedIndex").ToString();
            //    retValue = _htmlObject.GetProperty(prop.ToString()).ToString();
        }
    }

    public enum PropertyName
    {
        Id,
        Name,
        Class,
        ClassName,
        ValueAttribute,
        Title,
        Enabled,
        Type,
        TagName,
        HelpText,
        ControlDefinition,
        TagInstance,
        InnerText,
        ControlType,
        Exists,
        HasFocus,
        SelectedIndex,
        SelectedItem,
        SelectedText,
        SelectedValue,
        Text,
        Tag,
        Visible,
        State
    };
}

using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UITesting.HtmlControls;
using SonnetTESTLib;
using System;

namespace CodedUI
{
    public class CodedUIWebElements
    {
        #region Settings
        private Boolean _isPopupActive = false;
        private HtmlControl _htmlControl = new HtmlControl();
        private readXML _locatorBase = new readXML();
        private BrowserWindow _browserWindow, _popupWindow;
        private DataSource _dataSource = new DataSource();
        #endregion

        public CodedUIWebElements(CodedUIWebBase mainWindow)
        {
            _browserWindow = mainWindow.getInstance();
            _locatorBase.OpenXMLFile("locator.xml");
        }

        /// <summary>
        /// Locates the element using object repository maintained externally
        /// </summary>
        /// <param name="SearchType">Full Search or Partial Search</param>
        /// <param name="ElementLocator">element locator</param>
        /// <remarks><c>'elementName'</c> holds the object information maintained externally</remarks>
        //public CodedUIWebActions FindElement(WebElement.Search SearchType, WebElement.ControlType controlType, params string[] elementIdentifier)
        //{
        //    if (controlType.Equals(WebElement.ControlType.Window))
        //    {
        //        _popupWindow = new BrowserWindow();
        //        _popupWindow.Maximized = true;
        //        _isPopupActive = true;
        //    }

        //    UITestControl _htmlObject = SearchProperties(controlType.ToString(), SearchType.Equals(WebElement.Search.PartialMarch), elementIdentifier);

        //    return new CodedUIWebActions(_htmlObject, _dataSource);
        //}

        public CodedUIWebActions FindElement<TControl>(params string[] elementIdentifier) where TControl : UITestControl, new()
        {
            TControl controlType = new TControl();

            if (controlType.ClassName.Equals("Window"))
            {
                _popupWindow = new BrowserWindow();
                _popupWindow.Maximized = true;
                _isPopupActive = true;
            }

            // typeof(T).Namespace.Equals("CUITe.Controls.SilverlightControls")


            UITestControl _htmlObject = SearchProperties(controlType.ClassName, false, elementIdentifier);

            return new CodedUIWebActions(_htmlObject, _dataSource);
        }


        private BrowserWindow getWindow()
        {
            BrowserWindow ActiveWindow = _browserWindow;

            if (_isPopupActive)
                ActiveWindow = _popupWindow;

            return ActiveWindow;
        }

        private UITestControl SearchProperties(String controlType, Boolean PartialSearch, params string[] elementIdentifier)
        {
            UITestControl wElement = new UITestControl();

            if (getWindow() != null)
                wElement = new UITestControl(getWindow());

            wElement.TechnologyName = "Web";
            wElement.SearchProperties.Add("ControlType", controlType);

            if ((elementIdentifier.Length % 2) == 1)
                throw new Exception("Parameter does not have right set of key/value pairs - " + elementIdentifier.ToString());

            for (int iProp = 0; iProp < elementIdentifier.Length; iProp += 2)
            {
                String locatorName = _locatorBase.FindElement("/locator/" + controlType + "/element")
                    .FilterByAttribute("key", elementIdentifier.GetValue(iProp).ToString().ToLower().Trim()).GetValue();
                String locatorValue = elementIdentifier.GetValue(iProp + 1).ToString().Trim();

                if (PartialSearch)
                    wElement.SearchProperties.Contains(locatorValue);
                else
                    wElement.SearchProperties.Add(locatorName, locatorValue);
            }

            return wElement;
        }

        public DataSource TestDataSource
        {
            get
            {
                return _dataSource;
            }
        }
    }

    public class WebElement
    {
        public enum Search
        {
            FullMatch,
            PartialMarch
        };

        //public enum ControlType
        //{
        //    Window,
        //    Edit,
        //    Image,
        //    CheckBox,
        //    ComboBox,
        //    Div,
        //    FileInput,
        //    Hyperlink,
        //    Button,
        //    List,
        //    TextArea,
        //    Custom,
        //    Table,
        //    RadioButton,
        //    Cell,
        //    Frame,
        //    Label,
        //    Row
        //};
    }
}

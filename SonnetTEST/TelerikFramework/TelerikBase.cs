using ArtOfTest.WebAii.Core;
using System;
using System.Configuration;
using System.Drawing;

namespace TelerikFramework
{
    public class TelerikBase
    {
        public static Manager _Manager;//@Vishal
        public Manager GetDriver
        {
            get
            {
                return _Manager;
            }
        }
        
        private void LaunchBrowser(string url)
        {
            int elementWaitTime = Convert.ToInt16(ConfigurationManager.AppSettings["ITimeToWaitForElement"]);
            int pageLoadWaitTime = Convert.ToInt16(ConfigurationManager.AppSettings["ITimeToWaitForPageLoad"]);

            _Manager.ActiveBrowser.WaitUntilReady();
            // _Manager.ActiveBrowser.ClearCache(ArtOfTest.WebAii.Core.BrowserCacheType.Cookies);
            _Manager.ActiveBrowser.NavigateTo(url);

            // _Manager.ActiveBrowser.WaitForUrl(url, false, pageLoadWaitTime * 100);
            // _Manager.ActiveBrowser.WaitForAjax(elementWaitTime * 100);
        }

        /// <summary>
        /// Open Firefox
        /// </summary>
        /// <param name="url">application URL</param>
        /// <remarks>Check the Telerik Testing Framework extension in Add-ons Manager. Click Tools > Add-ons (or Ctrl+Shift+A). Ensure the Firefox Http Client is enabled.</remarks>
        public void LaunchFirefox(string url)
        {
            TestSetup.LaunchNewBrowser(BrowserType.FireFox);
            LaunchBrowser(url);
        }

        public void LaunchInternetExplorer(string url)
        {
            TestSetup.LaunchNewBrowser();
            LaunchBrowser(url);
        }

        /// <summary>
        /// Open Google Chrome
        /// </summary>
        /// <param name="url">application URL</param>
        /// <remarks>In case tool is not able to navigate to the URL, 
        ///     Open a Chrome browser window.
        ///     Enter chrome://extensions and press Enter.
        ///     Enable it by clicking Enable.      
        /// </remarks>
        /// <see cref="http://www.telerik.com/automated-testing-tools/support/documentation/user-guide/troubleshooting_guide/test-execution-problems/chrome/cannot-execute-in-chrome.aspx"/>
        public void LaunchChrome(string url)
        {
            TestSetup.LaunchNewBrowser(BrowserType.Chrome, true);
            LaunchBrowser(url);
        }

        public void NavigateToURL(string url)
        {
            _Manager.ActiveBrowser.NavigateTo(url);
        }

        // http://www.telerik.com/automated-testing-tools/support/documentation/user-guide/write-tests-in-code/intermediate-topics/settings-and-configuration/settings-class.aspx
        private Manager TestSetup
        {
            get
            {
                int browserWaitTime = Convert.ToInt16(ConfigurationManager.AppSettings["ITimeToWaitForBrowserStart"]);
                int elementWaitTime = Convert.ToInt16(ConfigurationManager.AppSettings["ITimeToWaitForElement"]);
                int ExecuteDelay = Convert.ToInt16(ConfigurationManager.AppSettings["ITimeToWaitForSearch"]);

                Settings mySettings = new Settings();

                // mySettings.DefaultBrowser = BrowserType.FireFox;
                mySettings.ClientReadyTimeout = browserWaitTime * 1000;

                // amount of execution delay (in milliseconds) between commands
                mySettings.ExecutionDelay = ExecuteDelay * 1000;

                // highlight/annotate the target elements that the requested action is being executed against
                mySettings.AnnotateExecution = true;

                //The amount of time to wait for a response to be received from the browser (in milliseconds) after sending it a command request
                mySettings.ExecuteCommandTimeout = elementWaitTime * 1000;

                //Controls the rate that the condition is tested by the HtmlWait class
                mySettings.WaitCheckInterval = 1000;
                
                // Create the manager object 
                _Manager = new Manager(mySettings);

                // Start the manager 
                _Manager.Start();

                return _Manager;
            }
        }

        //@vishal
        public string GetScreenSnapshot(String errFileName)
        {
            
            Bitmap image = _Manager.ActiveBrowser.Capture();
            image.Save(errFileName, System.Drawing.Imaging.ImageFormat.Bmp);
          //  image.Save(errFileName);

            return errFileName;
        }
        //@vishal
        public void SetUpTestEnvironment()
        {
            string browserType = ConfigurationManager.AppSettings["BrowserType"];
            string baseUrl = ConfigurationManager.AppSettings["BaseUrl"];
            SetupTestEnvironment(browserType, baseUrl);
        }
        //@vishal
        protected void SetupTestEnvironment(string browserType, string baseUrl)
        {
            #region Initialize WebDriver

            switch (browserType)
            {
                case "Firefox":
                    LaunchFirefox(baseUrl);
                    break;

                case "Chrome":
                    LaunchChrome(baseUrl);
                    break;
                
                default:
                    LaunchInternetExplorer(baseUrl);
                    break;
            }
            #endregion
        }
        
        //@vishal "made it static"
        public static void CloseBrowser()
        {
            if (_Manager != null) _Manager.Dispose();
        }

        public bool IsBrowserClosed
        {
            get
            {
                return (_Manager == null);
            }
        }
    }
}

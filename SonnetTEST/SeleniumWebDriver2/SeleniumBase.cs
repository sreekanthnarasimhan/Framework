using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Safari;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Configuration;
using System.Drawing;
using System.IO;

namespace SeleniumWebDriver2
{
    public class SeleniumBase
    {
        private IWebDriver _webDriver;
        private WebDriverWait _pageWait, _elementWait;
        private IJavaScriptExecutor _JSExecute;

        public void LaunchFirefox(string url)
        {
            FirefoxProfile profile = new FirefoxProfile();
            profile.AcceptUntrustedCertificates = true;
            LaunchBrowser(new FirefoxDriver(profile), url);
        }

        public void LaunchChrome(string url)
        {
            LaunchBrowser(new ChromeDriver(ConfigurationManager.AppSettings["ChromeDriver"]), url);
        }

        public void LaunchSafari(string url)
        {
            LaunchBrowser(new SafariDriver(), url);
        }

        private void LaunchBrowser(IWebDriver driver, string url)
        {
            int browserWaitTime = Convert.ToInt16(ConfigurationManager.AppSettings["ITimeToWaitForBrowserStart"]);
            int pageLoadWaitTime = Convert.ToInt16(ConfigurationManager.AppSettings["ITimeToWaitForPageLoad"]);
            int elementWaitTime = Convert.ToInt16(ConfigurationManager.AppSettings["ITimeToWaitForElement"]);
            long waitInterval = Convert.ToInt16(ConfigurationManager.AppSettings["WaitInterval"]);

            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().SetPageLoadTimeout(TimeSpan.FromSeconds(browserWaitTime));
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(elementWaitTime));
            driver.Manage().Timeouts().SetScriptTimeout(TimeSpan.FromSeconds(browserWaitTime));
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(browserWaitTime));

            driver.Navigate().GoToUrl(new Uri(url));
            _webDriver = driver;

            _pageWait = new WebDriverWait(_webDriver, new TimeSpan(0, 0, pageLoadWaitTime));
            _pageWait.PollingInterval = new TimeSpan(waitInterval);

            _elementWait = new WebDriverWait(_webDriver, new TimeSpan(0, 0, elementWaitTime));
            _elementWait.PollingInterval = new TimeSpan(waitInterval);

            _JSExecute = (IJavaScriptExecutor)driver;

            //String jquerytext = System.IO.File.ReadAllText(@"E:\scripts\jquery-2.0.3.js");
            String jquerytext = System.IO.File.ReadAllText(ConfigurationManager.AppSettings["JqueryPath"]);
            _JSExecute.ExecuteScript(jquerytext);
        }

        public bool IsBrowserClosed()
        {
            return (_webDriver == null);
        }

        public void CloseBrowser()
        {
            if (_webDriver != null)
                _webDriver.Dispose();

            _webDriver = null;
        }

        public WebDriverWait PageWait
        {
            get
            {
                return _pageWait;
            }
        }

        public WebDriverWait ElementWait
        {
            get
            {
                return _elementWait;
            }
        }

        public IWebDriver GetInstance
        {
            get
            {
                return _webDriver;
            }
        }

        public IJavaScriptExecutor GetJavaScriptExecutor
        {
            get
            {
                return _JSExecute;
            }
        }

        public string GetScreenSnapshot(String errFileName)
        {
            Image image = null;
            Screenshot screenShot = ((ITakesScreenshot)_webDriver).GetScreenshot();

            string screenshot = screenShot.AsBase64EncodedString;
            byte[] screenshotAsByteArray = screenShot.AsByteArray;

            MemoryStream ms = new MemoryStream(screenshotAsByteArray, 0, screenshotAsByteArray.Length);
            ms.Write(screenshotAsByteArray, 0, screenshotAsByteArray.Length);
            image = Image.FromStream(ms, true);
            image.Save(errFileName + ".Png");

            return errFileName;
        }

        public void NavigateToURL(string url)
        {
            _webDriver.Navigate().GoToUrl(new Uri(url));
        }
    }
}

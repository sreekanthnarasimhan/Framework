using System;
using Microsoft.VisualStudio.TestTools.UITest.Extension;
using Microsoft.VisualStudio.TestTools.UITesting;
using System.Drawing;

namespace CodedUI
{
    /// <summary>
    /// Main class to instantiate as an object to invoke methods under this
    /// </summary>
    /// <remarks>
    /// Used to identify elements within a web page, simulate user actions apart from performing check points, geting attribute value.
    /// It is also interfaced with external entities like test data management, report management etc..
    /// </remarks>
    /// <example><code>CodedUIWeb browser = new CodedUIWeb()</code></example>
    public class CodedUIWebBase
    {
        #region Settings
        private BrowserWindow _browserWindow;
        private Boolean _isPopupActive = false;
        private UITestControlCollection _htmlObjects = new UITestControlCollection();
        #endregion

        #region Methods - main operations
        /// <summary>
        /// Used to invoke web application on a specified browser
        /// </summary>
        /// <param name="browserType">Valid types - IE, FF</param>
        /// <param name="url">Application URL passed as String</param>
        /// <example><code>browser.InvokeApp("IE", "http://google.com");</code></example>
        public void InvokeApp(string browserType, string url)
        {
            _browserWindow = BrowserWindow.Launch(new Uri(url));
            _browserWindow.WaitForControlReady();
        }

        public void NavigateToURL(string url)
        {
            _browserWindow.NavigateToUrl(new Uri(url));
        }

        public string GetScreenSnapshot(String errFileName)
        {
            // Image fileName = _browserWindow.CaptureImage();
            Image fileName = UITestControl.Desktop.CaptureImage();
            fileName.Save(errFileName);

            return errFileName;
        }

        public BrowserWindow getInstance()
        {
            return _browserWindow;
        }

        /// <summary>
        /// To set playBack settings
        /// </summary>
        /// <returns>PlaybackSettings</returns>
        public PlaybackSettings Settings()
        {
            if (Playback.IsInitialized == false)
                Playback.Initialize();

            Playback.PlaybackSettings.ContinueOnError = true;
            // Set delay between actions to 1 second.
            Playback.PlaybackSettings.DelayBetweenActions = 1000;
            Playback.PlaybackSettings.SearchTimeout = 60000;
            // Gets or sets a value indicating whether search has to fail fast or after the timeout period
            Playback.PlaybackSettings.ShouldSearchFailFast = false;
            Playback.PlaybackSettings.SmartMatchOptions = SmartMatchOptions.TopLevelWindow;
            Playback.PlaybackSettings.ThinkTimeMultiplier = 2000;
            Playback.PlaybackSettings.WaitForReadyLevel = WaitForReadyLevel.UIThreadOnly;
            Playback.PlaybackSettings.WaitForReadyTimeout = 60000;

            return Playback.PlaybackSettings;
        }

        /// <summary>
        /// Close the browser
        /// </summary>
        /// <example><code>browser.ExitApp();</code></example>
        public void ExitApp()
        {
            if (_browserWindow != null)
                _browserWindow.Close();
        }

        /// <summary>
        /// Close recently opened window and activate main browser window
        /// </summary>
        /// <example>browser.CloseWindow();</example>
        public void CloseWindow()
        {
            _isPopupActive = false;
        }

        public bool isBrowserClosed
        {
            get
            {
                return (_browserWindow == null);
            }
        }

        /// <summary>
        /// Authenticates the user with the specified user name and password.
        /// </summary>
        /// <param name="userName">The user name.</param>
        /// <param name="password">The password.</param>
        public static void Authenticate(string userName, string password)
        {
            UIWindowsSecurityWindow winTemp2 = new UIWindowsSecurityWindow();
            if (winTemp2.UIUseAnotherAccountText.Exists)
            {
                Mouse.Click(winTemp2.UIUseAnotherAccountText);
            }
            winTemp2.UIUsernameEdit.Text = userName;
            winTemp2.UIPasswordEdit.Text = password;
            Mouse.Click(winTemp2.UIOKButton);
        }
        #endregion
    }
}

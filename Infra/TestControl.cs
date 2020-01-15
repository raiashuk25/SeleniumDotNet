using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.IO;

namespace SeleniumDotNetCoreSample
{
  public  class TestControl
    {
        public IWebDriver Driver = null;
        public String TestName;
        public DateTime TestStartTime;
        public String ScreenShotFileName;
        public Dictionary<String, String> DataObject;
        public WebElementHelper WebElementHelper;
  
        /// <summary>
        /// This is a default Test Controller constructor
        /// </summary>
        public TestControl()
        {
           
        }
        /// <summary>
        /// This method is used to initialize the test resources such as driver,testname,webelement helper, teststarttime and data object
        /// </summary>
        /// <param name="browser"></param>
        /// <param name="testDataObject"></param>
        private void InitializeResources(string browser, Dictionary<string, string> testDataObject)
        {
            TestName = TestContext.CurrentContext.Test.Name;
            Driver = GetWebDriver(WebDriverUtil.GetBrowserType(browser));
            TestStartTime = DateTime.Now;
            this.DataObject = testDataObject;
            this.ScreenShotFileName = "";
            WebElementHelper = new WebElementHelper(Driver);         
            WebElementHelper.NavigateToURL(TestContext.Parameters["URL"]);
        }

        /// <summary>
        /// This method is used to get the webdriver based on the required browser type defined.
        /// </summary>
        /// <param name="browserType"></param>
        /// <returns></returns>
        public IWebDriver GetWebDriver(BrowserType browserType)
        {
            if (Driver == null)
            {
                this.Driver = WebDriverUtil.GetDriver(browserType);
            }
            return Driver;
        }
        /// <summary>
        /// This method is used to instantiate all the test resources for the specified browser.
        /// </summary>
        /// <param name="testDataObject"></param>
        /// <param name="browser"></param>
        /// <returns></returns>
        public TestControl GetTestResources(Dictionary<String, String> testDataObject, string browser)
        {
            InitializeResources(browser, testDataObject);
            GlobalTestController.AddTestControl(this);
            return this;
        }

        /// <summary>
        /// This method is used to instantiate all the test resources for the test
        /// </summary>
        /// <returns TestControl></returns>
        public TestControl GetTestResources(String browser)
        {
            InitializeResources(browser, null);
            GlobalTestController.AddTestControl(this);
            return this;
        }

        /// <summary>
        /// This method is used to get the data from the dictionary object
        /// </summary>
        /// <param name="dataObject"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public String GetData(String key)
        {
            Boolean Result = DataObject.TryGetValue(key, out string value);
            if (!Result)
            {
                throw new NoTestDataException("Test Data Key does not exist " + key);
            }
            return value;
        }
        /// <summary>
        /// This method is used to perform the teardown for any test
        /// </summary>
        public void TearDown()
        {
            Driver.Close();
            Driver.Quit();
            Driver.Dispose();
        }

        /// <summary>
        /// This method captures the screenshot 
        /// </summary>
        /// <param name="tc"></param>
        public void CaptureScreenShot(TestControl tc)
        {
            String TestCaseName = tc.TestName;
            IWebDriver WebDriver = tc.Driver;
            this.ScreenShotFileName=   Path.Combine(new FileUtil().GetScreenShotPath(), TestCaseName + ".png");
            Screenshot Capture = ((ITakesScreenshot)WebDriver).GetScreenshot();
            Capture.SaveAsFile(ScreenShotFileName);
        }

    }


}

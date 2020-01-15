using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.IO;

namespace SeleniumDotNetCoreSample
{
  public  class DriverHelper
    {
        public IWebDriver Driver = null;
        public String TestName;
        public DateTime TestStartTime;
        public String ScreenShotFileName;
        public Dictionary<String, String> DataObject;
        public HtmlControl WebElementHelper;
  
        public DriverHelper()
        {
           
        }

        private void ResourceInit(string browser, Dictionary<string, string> testDataObject)
        {
            TestName = TestContext.CurrentContext.Test.Name;
            Driver = GetWebDriver(WebDriverUtil.GetBrowserType(browser));
            TestStartTime = DateTime.Now;
            this.DataObject = testDataObject;
            this.ScreenShotFileName = "";
            WebElementHelper = new HtmlControl(Driver);         
            WebElementHelper.NavigateToURL(TestContext.Parameters["URL"]);
        }

        public IWebDriver GetWebDriver(BrowserType browserType)
        {
            if (Driver == null)
            {
                this.Driver = WebDriverUtil.GetDriver(browserType);
            }
            return Driver;
        }

        public DriverHelper GetTestResources(String browser)
        {
            ResourceInit(browser, null);
            GlobalTestController.AddTestControl(this);
            return this;
        }

        public void TearDown()
        {
            Driver.Quit();
        }

    }


}

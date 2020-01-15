using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using System;

namespace SeleniumDotNetCoreSample
{
   public  static class WebDriverUtil
    {
       
        public static IWebDriver GetDriver(BrowserType browserType)
        {
            //Based on the configuarion file values specified for the 
            // Execution type as local or Remote corresoinding drivers get called.
            if (TestContext.Parameters["ExecutionType"].ToLower() == "local")
            {
                return GetLocalDriver(browserType);
            }
            else if (TestContext.Parameters["ExecutionType"].ToLower() == "remote")
            {
                string hubUrl = TestContext.Parameters["huburl"];
                return GetRemoteDriver(hubUrl, browserType);
            }
            else
            {
                return GetLocalDriver(browserType);
            }
        }
        
        public  static IWebDriver GetRemoteDriver(string hubUrl, BrowserType browserType)
        {
            IWebDriver driver = null;
            switch (browserType)
            {
                case BrowserType.Chrome:
                    ChromeOptions chromeOptions = new ChromeOptions();
                    driver = new RemoteWebDriver(new Uri(hubUrl), chromeOptions.ToCapabilities(), TimeSpan.FromSeconds(120));
                    break;
                case BrowserType.Firefox:
                    FirefoxOptions firefoxOptions = new FirefoxOptions();
                    driver = new RemoteWebDriver(new Uri(hubUrl), firefoxOptions.ToCapabilities(), TimeSpan.FromSeconds(120));
                    break;
                default:
                    throw new NotImplementedException();
            }
            driver.Manage().Window.Maximize();
            driver.Manage().Cookies.DeleteAllCookies();
            return driver;
        }


        private  static IWebDriver GetLocalDriver(BrowserType browserType)
        {
            IWebDriver driver = null;
            switch (browserType)
            {
                case BrowserType.Chrome:
                    ChromeOptions options = new ChromeOptions();
                    options.AddArguments("--disable-dev-shm-usage");
                    driver = new ChromeDriver(AppDomain.CurrentDomain.BaseDirectory, options, TimeSpan.FromSeconds(120));
                    driver.Manage().Window.Maximize();
                    driver.Manage().Cookies.DeleteAllCookies();
                    break;
                case BrowserType.Firefox:
                    FirefoxOptions firefoxOptions = new FirefoxOptions();
                    driver = new FirefoxDriver(AppDomain.CurrentDomain.BaseDirectory, firefoxOptions, TimeSpan.FromSeconds(120));
                    driver.Manage().Window.Maximize();
                    driver.Manage().Cookies.DeleteAllCookies();
                    break;
                default:
                    throw new InvalidBrowserTypeException("Browser type selected is not valid" + browserType);
            }
            return driver;
        }

        /// <summary>
        /// This method returns the browser type enum
        /// </summary>
        /// <param name="browser"></param>
        /// <returns BrowserType></returns>
        public static BrowserType GetBrowserType(String browser)
        {
            BrowserType browserType;

            switch (browser.ToUpper())
            {
                case "CHROME":
                    browserType = BrowserType.Chrome;
                    break;
                case "IE":
                    browserType = BrowserType.IE;
                    break;
                case "FIREFOX":
                    browserType = BrowserType.Firefox;
                    break;
                default:
                    throw new InvalidBrowserTypeException("Invalid browser selection " + browser);
            }
            return browserType;
        }

    }
}

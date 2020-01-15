using NUnit.Framework;
using OpenQA.Selenium;
using System;

namespace SeleniumDotNetCoreSample
{
    public class BaseTest
    {
        /// <summary>
        /// This methdod is used to do a one time setup before any tests run by configuring the filepath's and creating 
        /// result directory
        /// </summary>
        [OneTimeSetUp]
        public static void ExecutionSetUp()
        {
            //TestConfigurationBuilder.BuildConfiguration();
           // new FileUtil().SetFilePath();
        }
        //  [SetUp]
        public void SetUp()
        {
            //  TODO

        }
        /// <summary>
        /// This is a test tear down that gets called after each test and does the cleanup and reporting
        /// activity for the specific test.
        /// </summary>
        [TearDown]
        public void Teardown()
        {

        }
        /// <summary>
        /// This is a one time tear down and gets triggered once after all the tests are run.
        /// This method is  used for global reporting such as HTML reports/email summary.
        /// </summary>
        [OneTimeTearDown]
        public static void TearDown()
        {
            FileUtil util = new FileUtil();
            util.CreateReport();
        }
    }

}
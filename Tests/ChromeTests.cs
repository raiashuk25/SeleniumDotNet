using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;
[assembly: Parallelizable(ParallelScope.Children)]

namespace SeleniumDotNetCoreSample
{
    [TestFixture]
    class ChromeTests :BaseTest
    {
        public By searchField = By.Id("search_query_top");
        public By seachButton = By.XPath("//button[@name='submit_search']");
        public By alertMessage = By.XPath("//div[@id='center_column']//p[@class='alert alert-warning']");


        [Test]
        [DynamicRetry]
        public void ChromeSearchTShirt1()
        {
            DriverHelper tc = new DriverHelper().GetTestResources("Chrome");
            try
            {
                tc.WebElementHelper.NavigateToURL(TestContext.Parameters["URL"]);
                tc.WebElementHelper.SetText(searchField, Guid.NewGuid().ToString());
                tc.WebElementHelper.Click(seachButton);
                string messageAfterClickingSearch = tc.WebElementHelper.GetText(alertMessage);
                Assert.That(messageAfterClickingSearch, Does.Contain("No results were found for your search"));
            }
            finally
            {
                tc.TearDown();
            }          
        }
        [Test]
        [DynamicRetry]
        public void ChromeSearchTShirt2()
        {
            DriverHelper tc = new DriverHelper().GetTestResources("Chrome");
            try
            {
                tc.WebElementHelper.NavigateToURL(TestContext.Parameters["URL"]);
                tc.WebElementHelper.SetText(searchField, Guid.NewGuid().ToString());
                tc.WebElementHelper.Click(seachButton);
                string messageAfterClickingSearch = tc.WebElementHelper.GetText(alertMessage);
                Assert.That(messageAfterClickingSearch, Does.Contain("No results were found for your search"));
            }
            finally
            {
                tc.TearDown();
            }
        }
        [Test]
        [DynamicRetry]
        public void ChromeSearchTShirt3()
        {
            DriverHelper tc = new DriverHelper().GetTestResources("Chrome");
            try
            {
                tc.WebElementHelper.NavigateToURL(TestContext.Parameters["URL"]);
                tc.WebElementHelper.SetText(searchField, Guid.NewGuid().ToString());
                tc.WebElementHelper.Click(seachButton);
                string messageAfterClickingSearch = tc.WebElementHelper.GetText(alertMessage);
                Assert.That(messageAfterClickingSearch, Does.Contain("No results were found for your search"));
            }
            finally
            {
                tc.TearDown();
            }
        }
        [Test]
        [DynamicRetry]
        public void ChromeSearchTShirt4()
        {
            DriverHelper tc = new DriverHelper().GetTestResources("Chrome");
            try
            {
                tc.WebElementHelper.NavigateToURL(TestContext.Parameters["URL"]);
                tc.WebElementHelper.SetText(searchField, Guid.NewGuid().ToString());
                tc.WebElementHelper.Click(seachButton);
                string messageAfterClickingSearch = tc.WebElementHelper.GetText(alertMessage);
                Assert.That(messageAfterClickingSearch, Does.Contain("No results were found for your search"));
            }
            finally
            {
                tc.TearDown();
            }
        }
        [Test]
        [DynamicRetry]
        public void ChromeSearchTShirt5()
        {
            DriverHelper tc = new DriverHelper().GetTestResources("Chrome");
            try
            {
                tc.WebElementHelper.NavigateToURL(TestContext.Parameters["URL"]);
                tc.WebElementHelper.SetText(searchField, Guid.NewGuid().ToString());
                tc.WebElementHelper.Click(seachButton);
                string messageAfterClickingSearch = tc.WebElementHelper.GetText(alertMessage);
                Assert.That(messageAfterClickingSearch, Does.Contain("No results were found for your search"));
            }
            finally
            {
                tc.TearDown();
            }
        }

    }

}

using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;

namespace SeleniumDotNetCoreSample
{
    [TestFixture]
    class FirefoxTests// : BaseTest
    {
        public By searchField = By.Id("search_query_top");
        public By seachButton = By.XPath("//button[@name='submit_search']");
        public By alertMessage = By.XPath("//div[@id='center_column']//p[@class='alert alert-warning']");


        [Test]
        [DynamicRetry]
        public void FirefoxSearchTShirt1()
        {
            TestControl tc = new TestControl().GetTestResources("Firefox");
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
        public void FirefoxSearchTShirt2()
        {
            TestControl tc = new TestControl().GetTestResources("Firefox");
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
        public void FirefoxSearchTShirt3()
        {
            TestControl tc = new TestControl().GetTestResources("Firefox");
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
        public void FirefoxSearchTShirt4()
        {
            TestControl tc = new TestControl().GetTestResources("Firefox");
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
        public void FirefoxSearchTShirt5()
        {
            TestControl tc = new TestControl().GetTestResources("Firefox");
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

using NUnit.Framework;
using OpenQA.Selenium;
using System;

namespace SeleniumDotNetCoreSample
{
    public class BaseTest
    {

        [OneTimeSetUp]
        public static void ExecutionSetUp()
        {
        }

        [TearDown]
        public void Teardown()
        {

        }

        [OneTimeTearDown]
        public static void TearDown()
        {
        }
    }

}
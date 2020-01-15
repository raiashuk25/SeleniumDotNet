using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;


namespace SeleniumDotNetCoreSample
{

    public class HtmlControl
    {
        IWebDriver Driver;
        public int WebDriverWait = Convert.ToInt32(TestContext.Parameters["DriverWait"]); //TestConfigurationBuilder.frameworkConfiguration.DriverWait;
        /// <summary>
        /// On attemting tocustomize the waits during run time , it is advixed to set the values back to default by calling the 
        /// RestoreDefaultWait method
        /// </summary>
        public int ElementMaxWait, WaitFrequency;
        /// <summary>
        /// This is a parameterized constructor used to set the Driver
        /// </summary>
        /// <param name="Driver"></param>
        public HtmlControl(IWebDriver Driver)
        {
            this.Driver = Driver;
            RestoreDefaultWait();
        }

        /// <summary>
        /// <param name="by"></param>
        /// <returns "targetElement" ></returns>
        public IWebElement FindElement(By by)
        {
            WaitForElement(by);
            return Driver.FindElement(by);               
        }
        /// <summary>
        /// This method finds the element and returns based on the
        /// Displayed status.
        /// <param name="by"></param>
        /// <returns "targetElement" ></returns>
        public IWebElement FindElement(By by, bool verifyDisplayed)
        {
            if (verifyDisplayed)
            {
                return FindElement(by);
            }
            WaitForElementWithoutVerifyingItsEnabledOrDisplayedStatus(by);
            IWebElement targetElement = Driver.FindElement(by);
            return targetElement;
        }
        /// <summary>
        /// This method restores the waits to defaults based on the defined values in the configuration file.
        /// </summary>
        public void RestoreDefaultWait()
        {
            ElementMaxWait = Convert.ToInt32(TestContext.Parameters["MaxWait"]); ;
            WaitFrequency = Convert.ToInt32(TestContext.Parameters["WaitFrequency"]); 
        }

        /// <summary>
        /// This method is used to find element with reference to a webelement
        /// </summary>
        /// <param name="by"></param>
        /// <returns "targetElement" ></returns>
        public IWebElement FindElement(IWebElement element, By by)
        {
            WaitForElement(element.FindElement(by));
            IWebElement targetElement = element.FindElement(by);
            return targetElement;
        }

        /// <summary>
        /// This method delete all the cookies 
        /// </summary>
        public void ClearCookies()
        {
            Driver.Manage().Cookies.DeleteAllCookies();
        }

        /// <summary>
        /// This method clicks on any webelement that is clickable and needs the by element
        /// </summary>
        /// <param name="by"></param>
        public void Click(By by)
        {
            FindElement(by).Click();
        }

        /// <summary>
        /// This method clicks on any webelement that is clickable and needs the by element
        /// </summary>
        ///<param name="element"></param>
        public void Click(IWebElement element)
        {
            element.Click();
        }
        /// <summary>
        /// This method ise used to move focus on that particular elemenet and then perform the click
        /// </summary>
        /// <param name="element"></param>
        public void FocusedClick(IWebElement element)
        {
            new Actions(Driver).MoveToElement(element).Perform();
            element.Click();
        }

        /// <summary>
        /// This method is used to move focus on a particular element and then perform the click
        /// </summary>
        /// <param name="by"></param>
        public void FocusedClick(By by)
        {
            WaitForElementWithoutVerifyingItsEnabledOrDisplayedStatus(by);
            IWebElement targetElement = Driver.FindElement(by);
            FocusedClick(targetElement);
        }

        /// <summary>
        /// This method is used to focus on a particular element
        /// </summary>
        /// <param name="by"></param>
        public void FocusOnElement(By by)
        {
            IWebElement element = FindElement(by);
            new Actions(Driver).MoveToElement(element).Perform();
        }
        /// <summary>
        /// This method is used to focus on a particular element
        /// </summary>
        /// <param name="element"></param>
        public void FocusOnElement(IWebElement element)
        {
            new Actions(Driver).MoveToElement(element).Perform();
        }

        /// <summary>
        /// This method sets the text on the webelemnt
        /// </summary>
        /// <param name="by"></param>
        /// <param name="text"></param>
        public void SetText(By by, string text)
        {
            ClearText(by);
            FindElement(by).SendKeys(text);
        }
        /// <summary>
        /// This method sets the text on the webelement and validates the text with defined number of retries.
        /// </summary>
        /// <param name="by"></param>
        /// <param name="text"></param>
        /// <param name="retries"></param>
        public void SetText(By by, string text, int retries)
        {
            bool flag = false;
            for (int i = 0; i < retries; i++)
            {
                if (GetText(by) != text)
                {
                    ClearText(by);
                    FindElement(by).SendKeys(text);
                }
                else
                {
                    flag = true;
                    return;
                }
            }
            if (!flag)
            {
                throw new TextValidationExpection("Text set in the field " + text + " does not match with the displayed text " + GetText(by));
            }
        }
        /// <summary>
        /// This method sets the text on the webelement and validates the format of the text with with defined number of retries.
        /// </summary>
        /// <param name="by"></param>
        /// <param name="text"></param>
        /// <param name="retries"></param>
        /// <param name="validationText"></param>
        public void SetTextandValidateTextFomrat(By by, string text, int retries, String validationText)
        {
            bool flag = false;
            for (int i = 0; i < retries; i++)
            {
                if (GetText(by) != validationText)
                {
                    ClearText(by);
                    FindElement(by).SendKeys(text);
                }
                else
                {
                    flag = true;
                    return;
                }
            }
            if (!flag)
            {
                throw new TextValidationExpection("Text set in the field " + text + " does not match with the displayed text " + GetText(by));
            }
        }
        /// <summary>
        /// This method selects the valuw from a dropdown list by Index
        /// </summary>
        /// <param name="by"></param>
        /// <param name="index"></param>
        public void SelectByIndex(By by, int index)
        {
            IWebElement targetElement = FindElement(by);
            SelectElement selection = new SelectElement(targetElement);
            selection.SelectByIndex(index);
        }
        /// <summary>
        /// This method selects the value from a dropdown list by text
        /// </summary>
        /// <param name="by"></param>
        /// <param name="value"></param>
        /// <exception cref="DropDownSelectionValueNotFoundException"></exception>
        public void SelectByText(By by, string selectionByText)
        {
            IWebElement targetElement = FindElement(by);
            Boolean flag = false;
            SelectElement selection = new SelectElement(targetElement);
            IList<IWebElement> dropdownOptions = selection.Options;
            if (selection.SelectedOption.ToString() != selectionByText)
            {
                foreach (var element in dropdownOptions)
                {
                    if (element.Text == selectionByText)
                    {
                        selection.SelectByText(selectionByText);
                        flag = true;
                        return;
                    }

                }
                if (!flag)
                {
                    throw new DropDownSelectionValueNotFoundException("Value passed does not exist in the dropdown selection " + selectionByText);
                }

            }

        }

        /// <summary>
        /// This method selects the value from a dropdown list by value
        /// </summary>
        /// <param name="by"></param>
        /// <param name="value"></param>
        /// <exception cref="DropDownSelectionValueNotFoundException"></exception>
        public void SelectByValue(By by, string selectionValue)
        {
            IWebElement targetElement = FindElement(by);
            Boolean flag = false;
            SelectElement selection = new SelectElement(targetElement);
            IList<IWebElement> dropdownOptions = selection.Options;
            if (selection.SelectedOption.ToString() != selectionValue)
            {
                foreach (var element in dropdownOptions)
                {
                    if (element.GetAttribute("value") == selectionValue)
                    {
                        selection.SelectByValue(selectionValue);
                        flag = true;
                        return;
                    }

                }
                if (!flag)
                {
                    throw new DropDownSelectionValueNotFoundException("Value passed does not exist in the dropdown selection " + selectionValue);
                }

            }

        }
        /// <summary>
        /// This method waits for the element for a specified amount of time and return a boolean value
        /// </summary>
        /// <returns boolean></returns>
        /// <param name="by"></param>
        public void WaitForElement(By by)
        {
            int range = ElementMaxWait / WaitFrequency;
            Boolean flag = false;
            for (int i = 0; i <= range; i++)
            {
                try
                {
                    IWebElement element = Driver.FindElement(by);
                    if (element.Displayed && element.Enabled)
                    {
                        flag = true;
                        break;
                    }

                }
                catch (NoSuchElementException)
                {
                    Delay(WaitFrequency);
                }
            }
            if (!flag)
            {
                throw new WaitForElementNotSuccessfulException("Unable to wait for the element" + by);
            }
        }

        /// <summary>
        /// This method waits for the hidden element for a specified amount of time and return a boolean value
        /// without verifying its attributes like displayed or enabled status
        /// </summary>
        /// <returns boolean></returns>
        /// <param name="by"></param>
        public void WaitForElementWithoutVerifyingItsEnabledOrDisplayedStatus(By by)
        {
            int range = ElementMaxWait / WaitFrequency;
            Boolean flag = false;
            for (int i = 0; i <= range; i++)
            {
                try
                {
                    IWebElement element = Driver.FindElement(by);
                    flag = true;
                    break;
                }
                catch (NoSuchElementException)
                {
                    Delay(WaitFrequency);
                }
            }
            if (!flag)
            {
                throw new WaitForElementNotSuccessfulException("Unable to wait for the element" + by);
            }
        }
        /// <summary>
        /// This method waits for the element for a specified amount of time and return a boolean value
        /// </summary>
        /// <returns boolean></returns>
        /// <param name="element"></param>
        public void WaitForElement(IWebElement element)
        {
            int range = ElementMaxWait / WaitFrequency;
            Boolean flag = false;
            for (int i = 0; i < range; i++)
            {
                try
                {
                    if (element.Displayed && element.Enabled)
                    {
                        flag = true;
                        break;
                    }

                }
                catch (NoSuchElementException)
                {
                    Delay(WaitFrequency);
                }
            }
            if (!flag)
            {
                throw new WaitForElementNotSuccessfulException("Unable to wait for the element" + element.ToString());
            }
        }

        /// <summary>
        /// This method is used to introduce a delay
        /// </summary>
        /// <param name="seconds"></param>
        public void Delay(int seconds)
        {
            System.Threading.Thread.Sleep(seconds * 1000);
        }

        /// <summary>
        /// This Method does a special click on elements that cannot be clicked via .
        /// click method and needs special keyboard interaction.
        /// </summary>
        /// <param name="by"></param>
        public void KeyboardClick(By by)
        {
            FindElement(by).SendKeys(Keys.Space);
        }
        /// <summary>
        /// This method gets the text of any element 
        /// </summary>
        /// <param name="by"></param>
        public String GetText(By by)
        {
            return FindElement(by).Text;
        }
        /// <summary>
        /// This method gets the value and attribute for any element for the specific attribute .
        /// </summary>
        /// <param name="by"></param>
        /// <param name="attribute"></param>  
        public String GetAttributeValue(By by, string attribute)
        {
            return FindElement(by).GetAttribute(attribute);
        }
        /// <summary>
        /// This method finds all the elememts with the given By and returns a read only collection.
        /// </summary>
        /// <param name="by"></param>
        /// <returns "IReadOnlyCollection"></returns>
        public IReadOnlyCollection<IWebElement> FindElements(By by)
        {
            WaitForElement(by);
            return Driver.FindElements(by);

        }
        /// <summary>
        /// This method finds all the elememts with the given By and returns a read only collection.
        /// </summary>
        /// <param name="by"></param>
        /// <returns "IReadOnlyCollection"></returns>
        public IReadOnlyCollection<IWebElement> FindElements(IWebElement element, By by)
        {
            WaitForElement(FindElement(element, by));
            return element.FindElements(by);

        }
        /// <summary>
        /// This method waits for the staleness of the element.
        /// </summary>
        /// <param name="by"></param>
        public void WaitForStalenessOfElement(By by)
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(WebDriverWait));
            wait.Until(ExpectedConditions.StalenessOf(Driver.FindElement(by)));
        }
        /// <summary>
        /// This method is used to navigate to the desired URL
        /// </summary>
        /// <param name="url"></param>
        public void NavigateToURL(string url)
        {
            Driver.Navigate().GoToUrl(url);
        }
        /// <summary>
        /// This method is used to clear text from a text field
        /// </summary>
        /// <param name="by"></param>
        public void ClearText(By by)
        {
            FindElement(by).Clear();
        }
        /// <summary>
        /// This method returns if the element is displayed or not
        /// </summary>
        /// <param name="by"></param>
        /// <returns></returns>
        public bool IsDisplayed(By by)
        {
            return FindElement(by).Displayed;

        }
        /// <summary>
        /// This method returns if the element is displayed or not
        /// </summary>
        /// <param name="by"></param>
        /// <returns></returns>
        public bool IsDisplayed(IWebElement element)
        {
            return element.Displayed;
        }
        /// <summary>
        /// This method returns the title of the browser
        /// </summary>
        /// <returns></returns>
        public String GetTitle()
        {
            return Driver.Title;
        }
        /// <summary>
        /// This method is used to wait until the element is not visible in the DOM
        /// </summary>
        /// <param name="waitTime"></param>
        /// <param name="by"></param>
        public void WaitForInvisibilityOfElement(By by)
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(WebDriverWait));
            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(by));
        }
        /// <summary>
        /// This method is used to wait for the presence of an alert
        /// </summary>
        public void WaitForPresenceOfAlert()
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(WebDriverWait));
            wait.Until(ExpectedConditions.AlertIsPresent());
        }
        /// <summary>
        /// This method is used to wait for the existence of an element without any other validations
        /// </summary>
        /// <param name="by"></param>
        public void WaitForExistenceOfElement(By by)
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(WebDriverWait));
            wait.Until(ExpectedConditions.ElementExists(by));
        }
        /// <summary>
        /// This  method is used to switch to alert when it is available 
        /// </summary>
        /// <param name="frameName"></param>
        public void WaitForFrameToBeAvailableAndSwitch(String frameName)
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(WebDriverWait));
            wait.Until(ExpectedConditions.FrameToBeAvailableAndSwitchToIt(frameName));
        }
        /// <summary>
        /// This method is used to switch to Alert
        /// </summary>
        public void SwitchToAlert()
        {
            Driver.SwitchTo().Alert();
        }
        /// <summary>
        /// This method will swtch to Alert and click on OK/Accept button
        /// </summary>
        public void SwitchToAlertAndAccept()
        {
            Driver.SwitchTo().Alert().Accept();
        }
        /// <summary>
        /// This method will switch to alert and click on Dismiss button
        /// </summary>
        public void SwitchToAlertAndDismiss()
        {
            Driver.SwitchTo().Alert().Dismiss();
        }
        /// <summary>
        /// This methhod will switch to the specified window by name
        /// </summary>
        /// <param name="windowName"></param>
        public void SwitchToWindow(String windowName)
        {
            Driver.SwitchTo().Window(windowName);
        }
        /// <summary>
        /// This method will switch back to the default content
        /// </summary>
        public void SwitchToDefault()
        {
            Driver.SwitchTo().DefaultContent();
        }
        /// <summary>
        /// This method is use to wait until the Ajax calls are completed on the 
        /// page by verifying the active jquery calls
        /// </summary>
        public void waitForAjaxRequestsToComplete()
        {
            // Get the java script executor
            IJavaScriptExecutor executor = (IJavaScriptExecutor)Driver;
            //verify the jquery is defined or undefined and bsed on the condition verify the jquery.actve status 
            if ((bool)executor.ExecuteScript("return window.jQuery != undefined"))
            {
                //verify the condition and delay 1 sec each time
                while (!(bool)((IJavaScriptExecutor)Driver).ExecuteScript("return jQuery.active==0"))
                {
                    Delay(1);
                }
            }
        }

        /// <summary>
        /// This method verifies if the element exists in the DOM and retruns a boolean value
        /// </summary>
        /// <param name="by"></param>
        /// <param name="waitFrequency"></param>
        /// <param name="maxWaitTime"></param>
        /// <returns></returns>
        public bool VerifyTheExistenceOfElement(By by, int waitFrequency = 5, int maxWaitTime = 10)
        {
            //Sets the eait time for the element based on user choice
            ElementMaxWait = maxWaitTime;
            WaitFrequency = waitFrequency;
            try
            {
                FindElement(by);
                return true;
            }
            catch (WaitForElementNotSuccessfulException e)
            {
                return false;
            }
            finally
            {
                RestoreDefaultWait();
            }
        }

        /// <summary>
        /// This method returns the items count for a dropdown/list box
        /// </summary>
        /// <param name="dropdownLocator"></param>
        /// <returns></returns>
        public int GetItemCountOfADropDown(By dropdownLocator)
        {
            SelectElement dropDown = new SelectElement(FindElement(dropdownLocator));
            return dropDown.Options.Count;
        }
        /// <summary>
        /// This method is used to get the current selection value in the drop down
        /// </summary>
        /// <param name="by"></param>
        /// <returns></returns>
        public string GetSelectedValueOfDropDown(By by)
        {
            SelectElement selectedElement = new SelectElement(FindElement(by));
            return selectedElement.SelectedOption.Text;
        }

        /// <summary>
        /// This method verifies that drop down item is available in dropdown or not
        /// </summary>
        /// <param name="tc"></param>
        /// <param name="by"></param>
        /// <param name="dropDownItem"></param>
        /// <returns></returns>
        public bool VerifyTheContentOfADropDown(By by, string dropDownItem)
        {
            var list = new List<string>();
            SelectElement selects = new SelectElement(FindElement(by));
            foreach (var select in selects.Options)
            {
                list.Add(select.Text.ToString());
            }
            return (list.Contains(dropDownItem));
        }

        /// <summary>
        /// This method clears the edit field of any form
        /// </summary>
        /// <param name="tc"></param>
        /// <param name="by"></param>
        public void ClearInputField(By by)
        {
            IWebElement element = FindElement(by);
            element.SendKeys(Keys.Control + "a");
            element.SendKeys(Keys.Backspace);
        }

    }
}

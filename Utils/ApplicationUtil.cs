using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;

namespace SeleniumDotNetCoreSample
{
   public static class ApplicationUtil
    {
        /// <summary>
        /// This method is used to click on the actions menu and any link that is specified in any of the portal slick grid.
        /// </summary>
        /// <param name="tc"></param>
        /// <param name="grid"></param>
        /// <param name="actionMenuElement"></param>
        /// <param name="linkText"></param>
        public static void ClickOnLinkUnderActionMenuInPortalApplicationSlickGrid(TestControl tc, By grid, By actionMenuElement, By linkText)
        {
            IReadOnlyCollection<IWebElement> gridDataRecords = tc.WebElementHelper.FindElements(grid);
            foreach (var item in gridDataRecords)
            {
                try
                {
                    IWebElement actionsMenuElement = tc.WebElementHelper.FindElement(item, actionMenuElement);
                    tc.WebElementHelper.FocusedClick(actionsMenuElement);
                    tc.WebElementHelper.Click(linkText);
                    return;
                }
                catch (NoSuchElementException)
                {
                    throw new UnableToPerformRequiredOperationsOnTheGridException("Issues working on the specified Grid. Recheck the identifiers and the data");
                }
            }
        }

        /// <summary>
        /// This is method to verify, how many characters are allowed in a field
        /// </summary>
        /// <param name="tc"></param>
        /// <param name="by"></param>
        /// <param name="numberOfMaxCharacterToVerify"></param>
        /// <returns></returns>
        public static bool VerifyMaxNumberOfCharactersAnInputFieldCanTake(TestControl tc, By by, int numberOfMaxCharacterToVerify)
        {
            IWebElement element = tc.WebElementHelper.FindElement(by);
            var i = 1;
            while (i < (numberOfMaxCharacterToVerify + 10))
            {
                element.SendKeys(DateTime.Now.Millisecond + i.ToString());
                i += 1;
            }
            int possibleMaxNumberOfCharacterInTheInputField = tc.WebElementHelper.GetAttributeValue(by, "value").Length;
            return possibleMaxNumberOfCharacterInTheInputField == numberOfMaxCharacterToVerify;
        }

        /// <summary>
        /// This method verifies that special characters are not allowed in certain field
        /// </summary>
        /// <param name="tc"></param>
        /// <param name="by"></param>
        /// <returns></returns>
        public static bool VerifyUserCantEnterTheSpecialCharacters(TestControl tc, By by)
        {
            tc.WebElementHelper.ClearInputField(by);
            //List of all the special characters that the fileds should not accept
            string[] specialCharacters = new[] { "+", "&", "|", "!", "(", ")", "{", "}", "[", "]", "^", "~", "*", "?", ":", "\\", "\"" };
            for (int i = 0; i < specialCharacters.Length; i++)
            {
                tc.WebElementHelper.SetText(by, specialCharacters[i]);
                if (!string.IsNullOrEmpty(tc.WebElementHelper.GetAttributeValue(by, "value")))
                { return false; }

            }
            return true;
        }

          /// <summary>
        /// Generates the random number and converts in string of a given range
        /// </summary>
        /// <param name="rangeFrom"></param>
        /// <param name="rangeTo"></param>
        /// <returns></returns>
        public static string GetRandomNumberInStringFormat(int rangeFrom, int rangeTo)
        {
            Random random = new Random();
            return random.Next(rangeFrom, rangeTo).ToString();
        }

        /// <summary>
        /// This method generates the random number of given range
        /// </summary>
        /// <param name="rangeFrom"></param>
        /// <param name="rangeTo"></param>
        /// <returns></returns>
        public static int GetRandomNumber(int rangeFrom, int rangeTo)
        {
            Random random = new Random();
            return random.Next(rangeFrom, rangeTo);
        }

        /// <summary>
        /// Gets the next week day date
        /// </summary>
        /// <returns>Returns current date if it is a week day. Else reurns the next week day date</returns>
        public static string GetNextWeekDayDate()
        {
            DateTime today = DateTime.Today;
            int offsetDays = 0;
            switch (today.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    offsetDays = 2;
                    break;
                case DayOfWeek.Saturday:
                    offsetDays = 1;
                    break;
                default:
                    offsetDays = 0;
                    break;
            }
            return today.AddDays(offsetDays).ToString("MM/dd/yyyy");
        }
    }
}

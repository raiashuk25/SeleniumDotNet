using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace SeleniumDotNetCoreSample
{
  public  static class GlobalTestController
    {
        static List<DriverHelper> GlobalControlList = new List<DriverHelper>();
        static List<String> ExecutionLog = new List<String>();


        /// <summary>
        /// This Method adds a test control to the master list by verifying if the test name already exists
        /// </summary>
        /// <param name="tc"></param>
        public static void AddTestControl(DriverHelper tc)
        {
            if (VerifyIfTestControlExistsWithTheTestName(tc, tc.TestName))
            {
                RemoveTestControl(tc,tc.TestName);
            }
            GlobalControlList.Add(tc);
        }

        /// <summary>
        /// This method remove the test control from the master list
        /// </summary>
        /// <param name="tc"></param>
        public static void RemoveTestControl(DriverHelper tc, String testName)
        {
            if(VerifyIfTestControlExistsWithTheTestName(tc,testName)) GlobalControlList.Remove(tc);
        }
        /// <summary>
        /// This method verifes if the global test controller list has the testName specified and returns a flag
        /// </summary>
        /// <param name="tc"></param>
        /// <param name="testName"></param>
        /// <returns></returns>
        private static bool  VerifyIfTestControlExistsWithTheTestName(DriverHelper tc,String testName)
        {
            foreach (var testControl in GlobalControlList)
            {
                if (testControl.TestName == testName)
                {
                    return true;
                }
            }
            return false;
        }
  }
}

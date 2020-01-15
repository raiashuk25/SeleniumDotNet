using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace SeleniumDotNetCoreSample
{
  public  static class GlobalTestController
    {
        static List<TestControl> GlobalControlList = new List<TestControl>();
        static List<String> ExecutionLog = new List<String>();
        /// <summary>
        /// This method is used to get the test control from the master list
        /// </summary>
        /// <param name="testName"></param>
        /// <returns TestControl></returns>
        public static TestControl GetTestControl(string testName)
        {
            TestControl tc = null;
            foreach (var testControl in GlobalControlList)
            {
                if (testControl.TestName == testName)
                {
                    tc = testControl;
                }
            }
            return tc;
        }

        /// <summary>
        /// This Method adds a test control to the master list by verifying if the test name already exists
        /// </summary>
        /// <param name="tc"></param>
        public static void AddTestControl(TestControl tc)
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
        public static void RemoveTestControl(TestControl tc, String testName)
        {
            if(VerifyIfTestControlExistsWithTheTestName(tc,testName)) GlobalControlList.Remove(tc);
        }
        /// <summary>
        /// This method verifes if the global test controller list has the testName specified and returns a flag
        /// </summary>
        /// <param name="tc"></param>
        /// <param name="testName"></param>
        /// <returns></returns>
        private static bool  VerifyIfTestControlExistsWithTheTestName(TestControl tc,String testName)
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
        /// <summary>
        /// This Method adds test log
        /// </summary>
        /// <param name="log"></param>
        public static void AddLog(String log)
        {
            ExecutionLog.Add(log);
        }
        /// <summary>
        /// This method returns the current execution log
        /// </summary>
        /// <returns></returns>
        public static List<String> GetLog()
        {
            return ExecutionLog;
        }

    }
}

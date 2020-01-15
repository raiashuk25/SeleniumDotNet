using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;
using NUnit.Framework.Internal.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeleniumDotNetCoreSample
{
    public class RetryingCommand : DelegatingTestCommand
    {
        int localRetryTimes;
        /// <summary>
        /// This is a parameterized constructor to set the number of retrys and the testcommand
        /// </summary>
        /// <param name="innercommand"></param>
        /// <param name="times"></param>
        public RetryingCommand(TestCommand innercommand, int times) : base(innercommand)
        {
            localRetryTimes = times;
        }
        /// <summary>
        /// Execute method is an override to implement our own logic to run the test,
        /// capture the result and decide if we want to rereun baseed on the constarints defined.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override TestResult Execute(TestExecutionContext context)
        {


            RunTest(context);

            while (TestFailed(context) && localRetryTimes > 0 && context.CurrentResult.ResultState.Label == "Error")
            {
                localRetryTimes = localRetryTimes - 1;
                ClearResults(context);
                RunTest(context);
            }


            return context.CurrentResult;
        }

        /// <summary>
        /// This method is used to validate the result of the execution and return if it failed
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private bool TestFailed(TestExecutionContext context)
        {
            //  localRetryTimes = localRetryTimes - 1;
            return UnsuccessfulResultStates.Contains(context.CurrentResult.ResultState);
        }

        /// <summary>
        /// This method is used to run the tests using the context
        /// </summary>
        /// <param name="context"></param>
        private void RunTest(TestExecutionContext context)
        {
            context.CurrentResult = innerCommand.Execute(context);

        }
        /// <summary>
        /// This method is used to clear the previous result of that run to not duplicate the result of the latest run on failures and rerun
        /// </summary>
        /// <param name="context"></param>
        private void ClearResults(TestExecutionContext context)
        {
            context.CurrentResult = context.CurrentTest.MakeTestResult();
        }
        /// <summary>
        /// This method has the list of all states that are considered for rerun
        /// </summary>
        private static ResultState[] UnsuccessfulResultStates => new[]
        {
        ResultState.Failure,
        ResultState.Error
        };
    }
}

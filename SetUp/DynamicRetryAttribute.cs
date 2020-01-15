using NUnit.Framework;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;
using NUnit.Framework.Internal.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace SeleniumDotNetCoreSample
{
    [AttributeUsage(AttributeTargets.Method)]
    public class DynamicRetryAttribute : Attribute, IWrapSetUpTearDown
    {
        /// <summary>
        /// This method implements the abstract method  for the interface IWrapSetUpTearDown that is
        /// used to control the flow of execution using the innercommand and context to have 
        /// retry based on user choice.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public TestCommand Wrap(TestCommand command)
        {
            return new RetryingCommand(command, Convert.ToInt32(TestContext.Parameters["NumberOfRetrys"]));
        }

    }

}


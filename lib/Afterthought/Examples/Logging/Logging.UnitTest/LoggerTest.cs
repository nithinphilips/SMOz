using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logging.UnitTest.Target;

namespace Logging.UnitTest
{
    [TestClass]
    public class LoggerTest
    {
        /// <summary>
        /// Test whether the log written for the ctor
        /// </summary>
        [TestMethod]
        public void TestLoggingOnCtor()
        {
            Logger.Reset();
            //make sure the logger is at count == 0
            Assert.AreEqual(Logger.Log.Count, 0);
            //Create a new beer. yay
            var b = new Beer();

			//the log must contain correct info
			Assert.AreEqual("Before Logging.UnitTest.Target.Beer..ctor()", Logger.Log[0]);
			Assert.AreEqual("After Logging.UnitTest.Target.Beer..ctor()", Logger.Log[7]);
			
			//make sure the beer is alright
            Assert.IsTrue(b.Full);
            Assert.IsFalse(b.Dropped);
            Assert.IsFalse(b.Empty);

            //The number of log messages must be ok
            Assert.AreEqual(Logger.Log.Count, 16);
        }

        /// <summary>
        /// Test whether log works on methods
        /// </summary>
        [TestMethod]
        public void TestLoggingOnMethod()
        {
            var b = new Beer();

            //reset logger
            Logger.Reset();
            //make sure the logger is at count == 0
            Assert.AreEqual(Logger.Log.Count, 0);

            //drink beer :)
            b.Drink();

			//the log must contain correct info
			Assert.AreEqual("Before Logging.UnitTest.Target.Beer.Drink()", Logger.Log[0]);
			Assert.AreEqual("After Logging.UnitTest.Target.Beer.Drink()", Logger.Log[3]);
			
			//make sure the beer is alright
            Assert.IsTrue(b.Empty);
            Assert.IsFalse(b.Full);
            Assert.IsFalse(b.Dropped);

            //The number of log messages must be ok
            Assert.AreEqual(Logger.Log.Count, 12);

        }

        /// <summary>
        /// Test whether log works on methods with arguments
        /// </summary>
        [TestMethod]
        public void TestLoggingOnMethodWithArguments()
        {
            var b = new Beer();

            //reset logger
            Logger.Reset();
            //make sure the logger is at count == 0
            Assert.AreEqual(Logger.Log.Count, 0);

            //drink beer :)
            b.Drop(false);

			//the log must contain correct info
			Assert.AreEqual("Before Logging.UnitTest.Target.Beer.Drop(False)", Logger.Log[0]);
			Assert.AreEqual("After Logging.UnitTest.Target.Beer.Drop(False)", Logger.Log[7]);
			
			//make sure the beer is alright
            Assert.IsTrue(b.Empty);
            Assert.IsFalse(b.Full);
            Assert.IsTrue(b.Dropped);

            //The number of log messages must be ok
            Assert.AreEqual(Logger.Log.Count, 16);
        }
    }
}

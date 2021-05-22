using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using IIG.BinaryFlag;

namespace TestBinaryFlag
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Test_Route_1_2_lower_bound()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(delegate() { 
                var flag = new MultipleBinaryFlag(1);
            });         
        }

        [TestMethod]
        public void Test_Route_1_2_lower_upper_bound()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(delegate () {
                var flag = new MultipleBinaryFlag(17179868710);
            });
        }

        [TestMethod]
        public void Test_Route_1_3_5_2()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(delegate () {
                var flag = new MultipleBinaryFlag(20);
                flag.SetFlag(25);
            });
        }

        [TestMethod]
        public void Test_Route_1_3_9_10()
        {
            var flag = new MultipleBinaryFlag(16);
            Assert.IsTrue(flag.GetFlag().Value);
        }

        [TestMethod]
        public void Test_Route_1_3_5_6_9_10()
        {
            var flag = new MultipleBinaryFlag(16);
            flag.SetFlag(10);
            Assert.IsTrue(flag.GetFlag().Value);
        }

        [TestMethod]
        public void Test_Route_1_3_7_2()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(delegate () {
                var flag = new MultipleBinaryFlag(20);
                flag.ResetFlag(25);
            });
        }

        [TestMethod]
        public void Test_Route_1_3_7_8_9_11()
        {
            var flag = new MultipleBinaryFlag(16);
            flag.ResetFlag(10);
            Assert.IsFalse(flag.GetFlag().Value);
        }

        [TestMethod]
        public void Test_Route_1_4_9_11()
        {
            var flag = new MultipleBinaryFlag(112, false);
            Assert.IsFalse(flag.GetFlag().Value);
        }

        [TestMethod]
        public void Test_Route_1_3_12()
        {
            var flag = new MultipleBinaryFlag(112);
            flag.Dispose();
            Assert.IsNull(flag.GetFlag());
        }
    }
}

namespace Lk.Test.TasksTests
{
    using Lk.Test.Task5;
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Collections.Generic;

    [TestClass]
    public class IEnumerableCount_Tests
    {
        [DataTestMethod]
        [DataRow("0011000000000000", true, 2)]
        [DataRow("0011000000000000", false, 14)]
        [DataRow("0000000000000000", true, 0)]
        [DataRow("", true, 0)]
        public void Count_ArrayValues(string values, bool predicateValue, int expected)
        {
            var array = new List<bool>(TestHelpers.ConvertStringToBoolArray(values));
            var actual = array.Count(element => element == predicateValue);

            Assert.AreEqual(expected, actual);
        }

        [DataTestMethod]
        [DataRow(null, true)]
        [ExpectedException(typeof(ArgumentException))]
        public void Count_ExpectedArgumentException(bool[] values, bool? predicateValue)
        {
            values.Count(predicateValue.HasValue ? element => element == predicateValue : default(Func<bool, bool>));
            Assert.Fail();
        }
    }
}

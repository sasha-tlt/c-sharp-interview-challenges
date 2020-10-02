namespace Lk.Test.TasksTests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Lk.Test.Task6;

    [TestClass]
    public class IntToBitConverter_Tests
    {
        [DataTestMethod]
        [DataRow(0, "00000000000000000000000000000000")]
        [DataRow(-1431660135, "10101010101010101001100110011001")] //0xAAAA9999 
        public void Convert_Values(int value, string bits)
        {
            var expected = TestHelpers.ConvertStringToBoolArray(bits);
            var actual = IntToBitConverter.Convert(value);

            CollectionAssert.AreEqual(expected, actual);
        }

        [DataTestMethod]
        [DataRow("00000000000000000000000000000000", 0)]
        [DataRow("10101010101010101001100110011001", -1431660135)] //0xAAAA9999 
        public void ConvertBack_Values(string bits, int expected)
        {
            var value = TestHelpers.ConvertStringToBoolArray(bits);
            var actual = IntToBitConverter.ConvertBack(value);

            Assert.AreEqual(expected, actual);
        }

        [DataTestMethod]
        [DataRow("010101001100110011001")]
        [DataRow("1010101010101010100110011001100110101010101")]
        [ExpectedException(typeof(ArgumentException))]
        public void ConvertBack_ExpectedArgumentException(string bits)
        {
            var value = TestHelpers.ConvertStringToBoolArray(bits);
            var actual = IntToBitConverter.ConvertBack(value);

            Assert.Fail("Ожидается исключение ArgumentException");
        }
    }
}

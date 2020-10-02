namespace Lk.Test.TasksTests
{
    using Lk.Test.Task6;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Collections;

    [TestClass]
    public sealed class BitArraySet_Tests
    {
        [DataTestMethod]
        [DataRow(0, 0, true, "11111111111111111111111111111111", "00000000000000000000000000000000")]
        [DataRow(0, -1431660135, true, "11111111111111111111111111111111", "10101010101010101001100110011001")]
        [DataRow(3, -1431660135, true, "01011111111111111111111111111111111", "01010101010101010101001100110011001")]
        [DataRow(0, -1431660135, false, "11111111111111111111111111111111", "10011001100110010101010101010101")]
        public void Set_Values(int index, int value, bool msbFirst, string actualBits, string expectedBits)
        {
            BitArray actual = new BitArray(TestHelpers.ConvertStringToBoolArray(actualBits));
            BitArray expected = new BitArray(TestHelpers.ConvertStringToBoolArray(expectedBits));

            actual.Set(index, value, msbFirst);
            CollectionAssert.AreEqual(expected, actual);
        }

        [DataTestMethod]
        [DataRow(-1, 0, "11111111111111111111111111111111")]
        [DataRow(30, 0, "11111111111111111111111111111111")]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Set_ExpectedArgumentOutOfRangeException(int index, int value, string actualBits)
        {
            BitArray actual = new BitArray(TestHelpers.ConvertStringToBoolArray(actualBits));

            actual.Set(index, value);
            Assert.Fail();
        }
    }
}

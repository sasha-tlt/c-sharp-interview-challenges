namespace Lk.Test.TasksTests
{
    internal static class TestHelpers
    {
        public static bool[] ConvertStringToBoolArray(string value)
        {
            var result = new bool[value.Length];
            for (var i = 0; i < value.Length; i++)
            {
                result[i] = value[i] == '1' ? true : false;
            }

            return result;
        }
    }
}

namespace Lk.Test.Task6
{
    using System;

    public static class IntToBitConverter
    {
        public static bool[] Convert(int value)
        {
            bool[] result = new bool[32];
            for (var i = 0; i < 32; i++)
            {
                result[i] = System.Convert.ToBoolean((value >> 31 - i) & 0x01);
            }

            return result;
        }

        public static int ConvertBack(bool[] value)
        {
            if (value.Length != 32)
            {
                throw new ArgumentException("Недостаточная или избыточная длина массива");
            }

            int result = 0;
            for (var i = 0; i < 32; i++)
            {
                result = (result << 1) | (value[i] ? 1 : 0);
            }

            return result;
        }
    }
}

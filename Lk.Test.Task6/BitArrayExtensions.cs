namespace Lk.Test.Task6
{
    using System;
    using System.Collections;

    public static class BitArrayExtensions
    {
        public static void Set(this BitArray bitArray, int value, bool msbFirst = true)
        {
            if (bitArray.Length < 32)
            {
                throw new ArgumentException("Длина массива BitArray недостаточна для добавления числа");
            }

            bitArray.Set(0, value, msbFirst);
        }

        public static void Set(this BitArray bitArray, int index, int value, bool msbFirst = true)
        {
            if (index < 0 || (index + 32) > bitArray.Length)
            {
                throw new ArgumentOutOfRangeException("Значение параметра index меньше нуля или index больше или равен количеству элементов в массиве BitArray");
            }

            int current = msbFirst ? index : index + 31;
            int iterator = msbFirst ? 1 : -1;
            foreach (var bit in IntToBitConverter.Convert(value))
            {
                bitArray.Set(current, bit);
                current += iterator;
            }
        }
    }
}

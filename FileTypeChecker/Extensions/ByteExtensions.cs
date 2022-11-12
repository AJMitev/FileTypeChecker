namespace FileTypeChecker.Extensions
{
    using System;
    using System.Collections.Generic;

    public static class ByteExtensions
    {
        public static bool SequenceEqual(this IList<byte> thisArr, IList<byte> otherArr)
        {
            var lenght = Math.Min(thisArr.Count, otherArr.Count);

            for (int i = 0; i < lenght; i++)
            {
                var thisByte = thisArr[i];
                var otherByte = otherArr[i];

                if (thisByte != otherByte)
                {
                    return false;
                }
            }

            return true;
        }

        public static int CountMatchingBytes(this IList<byte> thisArr, IList<byte> otherArr)
        {
            var lenght = Math.Min(thisArr.Count, otherArr.Count);
            var counter = 0;

            for (int i = 0; i < lenght; i++)
            {
                var thisByte = thisArr[i];
                var otherByte = otherArr[i];

                if (thisByte != otherByte)
                {
                    return counter;
                }

                counter++;
            }

            return counter;
        }
    }
}

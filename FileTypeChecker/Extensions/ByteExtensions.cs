namespace FileTypeChecker.Extensions
{
    using System;

    public static class ByteExtensions
    {
        public static bool SequenceEqual(this byte[] thisArr, byte[] otherArr)
        {
            var lenght = Math.Min(thisArr.Length, otherArr.Length);

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

        public static int CountMatchingBytes(this byte[] thisArr, byte[] otherArr)
        {
            var lenght = Math.Min(thisArr.Length, otherArr.Length);
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

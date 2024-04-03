using System.Text;
using FileTypeChecker.Abstracts;

namespace FileTypeChecker.Tests.Types
{
    public class ArbitraryCsv2FileType : FileType
    {
        private const string Name = "Arbitrary Csv 2 FileType";
        private const string Extension = "arbitrarycsv2filetype";
        private static readonly byte[] MagicBytes = Encoding.UTF8.GetBytes("ID;field_1;field_2;field_3;field_4;field_5;field_6;field_7;field_8;field_9");
        private static readonly MagicSequence[] MagicBytesJaggedArray = { new MagicSequence(MagicBytes) };
        public ArbitraryCsv2FileType() : base(Name, Extension, MagicBytesJaggedArray) { }
    }
}
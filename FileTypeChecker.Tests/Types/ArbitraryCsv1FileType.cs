using System.Text;
using FileTypeChecker.Abstracts;

namespace FileTypeChecker.Tests.Types
{
    public class ArbitraryCsv1FileType : FileType
    {
        private const string Name = "Arbitrary Csv 1 FileType";
        private const string Extension = "arbitrarycsv1filetype";
        private static readonly MagicSequence[] MagicBytesJaggedArray = { new MagicSequence(Encoding.UTF8.GetBytes("ID;field_1;field_2;field_3;field_4;field_5;field_6;field_7;field_8")) };
        public ArbitraryCsv1FileType() : base(Name, Extension, MagicBytesJaggedArray) { }
    }
}
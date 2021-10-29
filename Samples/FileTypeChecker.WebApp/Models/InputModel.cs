namespace FileTypeChecker.WebApp.Models
{
    using FileTypeChecker.Types;
    using FileTypeChecker.Web.Attributes;
    using Microsoft.AspNetCore.Http;

    public class InputModel
    {
        [AllowImageOnly]
        public IFormFile FirstFile { get; set; }

        [AllowArchiveOnly]
        public IFormFile SecondFile { get; set; }

        [AllowedTypes(Bitmap.TypeExtension)]
        public IFormFile ThirdFile { get; set; }

        [ForbidExecutableFile]
        public IFormFile FourthFile { get; set; }

        [ForbidTypes(MicrosoftOfficeDocument.TypeExtension)]
        public IFormFile FifthFile { get; set; }
    }
}

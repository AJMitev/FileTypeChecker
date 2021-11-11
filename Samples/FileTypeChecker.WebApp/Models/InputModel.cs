namespace FileTypeChecker.WebApp.Models
{
    using FileTypeChecker.Types;
    using FileTypeChecker.Web.Attributes;
    using Microsoft.AspNetCore.Http;

    public class InputModel
    {
        [AllowImages]
        public IFormFile FirstFile { get; set; }

        [AllowArchives]
        public IFormFile SecondFile { get; set; }

        [AllowedTypes(Bitmap.TypeExtension)]
        public IFormFile ThirdFile { get; set; }

        [ForbidExecutables]
        public IFormFile FourthFile { get; set; }

        [ForbidTypes(MicrosoftOfficeDocument.TypeExtension)]
        public IFormFile FifthFile { get; set; }
    }
}

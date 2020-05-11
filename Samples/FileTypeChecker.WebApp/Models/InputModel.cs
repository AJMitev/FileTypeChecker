namespace FileTypeChecker.WebApp.Models
{
    using FileTypeChecker.Web.Attributes;
    using Microsoft.AspNetCore.Http;

    public class InputModel
    {
        [AllowImageOnly]
        public IFormFile FirstFile { get; set; }

        [AllowArchiveOnly]
        public IFormFile SecondFile { get; set; }

        [AllowedTypes(FileExtension.Bitmap)]
        public IFormFile ThirdFile { get; set; }

        [ForbidExecutableFile]
        public IFormFile FourthFile { get; set; }

        [ForbidTypes(FileExtension.Doc)]
        public IFormFile FifthFile { get; set; }
    }
}

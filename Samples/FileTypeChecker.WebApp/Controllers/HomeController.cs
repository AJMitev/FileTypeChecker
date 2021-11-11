namespace FileTypeChecker.WebApp.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using FileTypeChecker.WebApp.Models;
    using System.Text;
    using FileTypeChecker.Web.Attributes;
    using Microsoft.AspNetCore.Http;
    using System.Linq;

    public class HomeController : Controller
    {
        public IActionResult Index()
         => this.View("Multiple");

        public IActionResult Multiple()
            => this.View();

        [HttpPost("fileUpload")]
        public IActionResult UploadFile(InputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                this.TempData.Clear();
                this.TempData["Error"] = this.ParseErrors();
            }
            else if (inputModel.FirstFile != null || inputModel.SecondFile != null
                  || inputModel.ThirdFile != null || inputModel.FourthFile != null
                  || inputModel.FifthFile != null)
            {
                this.TempData.Clear();
                this.TempData["Success"] = true;
            }

            return this.RedirectToAction(nameof(Index));
        }

        [HttpPost("filesUpload")]
        public IActionResult UploadFiles([AllowImages] IFormFile imageFile, [AllowArchives] IFormFile archiveFile)
        {
            if (!this.ModelState.IsValid)
            {
                this.TempData.Clear();
                this.TempData["Error"] = this.ParseErrors();
            }
            else if (imageFile != null || archiveFile != null)
            {
                this.TempData.Clear();
                this.TempData["Success"] = true;
            }

            return this.RedirectToAction(nameof(Index));
        }

        private string ParseErrors()
        {
            var builder = new StringBuilder();

            foreach (var modelState in this.ModelState)
            {
                foreach (var error in modelState.Value.Errors)
                {
                    builder.AppendLine($"{modelState.Key}: {error.ErrorMessage}; ");
                }
            }

            return builder.ToString();
        }
    }
}

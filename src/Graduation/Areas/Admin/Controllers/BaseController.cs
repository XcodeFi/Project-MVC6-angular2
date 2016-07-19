using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Graduation.Infrastructure;
using Microsoft.Net.Http.Headers;
using System.IO;
using Graduation.Infrastructure.Repositories.Abstract;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Graduation.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {

        protected ILoggingRepository _logging;
        protected IHostingEnvironment hostingEnv;
        protected readonly GraduationDbContext _context;
        protected string UploadDestination { get; set; }
        protected string[] AllowedExtensions { get; set; }
        public BaseController(
            ILoggingRepository logging,
            GraduationDbContext context,
            IHostingEnvironment env
            )
        {
            _logging = logging;
            _context = context;
            this.hostingEnv = env;
             _context = context;
        }

        #region help
        [HttpPost]
        public IActionResult UploadFilesAjax()
        {
            long size = 0;
            var files = Request.Form.Files;
            string _fileName = String.Empty;
            foreach (var file in files)
            {
                var filename = ContentDispositionHeaderValue
                                .Parse(file.ContentDisposition)
                                .FileName
                                .Trim('"');
                _fileName = filename;
                //filename = hostingEnv.WebRootPath+ "/images/" + $@"\{filename}";
                filename = UploadDestination + $@"/{filename}";
                size += file.Length;

                if (size == 0)
                {
                    return NotFound();
                }
                //check extension
                bool extension = this.VerifyFileExtension(filename);
                if (extension == false)
                {
                    return new BadRequestObjectResult("File is not image!"); //Json("File is not image!");
                }
                using (FileStream fs = System.IO.File.Create(filename))
                {
                    file.CopyTo(fs);
                    fs.Flush();
                }
            }
            string message = $"{files.Count} {_fileName} file(s) /  { size} bytes uploaded successfully!";
            return Json(_fileName);
        }
        private bool VerifyFileExtension(string path)
        {
            return AllowedExtensions.Contains(Path.GetExtension(path));
        }
        #endregion
    }
}

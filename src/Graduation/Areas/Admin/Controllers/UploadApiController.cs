﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Net.Http.Headers;
using System.IO;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Graduation.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("api/[controller]")]
    [Authorize]
    public class UploadApiController : Controller
    {
        private IHostingEnvironment hostingEnv;
        private string UploadDestination { get; set; }
        private string[] AllowedExtensions { get; set; }

        public UploadApiController(IHostingEnvironment env)
        {
            this.hostingEnv = env;
            AllowedExtensions = new string[] { ".jpg", ".png", ".gif", ".PNG" };
            UploadDestination = hostingEnv.WebRootPath + "/images/cms/";
        }

        //[HttpPost("upload/{path:string}")]
        //public IActionResult UploadFilesAjax(string path)
        //{
        //    long size = 0;
        //    var files = Request.Form.Files;
        //    string _fileName = String.Empty;
        //    foreach (var file in files)
        //    {
        //        var filename = ContentDispositionHeaderValue
        //                        .Parse(file.ContentDisposition)
        //                        .FileName
        //                        .Trim('"');
        //        _fileName = filename;
        //        filename = UploadDestination+path + $@"/{filename}";
        //        size += file.Length;

        //        //check extension
        //        bool extension = this.VerifyFileExtension(filename);
        //        if (extension == false)
        //        {
        //            return new BadRequestObjectResult("File is not image!");
        //        }
        //        using (FileStream fs = System.IO.File.Create(filename))
        //        {
        //            file.CopyTo(fs);
        //            fs.Flush();
        //        }
        //    }
        //    string message = $"{files.Count} {_fileName} file(s) /  { size} bytes uploaded successfully!";
        //    return new OkObjectResult(_fileName);
        //}
        private bool VerifyFileExtension(string path)
        {
            return AllowedExtensions.Contains(Path.GetExtension(path));
        }
        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

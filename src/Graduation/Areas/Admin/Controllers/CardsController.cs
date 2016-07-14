using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Graduation.Entities;
using Graduation.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using System.Net;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Net.Http.Headers;
using Graduation.Infrastructure.Repositories.Abstract;

namespace Graduation.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize("Manager")]
    public class CardsController : Controller
    {
        private readonly GraduationDbContext _context;

        private ICateRepository _cateRepo;
        private IHostingEnvironment hostingEnv;
        private string UploadDestination { get; set; }
        private string[] AllowedExtensions { get; set; }
        public CardsController(GraduationDbContext context, IHostingEnvironment env,
            ICateRepository cateRepo)
        {
            _cateRepo = cateRepo;
            this.hostingEnv = env;
            AllowedExtensions = new string[] { ".jpg", ".png", ".gif", ".PNG" };
            _context = context;
            UploadDestination = hostingEnv.WebRootPath + "/images/cms/news";
        }

        // GET: Cards
        public IActionResult Index()
        {
            ViewData["CateId"] = new SelectList(_cateRepo.GetAll(), "Id", "Name");
            return View();
        }

        // GET: Cards/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var card = await _context.Cards.SingleOrDefaultAsync(m => m.Id == id);
            if (card == null)
            {
                return NotFound();
            }

            return View(card);
        }

        // GET: Cards/Create
        public IActionResult Create()
        {
            ViewData["ApplycationUserId"] = new SelectList(_context.Users, "Id", "UserName");
            ViewData["CateId"] = new SelectList(_context.Categories, "Id", "Name");
            return View();
        }

        // POST: Cards/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ApplycationUserId,CardSize,CardType,CateId,Content,ImageUrl,IsDeleted,IsPublished,LikesNo,RateNo,TextSearch,Title,UrlSlug,ViewNo")] Card card)
        {
            if (ModelState.IsValid)
            {
                _context.Add(card);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["ApplycationUserId"] = new SelectList(_context.Users, "Id", "Id", card.ApplycationUserId);
            ViewData["CateId"] = new SelectList(_context.Categories, "Id", "Name");
            return View(card);
        }

       
        private bool CardExists(int id)
        {
            return _context.Cards.Any(e => e.Id == id);
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
                    return NotFound(); //Json("File is not image!");
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

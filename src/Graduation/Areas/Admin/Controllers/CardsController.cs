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

namespace Graduation.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize("Manager")]
    public class CardsController : Controller
    {
        private readonly GraduationDbContext _context;
        private IHostingEnvironment hostingEnv;
        private string UploadDestination { get; set; }
        private string[] AllowedExtensions { get; set; }
        public CardsController(GraduationDbContext context,IHostingEnvironment env )
        {
            this.hostingEnv = env;
            AllowedExtensions = new string[] { ".jpg", ".png", ".gif",".PNG" };
            _context = context;
            UploadDestination = hostingEnv.WebRootPath + "/images/cms/news";
        }

        // GET: Cards
        public async Task<IActionResult> Index()
        {
            var graduationDbContext = _context.Cards.Include(c => c.ApplicationUser).Include(c => c.Category);
            return View(await graduationDbContext.ToListAsync());
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
            ViewData["CateId"] = new SelectList(_context.Categories, "Id", "Name", card.CateId);
            return View(card);
        }

        // GET: Cards/Edit/5
        public async Task<IActionResult> Edit(int? id)
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
            ViewData["ApplycationUserId"] = new SelectList(_context.Users, "Id", "Id", card.ApplycationUserId);
            ViewData["CateId"] = new SelectList(_context.Categories, "Id", "Description", card.CateId);
            return View(card);
        }

        // POST: Cards/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ApplycationUserId,CardSize,CardType,CateId,Content,DateCreated,ImageUrl,IsDeleted,IsPublished,LikesNo,RateNo,TextSearch,Title,UrlSlug,ViewNo")] Card card)
        {
            if (id != card.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(card);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CardExists(card.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            ViewData["ApplycationUserId"] = new SelectList(_context.Users, "Id", "Id", card.ApplycationUserId);
            ViewData["CateId"] = new SelectList(_context.Categories, "Id", "Description", card.CateId);
            return View(card);
        }

        // GET: Cards/Delete/5
        public async Task<IActionResult> Delete(int? id)
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

        // POST: Cards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var card = await _context.Cards.SingleOrDefaultAsync(m => m.Id == id);
            _context.Cards.Remove(card);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool CardExists(int id)
        {
            return _context.Cards.Any(e => e.Id == id);
        }

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

                //check extension
                bool extension = this.VerifyFileExtension(filename);
                if (extension == false)
                {
                    return Json("File is not image!");
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
    }
}

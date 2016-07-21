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
using Graduation.Infrastructure.Repositories.Abstract;
using Graduation.Areas.Admin.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Net.Http.Headers;
using System.IO;
using Graduation.Infrastructure.Core;

namespace Graduation.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize("Manager")]
    public class CategoriesController : BaseController
    {
        private ICateRepository _cateRepo;
        
        public CategoriesController(
            ILoggingRepository logging,
            GraduationDbContext context,
            ICateRepository cateRepo,
            IHostingEnvironment env
            ):base(logging,context,env)
        {
        
            _cateRepo = cateRepo;
            AllowedExtensions = new string[] { ".jpg", ".png", ".gif", ".PNG" };
            UploadDestination = env.WebRootPath + "/images/cms/cates";
        }

        // GET: Categories
        public IActionResult Index()
        {
            ViewData["ParentId"] = new SelectList(_cateRepo.FindBy(c => c.Level == 0), "Id", "Name");

            IEnumerable<Level> _level = new List<Level>()
            {
                new Level { Id=0,Value=0},
                new Level { Id=1,Value=1}
            };

            ViewData["Level"] = new SelectList(_level, "Id", "Value");
            return View();
        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.SingleOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.Id == id);
        }
    }
    class Level
    {
        public int Id { get; set; }
        public int Value { get; set; }
    }
}

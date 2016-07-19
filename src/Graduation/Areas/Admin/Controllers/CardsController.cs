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
    public class CardsController : BaseController
    {
        private ICateRepository _cateRepo;
        public CardsController(
            ILoggingRepository logging,
            GraduationDbContext context, 
            IHostingEnvironment env,
            ICateRepository cateRepo)
            :base(logging,context,env)
        {
            _cateRepo = cateRepo;
            AllowedExtensions = new string[] { ".jpg", ".png", ".gif", ".PNG" };
            UploadDestination = env.WebRootPath + "/images/cms/news";
        }

        // GET: Cards
        public IActionResult Index()
        {
            ViewData["CateId"] = new SelectList(_cateRepo.GetAll().Where(c=>c.Level==1), "Id", "Name");
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
        private bool CardExists(int id)
        {
            return _context.Cards.Any(e => e.Id == id);
        }
    }
}

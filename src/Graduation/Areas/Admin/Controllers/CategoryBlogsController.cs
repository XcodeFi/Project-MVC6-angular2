using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Graduation.Entities;
using Graduation.Infrastructure;

namespace Graduation.Areas.Admin.Controllers
{
    public class CategoryBlogsController : Controller
    {
        private readonly GraduationDbContext _context;

        public CategoryBlogsController(GraduationDbContext context)
        {
            _context = context;    
        }

        // GET: CategoryBlogs
        public async Task<IActionResult> Index()
        {
            return View(await _context.CategoryBlogs.ToListAsync());
        }

        // GET: CategoryBlogs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoryBlog = await _context.CategoryBlogs.SingleOrDefaultAsync(m => m.Id == id);
            if (categoryBlog == null)
            {
                return NotFound();
            }

            return View(categoryBlog);
        }

        // GET: CategoryBlogs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CategoryBlogs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DateCreated,DateEdited,Description,Status,Title")] CategoryBlog categoryBlog)
        {
            if (ModelState.IsValid)
            {
                _context.Add(categoryBlog);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(categoryBlog);
        }

        // GET: CategoryBlogs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoryBlog = await _context.CategoryBlogs.SingleOrDefaultAsync(m => m.Id == id);
            if (categoryBlog == null)
            {
                return NotFound();
            }
            return View(categoryBlog);
        }

        // POST: CategoryBlogs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DateCreated,DateEdited,Description,Status,Title")] CategoryBlog categoryBlog)
        {
            if (id != categoryBlog.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categoryBlog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryBlogExists(categoryBlog.Id))
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
            return View(categoryBlog);
        }

        // GET: CategoryBlogs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoryBlog = await _context.CategoryBlogs.SingleOrDefaultAsync(m => m.Id == id);
            if (categoryBlog == null)
            {
                return NotFound();
            }

            return View(categoryBlog);
        }

        // POST: CategoryBlogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var categoryBlog = await _context.CategoryBlogs.SingleOrDefaultAsync(m => m.Id == id);
            _context.CategoryBlogs.Remove(categoryBlog);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool CategoryBlogExists(int id)
        {
            return _context.CategoryBlogs.Any(e => e.Id == id);
        }
    }
}

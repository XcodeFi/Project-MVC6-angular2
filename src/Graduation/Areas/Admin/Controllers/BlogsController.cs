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

namespace Graduation.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize("Manager")]
    public class BlogsController : Controller
    {
        private readonly GraduationDbContext _context;

        public BlogsController(GraduationDbContext context)
        {
            _context = context;    
        }

        // GET: Blogs
        public async Task<IActionResult> Index()
        {
            var graduationDbContext = _context.Blogs.Include(b => b.ApplicationUser).Include(b => b.CategoryBlog);
            return View(await graduationDbContext.ToListAsync());
        }

        // GET: Blogs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blog = await _context.Blogs.SingleOrDefaultAsync(m => m.Id == id);
            if (blog == null)
            {
                return NotFound();
            }

            return View(blog);
        }

        // GET: Blogs/Create
        public IActionResult Create()
        {
            ViewData["AppicationUserId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["CateBlogId"] = new SelectList(_context.CategoryBlogs, "Id", "Description");
            return View();
        }

        // POST: Blogs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AppicationUserId,CateBlogId,Content,DateEdited,DatePosted,ImageUrl,IsDeleted,IsLocked,LikeNo,Status,Summary,TextSearch,Title,UrlSlug,UserId,ViewNo")] Blog blog)
        {
            if (ModelState.IsValid)
            {
                _context.Add(blog);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["AppicationUserId"] = new SelectList(_context.Users, "Id", "Id", blog.AppicationUserId);
            ViewData["CateBlogId"] = new SelectList(_context.CategoryBlogs, "Id", "Description", blog.CateBlogId);
            return View(blog);
        }

        // GET: Blogs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blog = await _context.Blogs.SingleOrDefaultAsync(m => m.Id == id);
            if (blog == null)
            {
                return NotFound();
            }
            ViewData["AppicationUserId"] = new SelectList(_context.Users, "Id", "Id", blog.AppicationUserId);
            ViewData["CateBlogId"] = new SelectList(_context.CategoryBlogs, "Id", "Description", blog.CateBlogId);
            return View(blog);
        }

        // POST: Blogs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AppicationUserId,CateBlogId,Content,DateEdited,DatePosted,ImageUrl,IsDeleted,IsLocked,LikeNo,Status,Summary,TextSearch,Title,UrlSlug,UserId,ViewNo")] Blog blog)
        {
            if (id != blog.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(blog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BlogExists(blog.Id))
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
            ViewData["AppicationUserId"] = new SelectList(_context.Users, "Id", "Id", blog.AppicationUserId);
            ViewData["CateBlogId"] = new SelectList(_context.CategoryBlogs, "Id", "Description", blog.CateBlogId);
            return View(blog);
        }

        // GET: Blogs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blog = await _context.Blogs.SingleOrDefaultAsync(m => m.Id == id);
            if (blog == null)
            {
                return NotFound();
            }

            return View(blog);
        }

        // POST: Blogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var blog = await _context.Blogs.SingleOrDefaultAsync(m => m.Id == id);
            _context.Blogs.Remove(blog);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool BlogExists(int id)
        {
            return _context.Blogs.Any(e => e.Id == id);
        }
    }
}

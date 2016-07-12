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
    public class ErrorsController : Controller
    {
        private readonly GraduationDbContext _context;

        public ErrorsController(GraduationDbContext context)
        {
            _context = context;    
        }

        // GET: Errors
        public async Task<IActionResult> Index()
        {
            return View(await _context.Errors.ToListAsync());
        }

        // GET: Errors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var error = await _context.Errors.SingleOrDefaultAsync(m => m.Id == id);
            if (error == null)
            {
                return NotFound();
            }

            return View(error);
        }

        // GET: Errors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Errors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DateCreated,Message,StackTrace")] Error error)
        {
            if (ModelState.IsValid)
            {
                _context.Add(error);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(error);
        }

        // GET: Errors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var error = await _context.Errors.SingleOrDefaultAsync(m => m.Id == id);
            if (error == null)
            {
                return NotFound();
            }
            return View(error);
        }

        // POST: Errors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DateCreated,Message,StackTrace")] Error error)
        {
            if (id != error.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(error);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ErrorExists(error.Id))
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
            return View(error);
        }

        // GET: Errors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var error = await _context.Errors.SingleOrDefaultAsync(m => m.Id == id);
            if (error == null)
            {
                return NotFound();
            }

            return View(error);
        }

        // POST: Errors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var error = await _context.Errors.SingleOrDefaultAsync(m => m.Id == id);
            _context.Errors.Remove(error);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ErrorExists(int id)
        {
            return _context.Errors.Any(e => e.Id == id);
        }
    }
}

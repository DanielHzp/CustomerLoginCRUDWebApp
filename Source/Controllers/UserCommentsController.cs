﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UserLoginCRUDWebApp.Data;
using UserLoginCRUDWebApp.Models;

namespace UserLoginCRUDWebApp.Controllers
{
    public class UserCommentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserCommentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: UserComments
        //Item that is going to show all comments
        public async Task<IActionResult> Index()
        {
              return _context.UserComment != null ? 
                          View(await _context.UserComment.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.UserComment'  is null.");
        }


        // GET: UserComments/Show search form
        //If the URL contains this method name, the following command is executed with this controller
        public async Task<IActionResult> SearchFormView()
        {
            //We need to create search form VIEW in views folder
            return View(); //Go find the view with given name
                        
        }
        // POST: UserComments/Show search form RESULTS
        //If the URL contains this method name, the following command is executed with this controller
       // public string DisplaySearchResults(String SearchInput)
        public async Task<IActionResult> DisplaySearchResults(String SearchInput)
        {
            //Return message with searched input  public string .....
            //return "You searched for: "+ SearchInput; 

            //We want to return a list of filtered results from the Index table
            return View("Index", await _context.UserComment.Where(j=>j.CommentDescription.Contains(SearchInput)).ToListAsync()); //Go to index view and search the input string

        }

        // GET: UserComments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.UserComment == null)
            {
                return NotFound();
            }

            var userComment = await _context.UserComment
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userComment == null)
            {
                return NotFound();
            }

            return View(userComment);
        }

        //Show USER LOGIN PAGE before redirecting to CREATE NEW COMMENT view
        //[Authorize]
        // GET: UserComments/Create
        public IActionResult Create()
        {
            
            return View();
        }



        // POST: UserComments/Create ADD NEW COMMENTS TO DATABASE!!!!
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
       // [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CommentDescription,Details")] UserComment userComment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userComment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(userComment);
        }

        // GET: UserComments/Edit/5
        //[Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.UserComment == null)
            {
                return NotFound();
            }

            var userComment = await _context.UserComment.FindAsync(id);
            if (userComment == null)
            {
                return NotFound();
            }
            return View(userComment);
        }

        // POST: UserComments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
       // [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CommentDescription,Details")] UserComment userComment)
        {
            if (id != userComment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userComment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserCommentExists(userComment.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(userComment);
        }

        // GET: UserComments/Delete/5
        //[Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.UserComment == null)
            {
                return NotFound();
            }

            var userComment = await _context.UserComment
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userComment == null)
            {
                return NotFound();
            }

            return View(userComment);
        }

        // POST: UserComments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.UserComment == null)
            {
                return Problem("Entity set 'ApplicationDbContext.UserComment'  is null.");
            }
            var userComment = await _context.UserComment.FindAsync(id);
            if (userComment != null)
            {
                _context.UserComment.Remove(userComment);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserCommentExists(int id)
        {
          return (_context.UserComment?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DeviceManagement_WebApp.Data;
using DeviceManagement_WebApp.Models;
using DeviceManagement_WebApp.Repository;

namespace DeviceManagement_WebApp.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ConnectedOfficeContext _context;
        private readonly ICategoryRepository _categoryRepository;

        public CategoriesController(ConnectedOfficeContext context, ICategoryRepository categoryRepository)
        {
            _context = context;
            _categoryRepository = categoryRepository;
        }
        
        //Get all categories in the database
        // GET: Categories
        public async Task<IActionResult> Index()
        {
            return View(_categoryRepository.GetAll());
        }

        //Get a specific category in the  database
        // GET: Categories/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = _categoryRepository.GetById(id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        //Create a category in the database
        // GET: Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        //Create a category in the database
        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryId,CategoryName,CategoryDescription,DateCreated")] Category category)
        {
            category.CategoryId = Guid.NewGuid();
            _categoryRepository.Add(category);
            return RedirectToAction(nameof(Index));
        }

        //Edit a category in the database
        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = _categoryRepository.GetById(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        //Edit a  category in the database
        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("CategoryId,CategoryName,CategoryDescription,DateCreated")] Category category)
        {
            if (id != category.CategoryId)
            {
                return NotFound();
            }
            _categoryRepository.Update(category);
            return RedirectToAction(nameof(Index));
        }

        //Delete a category in the database
        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = _categoryRepository.GetById(id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        //Delete a category in the database
        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var category = _categoryRepository.GetById(id);
            if(category != null)
            {
                _categoryRepository.Remove(category);
            }

            return RedirectToAction(nameof(Index));
        }

        //Check if a category exists in the  database
        private bool CategoryExists(Guid id)
        {
            var category = _categoryRepository.GetById(id);
            return category != null ? true : false;
        }
    }
}

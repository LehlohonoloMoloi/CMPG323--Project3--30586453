using System;
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
    public class DevicesController : Controller
    {
        private readonly ConnectedOfficeContext _context;
        private readonly IDeviceRepository _deviceRepository;
        private readonly IZoneRepository _zoneRepository;
        private readonly ICategoryRepository _categoryRepository;

        public DevicesController(ConnectedOfficeContext context, IDeviceRepository deviceRepository, IZoneRepository zoneRepository, ICategoryRepository categoryRepository)
        {
            _context = context;
            _deviceRepository = deviceRepository;
            _zoneRepository = zoneRepository;
            _categoryRepository = categoryRepository;
        }

        //Get all devices in the database
        // GET: Devices
        public async Task<IActionResult> Index()
        {
            return View(_deviceRepository.GetAll());
        }

        //Get a specific device in the database
        // GET: Devices/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var device = _deviceRepository.GetById(id);
            if (device == null)
            {
                return NotFound();
            }

            return View(device);
        }

        //Create a device in the database
        // GET: Devices/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "CategoryName");
            ViewData["ZoneId"] = new SelectList(_context.Zone, "ZoneId", "ZoneName");
            return View();
        }

        //Create a device in the database
        // POST: Devices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DeviceId,DeviceName,CategoryId,ZoneId,Status,IsActive,DateCreated")] Device device)
        {
            device.DeviceId = Guid.NewGuid();
            _deviceRepository.Add(device); 
            return RedirectToAction(nameof(Index));


        }

        //Edit a device in the database
        // GET: Devices/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var device = _deviceRepository.GetById(id);
            if (device == null)
            {
                return NotFound();  
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "CategoryName", device.CategoryId);
            ViewData["ZoneId"] = new SelectList(_context.Zone, "ZoneId", "ZoneName", device.ZoneId);
            return View(device);
        }

        //Edit a device in the database
        // POST: Devices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("DeviceId,DeviceName,CategoryId,ZoneId,Status,IsActive,DateCreated")] Device device)
        {
            if (id != device.DeviceId)
            {
                return NotFound();
            }
            _deviceRepository.Update(device);
            return RedirectToAction(nameof(Index));

        }

        //Delete a device in the database
        // GET: Devices/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var device = _categoryRepository.GetById(id);
            if (device == null)
            {
                return NotFound();
            }

            return View(device);
        }

        //Delete a device in the database
        // POST: Devices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var device = _deviceRepository.GetById(id);
            _deviceRepository.Remove(device);
            return RedirectToAction(nameof(Index));
        }

        //Check if a device exists in the database
        private bool DeviceExists(Guid id)
        {
            var device = _deviceRepository.GetById(id);
            return device != null ? true : false;
        }
    }
}

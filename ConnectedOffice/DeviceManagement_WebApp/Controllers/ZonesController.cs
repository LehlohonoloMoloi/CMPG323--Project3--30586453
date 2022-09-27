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
    public class ZonesController : Controller
    {
        private readonly ConnectedOfficeContext _context;
        private readonly IZoneRepository _zoneRepository;


        public ZonesController(ConnectedOfficeContext context, IZoneRepository zoneRepository)
        {
            _context = context;
            _zoneRepository = zoneRepository;
        }

        //Get all zones in thhe database
        // GET: Zones
        public async Task<IActionResult> Index()
        {
            return View(_zoneRepository.GetAll());
        }

        //get a specific zone in the database using zone_id
        // GET: Zones/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zone = _zoneRepository.GetById(id);
            if (zone == null)
            {
                return NotFound();
            }

            return View(zone);
        }

        //Create a new zone in the database
        // GET: Zones/Create
        public IActionResult Create()
        {
            return View();
        }

        //Create a new zone in the database
        // POST: Zones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ZoneId,ZoneName,ZoneDescription,DateCreated")] Zone zone)
        {
            zone.ZoneId = Guid.NewGuid();
            _zoneRepository.Add(zone);
            return RedirectToAction(nameof(Index));
        }

        //Edit a specific zone in the database
        // GET: Zones/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zone = _zoneRepository.GetById(id);
            if (zone == null)
            {
                return NotFound();
            }
            return View(zone);
        }

        //Edit a specific zone in the database
        // POST: Zones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ZoneId,ZoneName,ZoneDescription,DateCreated")] Zone zone)
        {
            if (id != zone.ZoneId)
            {
                return NotFound();
            }

            _zoneRepository.Update(zone);
            return RedirectToAction(nameof(Index));

        }

        //Delete a zone in the database
        // GET: Zones/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zone = _zoneRepository.GetById(id);
            if (zone == null)
            {
                return NotFound();
            }

            return View(zone);
        }

        //Delete a zone in the database 
        // POST: Zones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var zone = _zoneRepository.GetById(id);
            if(zone != null)
            {
                _zoneRepository.Remove(zone);
            }
            return RedirectToAction(nameof(Index));
        }

        //Check if a zone exists in the database
        private bool ZoneExists(Guid id)
        {
            var zone = _zoneRepository.GetById(id);
            return zone != null ? true : false;
        }
    }
}

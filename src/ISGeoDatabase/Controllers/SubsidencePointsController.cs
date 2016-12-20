using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ISGeoDatabase.Data;
using ISGeoDatabase.Models;
using System;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;

namespace ISGeoDatabase.Controllers
{
    public class SubsidencePointsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private List<SelectListItem> Countries;
        private List<SelectListItem> Region;
        private List<SelectListItem> DataTypes;
        private List<SelectListItem> Cities;

        public SubsidencePointsController(ApplicationDbContext context)
        {
            _context = context;    
        }
        private IEnumerable<string> GetCountries()
        {
            return new List<string> { "Mexico", "USA", "Canada" };
        }

        private IEnumerable<string> GetRegions()
        {
            return new List<string> { "North", "South", "East", "West", "Guanajuato" };
        }

        private IEnumerable<string> GetDataTypes()
        {
            return new List<string> { "Structural Measurement", "Image", "Other" };
        }

        private IEnumerable<string> GetCities()
        {
            return new List<string> { "Celeya", "Mexico City" };
        }


        // GET: SubsidencePointer
        public async Task<IActionResult> Index(FormFields form)
        {
            SetViewBags(form.sortOrder);
            var data = from m in _context.SubsidencePoint select m;
            switch (form.sortOrder)
            {
                case "region":
                    data = data.OrderBy(o => o.Region);
                    break;
                case "region_desc":
                    data = data.OrderByDescending(o => o.Region);
                    break;
                case "city":
                    data = data.OrderBy(o => o.City);
                    break;
                case "city_desc":
                    data = data.OrderByDescending(o => o.City);
                    break;
                case "team":
                    data = data.OrderBy(o => o.TeamNumber);
                    break;
                case "team_desc":
                    data = data.OrderByDescending(o => o.TeamNumber);
                    break;
                case "station":
                    data = data.OrderBy(o => o.StationNumber);
                    break;
                case "station_desc":
                    data = data.OrderByDescending(o => o.StationNumber);
                    break;
                case "date":
                    data = data.OrderBy(o => o.DateAndTime);
                    break;
                case "date_desc":
                    data = data.OrderByDescending(o => o.DateAndTime);
                    break;
                case "coord":
                    data = data.OrderBy(o => o.Coordinates);
                    break;
                case "coord_desc":
                    data = data.OrderByDescending(o => o.Coordinates);
                    break;
                case "fault":
                    data = data.OrderBy(o => o.Fault);
                    break;
                case "fault_desc":
                    data = data.OrderByDescending(o => o.Fault);
                    break;
                case "type":
                    data = data.OrderBy(o => o.DataType);
                    break;
                case "type_desc":
                    data = data.OrderByDescending(o => o.DataType);
                    break;
                case "strike":
                    data = data.OrderBy(o => o.Strike);
                    break;
                case "strike_desc":
                    data = data.OrderByDescending(o => o.Strike);
                    break;
                case "throw":
                    data = data.OrderBy(o => o.Throw);
                    break;
                case "throw_desc":
                    data = data.OrderByDescending(o => o.Throw);
                    break;
                case "slip":
                    data = data.OrderBy(o => o.Slip);
                    break;
                case "slip_desc":
                    data = data.OrderByDescending(o => o.Slip);
                    break;
                case "angle":
                    data = data.OrderBy(o => o.Angle);
                    break;
                case "angle_desc":
                    data = data.OrderByDescending(o => o.Angle);
                    break;
                case "country_desc":
                    data = data.OrderByDescending(o => o.Country);
                    break;
                case "country":
                    data = data.OrderBy(o => o.Country);
                    break;
                default:
                    data = data.OrderBy(o => o.Country);
                    break;
            }
            int pageSize = 5;
            int pageNumber = (form.page ?? 1);
            if (!string.IsNullOrEmpty(form.country))
                {
                    data = data.Where(x => x.Country.Equals(form.country));
                }
                if (!string.IsNullOrEmpty(form.region))
                {
                    data = data.Where(x => x.Region.Equals(form.region));
                }
                if (!string.IsNullOrEmpty(form.city))
                {
                    data = data.Where(x => x.City.Equals(form.city));
                }
                if (!string.IsNullOrEmpty(form.teamno))
                {
                    data = data.Where(x => x.TeamNumber.Equals(form.teamno));
                }
                if (!string.IsNullOrEmpty(form.fault))
                {
                    data = data.Where(x => x.Fault.Equals(form.fault));
                }
                if (!string.IsNullOrEmpty(form.type))
                {
                    data = data.Where(x => x.DataType.Equals(form.type));
                }
                if (form.startdate.CompareTo(form.enddate) < 0)
                {
                    data = data.Where(x => (x.DateAndTime > form.startdate) && (x.DateAndTime < form.enddate));
                }

                if (form.strikelow < form.strikehigh)
                {
                    data = data.Where(x => (x.Strike > form.strikelow) && (x.Strike < form.strikehigh));
                }
                if (form.thwlow < form.thwhigh)
                {
                    data = data.Where(x => (x.Throw > form.thwlow) && (x.Throw < form.thwhigh));
                }
                if (form.sliplow < form.sliphigh)
                {
                    data = data.Where(x => (x.Slip > form.sliplow) && (x.Slip < form.sliphigh));
                }
                if (form.anglelow < form.anglehigh)
                {
                    data = data.Where(x => (x.Angle > form.anglelow) && (x.Angle < form.anglehigh));
                }
            var count = data.Count();
            data = data.Skip(pageSize * (pageNumber - 1)).Take(pageSize);
            var model = new SubTable {
                Page = pageNumber,
                Size = count,
                NumPages = (((count % 5) == 0) ? (count / 5) : ((count / 5) + 1)),
                Table = await data.ToListAsync(),
                Filter = form
            };
            return View(model);
        }

        //[HttpPost]
        public async Task<IActionResult> GetTable(FormFields form)
        {
            SetViewBags(form.sortOrder);
            var data = from m in _context.SubsidencePoint select m;
            switch (form.sortOrder)
            {
                case "region":
                    data = data.OrderBy(o => o.Region);
                    break;
                case "region_desc":
                    data = data.OrderByDescending(o => o.Region);
                    break;
                case "city":
                    data = data.OrderBy(o => o.City);
                    break;
                case "city_desc":
                    data = data.OrderByDescending(o => o.City);
                    break;
                case "team":
                    data = data.OrderBy(o => o.TeamNumber);
                    break;
                case "team_desc":
                    data = data.OrderByDescending(o => o.TeamNumber);
                    break;
                case "station":
                    data = data.OrderBy(o => o.StationNumber);
                    break;
                case "station_desc":
                    data = data.OrderByDescending(o => o.StationNumber);
                    break;
                case "date":
                    data = data.OrderBy(o => o.DateAndTime);
                    break;
                case "date_desc":
                    data = data.OrderByDescending(o => o.DateAndTime);
                    break;
                case "coord":
                    data = data.OrderBy(o => o.Coordinates);
                    break;
                case "coord_desc":
                    data = data.OrderByDescending(o => o.Coordinates);
                    break;
                case "fault":
                    data = data.OrderBy(o => o.Fault);
                    break;
                case "fault_desc":
                    data = data.OrderByDescending(o => o.Fault);
                    break;
                case "type":
                    data = data.OrderBy(o => o.DataType);
                    break;
                case "type_desc":
                    data = data.OrderByDescending(o => o.DataType);
                    break;
                case "strike":
                    data = data.OrderBy(o => o.Strike);
                    break;
                case "strike_desc":
                    data = data.OrderByDescending(o => o.Strike);
                    break;
                case "throw":
                    data = data.OrderBy(o => o.Throw);
                    break;
                case "throw_desc":
                    data = data.OrderByDescending(o => o.Throw);
                    break;
                case "slip":
                    data = data.OrderBy(o => o.Slip);
                    break;
                case "slip_desc":
                    data = data.OrderByDescending(o => o.Slip);
                    break;
                case "angle":
                    data = data.OrderBy(o => o.Angle);
                    break;
                case "angle_desc":
                    data = data.OrderByDescending(o => o.Angle);
                    break;
                case "country_desc":
                    data = data.OrderByDescending(o => o.Country);
                    break;
                case "country":
                    data = data.OrderBy(o => o.Country);
                    break;
                default:
                    data = data.OrderBy(o => o.Country);
                    break;
            }
            int pageSize = 5;
            int pageNumber = (form.page ?? 1);
            if (!string.IsNullOrEmpty(form.country))
            {
                data = data.Where(x => x.Country.Equals(form.country));
            }
            if (!string.IsNullOrEmpty(form.region))
            {
                data = data.Where(x => x.Region.Equals(form.region));
            }
            if (!string.IsNullOrEmpty(form.city))
            {
                data = data.Where(x => x.City.Equals(form.city));
            }
            if (!string.IsNullOrEmpty(form.teamno))
            {
                data = data.Where(x => x.TeamNumber.Equals(form.teamno));
            }
            if (!string.IsNullOrEmpty(form.fault))
            {
                data = data.Where(x => x.Fault.Equals(form.fault));
            }
            if (!string.IsNullOrEmpty(form.type))
            {
                data = data.Where(x => x.DataType.Equals(form.type));
            }
            if (form.startdate.CompareTo(form.enddate) < 0)
            {
                data = data.Where(x => (x.DateAndTime > form.startdate) && (x.DateAndTime < form.enddate));
            }

            if (form.strikelow < form.strikehigh)
            {
                data = data.Where(x => (x.Strike > form.strikelow) && (x.Strike < form.strikehigh));
            }
            if (form.thwlow < form.thwhigh)
            {
                data = data.Where(x => (x.Throw > form.thwlow) && (x.Throw < form.thwhigh));
            }
            if (form.sliplow < form.sliphigh)
            {
                data = data.Where(x => (x.Slip > form.sliplow) && (x.Slip < form.sliphigh));
            }
            if (form.anglelow < form.anglehigh)
            {
                data = data.Where(x => (x.Angle > form.anglelow) && (x.Angle < form.anglehigh));
            }
            int count = data.Count();
            data = data.Skip(pageSize * (pageNumber - 1)).Take(pageSize);
            var model = new SubTable
            {
                Page = pageNumber,
                Size = count,
                NumPages = (((count % 5) == 0) ? (count / 5) : ((count / 5) + 1)),
                Table = await data.ToListAsync(),
                Filter = form
            };
            return PartialView("_Table", model);
        }


        private void SetViewBags(string sortOrder)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.CountrySortParm = sortOrder == "country" ? "country_desc" : "country";
            ViewBag.RegionSortParm = sortOrder == "region" ? "region_dec" : "region";
            ViewBag.CitySortParm = sortOrder == "city" ? "city_desc" : "city";
            ViewBag.TeamSortParm = sortOrder == "team" ? "team_desc" : "team";
            ViewBag.StationSortParm = sortOrder == "station" ? "station_desc" : "station";
            ViewBag.DateSortParm = sortOrder == "date" ? "date_desc" : "date";
            ViewBag.CoordinatesSortParm = sortOrder == "coord" ? "coord_desc" : "coord";
            ViewBag.FaultSortParm = sortOrder == "fault" ? "fault_desc" : "fault";
            ViewBag.TypeSortParm = sortOrder == "type" ? "type_desc" : "type";
            ViewBag.StrikeSortParm = sortOrder == "strike" ? "strike_desc" : "strike";
            ViewBag.ThrowSortParm = sortOrder == "throw" ? "throw_desc" : "throw";
            ViewBag.SlipSortParm = sortOrder == "slip" ? "slip_desc" : "slip";
            ViewBag.AngleSortParm = sortOrder == "angle" ? "angle_desc" : "angle";
        }

        [Produces("application/json")]
        [HttpGet]
        public string GetAll()
        {
            var data = from m in _context.SubsidencePoint select m;
            return JsonConvert.SerializeObject(data);
        }

        [Produces("application/json")]
        [HttpGet]
        public string Filter(FormFields form)
        {
            SetViewBags(form.sortOrder);
            var data = from m in _context.SubsidencePoint select m;
            switch (form.sortOrder)
            {
                case "region":
                    data = data.OrderBy(o => o.Region);
                    break;
                case "region_desc":
                    data = data.OrderByDescending(o => o.Region);
                    break;
                case "city":
                    data = data.OrderBy(o => o.City);
                    break;
                case "city_desc":
                    data = data.OrderByDescending(o => o.City);
                    break;
                case "team":
                    data = data.OrderBy(o => o.TeamNumber);
                    break;
                case "team_desc":
                    data = data.OrderByDescending(o => o.TeamNumber);
                    break;
                case "station":
                    data = data.OrderBy(o => o.StationNumber);
                    break;
                case "station_desc":
                    data = data.OrderByDescending(o => o.StationNumber);
                    break;
                case "date":
                    data = data.OrderBy(o => o.DateAndTime);
                    break;
                case "date_desc":
                    data = data.OrderByDescending(o => o.DateAndTime);
                    break;
                case "coord":
                    data = data.OrderBy(o => o.Coordinates);
                    break;
                case "coord_desc":
                    data = data.OrderByDescending(o => o.Coordinates);
                    break;
                case "fault":
                    data = data.OrderBy(o => o.Fault);
                    break;
                case "fault_desc":
                    data = data.OrderByDescending(o => o.Fault);
                    break;
                case "type":
                    data = data.OrderBy(o => o.DataType);
                    break;
                case "type_desc":
                    data = data.OrderByDescending(o => o.DataType);
                    break;
                case "strike":
                    data = data.OrderBy(o => o.Strike);
                    break;
                case "strike_desc":
                    data = data.OrderByDescending(o => o.Strike);
                    break;
                case "throw":
                    data = data.OrderBy(o => o.Throw);
                    break;
                case "throw_desc":
                    data = data.OrderByDescending(o => o.Throw);
                    break;
                case "slip":
                    data = data.OrderBy(o => o.Slip);
                    break;
                case "slip_desc":
                    data = data.OrderByDescending(o => o.Slip);
                    break;
                case "angle":
                    data = data.OrderBy(o => o.Angle);
                    break;
                case "angle_desc":
                    data = data.OrderByDescending(o => o.Angle);
                    break;
                case "country_desc":
                    data = data.OrderByDescending(o => o.Country);
                    break;
                case "country":
                    data = data.OrderBy(o => o.Country);
                    break;
                default:
                    data = data.OrderBy(o => o.Country);
                    break;
            }
            if (!string.IsNullOrEmpty(form.country))
            {
                data = data.Where(x => x.Country.Equals(form.country));
            }
            if (!string.IsNullOrEmpty(form.region))
            {
                data = data.Where(x => x.Region.Equals(form.region));
            }
            if (!string.IsNullOrEmpty(form.city))
            {
                data = data.Where(x => x.City.Equals(form.city));
            }
            if (!string.IsNullOrEmpty(form.teamno))
            {
                data = data.Where(x => x.TeamNumber.Equals(form.teamno));
            }
            if (!string.IsNullOrEmpty(form.fault))
            {
                data = data.Where(x => x.Fault.Equals(form.fault));
            }
            if (!string.IsNullOrEmpty(form.type))
            {
                data = data.Where(x => x.DataType.Equals(form.type));
            }
            if (form.startdate.CompareTo(form.enddate) < 0)
            {
                data = data.Where(x => (x.DateAndTime > form.startdate) && (x.DateAndTime < form.enddate));
            }

            if (form.strikelow < form.strikehigh)
            {
                data = data.Where(x => (x.Strike > form.strikelow) && (x.Strike < form.strikehigh));
            }
            if (form.thwlow < form.thwhigh)
            {
                data = data.Where(x => (x.Throw > form.thwlow) && (x.Throw < form.thwhigh));
            }
            if (form.sliplow < form.sliphigh)
            {
                data = data.Where(x => (x.Slip > form.sliplow) && (x.Slip < form.sliphigh));
            }
            if (form.anglelow < form.anglehigh)
            {
                data = data.Where(x => (x.Angle > form.anglelow) && (x.Angle < form.anglehigh));
            }
            return JsonConvert.SerializeObject(data);
        }


        [Authorize]

        // GET: SubsidencePoints/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subsidencePoint = await _context.SubsidencePoint.Include(x => x.Images).SingleOrDefaultAsync(m => m.ID == id);
            if (subsidencePoint == null)
            {
                return NotFound();
            }

            return View(subsidencePoint);
        }

        [Authorize]

        // GET: SubsidencePoints/Create
        public IActionResult Create()
        {
            var model = new SubsidencePoint();
            model.Countries = GetCountries();
            model.Regions = GetRegions();
            model.DataTypes = GetDataTypes();
            model.Cities = GetCities();
            return View(model);
        }

        // POST: SubsidencePoints/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SubsidencePoint subsidencePoint)
        {
            if (ModelState.IsValid)
            {
                _context.Add(subsidencePoint);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            subsidencePoint.Countries = GetCountries();
            subsidencePoint.Regions = GetRegions();
            subsidencePoint.DataTypes = GetDataTypes();
            subsidencePoint.Cities = GetCities();
            return View(subsidencePoint);
        }



        // GET: SubsidencePoints/Edit/5
        [Authorize]

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subsidencePoint = await _context.SubsidencePoint.Include(x => x.Images).SingleOrDefaultAsync(m => m.ID == id);
            if (subsidencePoint == null)
            {
                return NotFound();
            }
            subsidencePoint.Countries = GetCountries();
            subsidencePoint.Regions = GetRegions();
            subsidencePoint.DataTypes = GetDataTypes();
            subsidencePoint.Cities = GetCities();
            return View(subsidencePoint);
        }

        // POST: SubsidencePoints/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SubsidencePoint subsidencePoint)
        {
            if (id != subsidencePoint.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(subsidencePoint);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubsidencePointExists(subsidencePoint.ID))
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
            subsidencePoint.Countries = GetCountries();
            subsidencePoint.Regions = GetRegions();
            subsidencePoint.DataTypes = GetDataTypes();
            subsidencePoint.Cities = GetCities();
            return View(subsidencePoint);
        }

        [Authorize]
        // GET: SubsidencePoints/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subsidencePoint = await _context.SubsidencePoint.SingleOrDefaultAsync(m => m.ID == id);
            if (subsidencePoint == null)
            {
                return NotFound();
            }

            return View(subsidencePoint);
        }

        // POST: SubsidencePoints/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var subsidencePoint = await _context.SubsidencePoint.SingleOrDefaultAsync(m => m.ID == id);
            _context.SubsidencePoint.Remove(subsidencePoint);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool SubsidencePointExists(int id)
        {
            return _context.SubsidencePoint.Any(e => e.ID == id);
        }



    }
}

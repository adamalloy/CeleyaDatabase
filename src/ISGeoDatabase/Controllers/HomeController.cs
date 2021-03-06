﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ISGeoDatabase.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Data/
        public IActionResult Index()
        {
            return View();
        }

        //
        // GET: /About/

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        //
        // GET: /Contact/
        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        //
        // GET : /Error/
        public IActionResult Error()
        {
            return View();
        }
    }
}

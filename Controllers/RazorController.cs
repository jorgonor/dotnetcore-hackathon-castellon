using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using examples_dotnet_core.Data;
using examples_dotnet_core.Models;
using Microsoft.EntityFrameworkCore;

namespace examples_dotnet_core.Controllers
{
    public class RazorController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public RazorController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IActionResult> Index()
        {
            List<Vehicle> vehicles = await _dbContext.Vehicles.ToListAsync();

            return View(vehicles);
        }
    }
}

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
    public class EFCoreController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public EFCoreController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IActionResult> Index(
            [FromQuery(Name = "pattern")] string pattern,
            [FromQuery(Name = "creation_date")] string creationDate)
        {
            if (String.IsNullOrEmpty(pattern) ||
                String.IsNullOrEmpty(creationDate)) {
                return BadRequest("Parameters are mandatory");
            }

            DateTime creationDateTime;

            try {
                creationDateTime = DateTime.Parse(creationDate);
            } catch {
                return BadRequest("The date provided is not a valid datetime.");
            }

            IQueryable<Vehicle> qry = from v in _dbContext.Vehicles
                                      where
                                          v.Name.Contains(pattern) &&
                                          v.CreatedAt.Date == creationDateTime.Date
                                      select v;

            List<Vehicle> vehicles = await qry.ToListAsync();

            return Ok(vehicles);
        }
    }
}

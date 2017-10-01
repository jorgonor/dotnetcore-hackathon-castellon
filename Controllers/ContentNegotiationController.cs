using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace examples_dotnet_core.Controllers
{
    public class ContentNegotiationController : Controller
    {
        private static List<string> items = new List<string>() {
            "Hackathon",
            "Castellón",
            "2017"
        };
        
        public List<string> Index()
        {
            return items;
        }
    }
}

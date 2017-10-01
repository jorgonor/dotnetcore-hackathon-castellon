using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace examples_dotnet_core.Controllers
{
    [Route("ModelBinding"), Produces("application/json")]
    public class ModelBindingController : Controller
    {
        public class Body
        {
            public int Number { get; set; }
            [Required]
            public List<int> List { get; set; }
        }

        [HttpPost, Route("{id}")]
        public IActionResult Index(
            [FromQuery(Name = "query")] string query,
            [FromBody] Body body,
            [FromRoute(Name = "id")] int id,
            [FromHeader(Name = "User-Agent")] string userAgent    
        )
        {
            bool modelStateValid = ModelState.IsValid;
            var responseData =  new {
                id,
                query,
                userAgent,
                body,
                modelStateValid
            };

            if (!modelStateValid) {
                return BadRequest(responseData);
            }

            return Ok(responseData);
        }
    }
}
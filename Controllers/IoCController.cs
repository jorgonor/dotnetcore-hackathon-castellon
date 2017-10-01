using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using examples_dotnet_core.Services;
using Microsoft.AspNetCore.Mvc;

namespace examples_dotnet_core.Controllers
{
    [Route("IoC")]
    public class IoCController : Controller
    {
        private readonly IJokeProvider _jokeProvider;

        public class JokeViewModel
        {
            public string Message { get;set; }
        }

        public IoCController(IJokeProvider jokeProvider)
        {
            _jokeProvider = jokeProvider;
        }

        public async Task<IActionResult> Index()
        {
            string message = await _jokeProvider.SayJoke();

            return View(new JokeViewModel{
                Message = message
            });
        }
    }
}

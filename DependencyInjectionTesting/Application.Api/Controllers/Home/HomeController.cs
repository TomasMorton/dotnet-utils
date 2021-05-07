using Application.Api.SimulatedDependency;
using Microsoft.AspNetCore.Mvc;

namespace Application.Api.Controllers.Home
{
    public class HomeController : BaseController
    {
        private readonly IDependency _dependency;

        public HomeController(IDependency dependency)
        {
            _dependency = dependency;
        }

        [HttpGet("/")]
        public IActionResult Welcome()
        {
            return Ok("Welcome!");
        }
    }
}
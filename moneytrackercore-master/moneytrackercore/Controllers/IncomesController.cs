using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace moneytrackercore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncomesController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new { Name = "Shawn", FavoriteColor = "Blue" });
        }
    }
}
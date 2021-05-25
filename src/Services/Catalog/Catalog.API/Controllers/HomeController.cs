using Catalog.API.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        public ICatalogContext catalogContext;
        public HomeController(ICatalogContext catalogContext)
        {
            this.catalogContext = catalogContext;
        }

        // GET: HomeController
        [HttpGet]
        public ActionResult Index()
        {
            return Ok();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MagentoIntegrationApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly ICustomer _service;
        public TestController(ICustomer service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Get(int id)
        {
            _service.GetAsync(id);
            return Ok();
        }

    }
}

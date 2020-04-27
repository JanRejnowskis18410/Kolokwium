using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyProject.Services;

namespace MyProject.Controllers
{
    [Route("api/medicaments")]
    [ApiController]
    public class MedicamentsController : ControllerBase
    {
        private IDBService _service;

        public MedicamentsController(IDBService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public IActionResult getMedicament(int id)
        {
            return Ok(_service.GetMedicament(id));
        }
    }
}
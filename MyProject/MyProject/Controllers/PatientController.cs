using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyProject.Services;

namespace MyProject.Controllers
{
    [Route("api/patients")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private IDBService _service;

        public PatientController(IDBService service)
        {
            _service = service;
        }

        //[HttpDelete("{id}")]
        //public IActionResult deletePatient(int id)
        //{
        //    return Ok(null);
        //}
    }
}
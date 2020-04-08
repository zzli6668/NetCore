using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetCore.Models;
using NetCore.Services;

namespace NetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusinessController : ControllerBase
    {
        private readonly BusinessService _businessService;

        public BusinessController(BusinessService businessService)
        {
            _businessService = businessService;
        }

        [HttpGet]
        public ActionResult<List<Business>> Get() =>
            _businessService.Get();

        [HttpGet("{id:length(24)}", Name = "GetBook")]
        public ActionResult<Business> Get(string id)
        {
            var book = _businessService.Get(id);

            if (book == null)
            {
                return NotFound();
            }

            return book;
        }

        [HttpPost]
        public ActionResult<Business> Create(Business business)
        {
            _businessService.Create(business);

            return CreatedAtRoute("GetBook", new { id = business.Id.ToString() }, business);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Business businessIn)
        {
            var business = _businessService.Get(id);

            if (business == null)
            {
                return NotFound();
            }

            _businessService.Update(id, businessIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var business = _businessService.Get(id);

            if (business == null)
            {
                return NotFound();
            }

            _businessService.Remove(business.Id);

            return NoContent();
        }
    }
}
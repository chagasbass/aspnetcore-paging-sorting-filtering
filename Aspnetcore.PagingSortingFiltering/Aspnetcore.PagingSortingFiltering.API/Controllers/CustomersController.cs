using Aspnetcore.PagingSortingFiltering.API.Bases;
using Aspnetcore.PagingSortingFiltering.API.Entities;
using Aspnetcore.PagingSortingFiltering.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace Aspnetcore.PagingSortingFiltering.API.Controllers
{
    [Route("v1/customers")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomersController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetAllWithFilterAsync([FromQuery] CustomerRequestParameters customerRequestParameters)
        {
            if (!customerRequestParameters.ValidateAgeRange)
                return BadRequest("Max age can't be less than min age.");

            var customers = await _customerRepository.GetCustomersByFiltersAsync(customerRequestParameters);

            Response.Headers.Add("X-Pagination",
                                 JsonSerializer.Serialize(customers.MetaData));

            return Ok(customers);
        }
    }
}

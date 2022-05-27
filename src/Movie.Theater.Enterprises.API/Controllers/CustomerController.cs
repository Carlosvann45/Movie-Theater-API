using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Movie.Theater.Enterprises.Models.DTOs;
using Movie.Theater.Enterprises.Providers.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Movie.Theater.Enterprises.API.Controllers
{
    /// <summary>
    /// Customer controller to proccess request to the user database
    /// </summary>
    [ApiController]
    [Route(Constants.CUSTOMER_ENDPOINT)]
    [Produces(Constants.APP_PRODUCES)]
    [Consumes(Constants.APP_CONSUMES)]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerProvider customerProvider;
        private readonly ILogger<CustomerController> logger;
        private readonly IMapper mapper;

        public CustomerController(ICustomerProvider customerProvider, ILogger<CustomerController> logger, IMapper mapper)
        {
            this.customerProvider = customerProvider;
            this.logger = logger;
            this.mapper = mapper;
        }

        [HttpGet(Constants.LOGIN_ENDPOINT)]
        public async Task<ActionResult<string>> LoginInCustomerAsync([Required] string email, [Required] string password)
        {
            logger.LogInformation(Constants.LOG_LOG_IN_CUSTOMER);

            var authorizationToken = await customerProvider.LogInCustomerAsync(email, password);

            return Ok(authorizationToken);
        }


        [HttpPost(Constants.REGISTER_ENDPOINT)]
        public async Task<ActionResult<CustomerDTO>> RegisterCustomerAsync([FromBody] CustomerDTO customerToCreate)
        {
            logger.LogInformation(Constants.LOG_REGISTER_CUSTOMER);

            var createdCustomer = await customerProvider.RegisterCustomerAsync(customerToCreate);
            var customerDto = mapper.Map<CustomerDTO>(createdCustomer);

            return Created(Constants.CUSTOMER_ENDPOINT, customerDto);
        }


    }
}

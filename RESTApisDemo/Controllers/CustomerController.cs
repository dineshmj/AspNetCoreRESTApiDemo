using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using RESTApisDemo.Authorization;
using RESTApisDemo.Business;
using RESTApisDemo.Models;

namespace RESTApisDemo.Controllers
{
    [Authorize]
    [ApiController]
    [Route ("api/[controller]")]
    public sealed class CustomerController
        : ControllerBase
    {
        private readonly ICustomerBizFacade customerBizFacade;

        public CustomerController (ICustomerBizFacade customerBizFacade)
        {
            this.customerBizFacade = customerBizFacade;
        }

        // GET: api/<CustomerController>
        [HttpGet]
        public IActionResult GetAll ()
        {
            // Get all
            var customers = this.customerBizFacade.GetAll ();

            // Are there any?
            if (customers?.Count > 0)
            {
                return base.Ok (customers);     // Returns 200 - OK
            }

            // No.
            return base.NotFound ();            // Returns 404 - Not Found
        }

        // GET api/<CustomerController>/5
        [HttpGet ("{id}")]
        public IActionResult GetBy (int id)
        {
            // Get matching customer.
            var existingCustomer = this.customerBizFacade.GetBy (id);

            // Does it exist?
            if (null == existingCustomer)
            {
                return base.NotFound ();        // Returns 404 - Not Found
            }

            // Yes.
            return base.Ok (existingCustomer);  // Returns 200 - OK
        }

        // POST api/<CustomerController>
        [HttpPost]
        [Authorize(Policy = PolicyHub.ADMIN_POLICY)]
        public IActionResult Post ([FromBody] Customer newCustomer)
        {
            // Attempt to create the resource.
            var resourceStatus
                = this.customerBizFacade.CreateNew
                    (
                        newCustomer,
                        out IDictionary<string, string>? validationErrors
                    );

            // Are there business validation errors?
            if (resourceStatus == ResourceStatus.ValidationFailure)
            {
                // Yes.
                return base.UnprocessableEntity (validationErrors);
                // Returns 422 - Unprocessable Entity
            }

            // Send 201 - Created, with "Location" header.
            return
                base.CreatedAtAction
                    (
                        nameof (this.GetBy),
                        new { newCustomer.Id },
                        null
                    );        // Returns 201 - Created, with Location response header
        }

        // PUT api/<CustomerController>/5
        [HttpPut]
        [Authorize (Policy = PolicyHub.ADMIN_POLICY)]
        public IActionResult Put ([FromBody] Customer modifiedCustomer)
        {
            // Attempt to update the resource.
            var resourceStatus
                = this.customerBizFacade.UpdateExisting
                    (
                        modifiedCustomer,
                        out IDictionary<string, string>? validationErrors
                    );

            // Are there business validation errors?
            if (resourceStatus == ResourceStatus.ValidationFailure)
            {
                // Yes.
                return this.UnprocessableEntity (validationErrors);
                // Returns 422 - Unprocessable Entity
            }

            // Resource not present?
            if (resourceStatus == ResourceStatus.NotFound)
            {
                return base.NotFound ();                                    // 404 - Not Found
            }

            return base.NoContent ();                                       // 204 - No Content
        }

        // DELETE api/<CustomerController>/5
        [HttpDelete ("{id}")]
        [Authorize (Policy = PolicyHub.ADMIN_POLICY)]
        public IActionResult Delete (int id)
        {
            // Attempt to delete the resource.
            var resourceStatus = this.customerBizFacade.DeleteBy (id);

            // Is the resource present?
            if (resourceStatus == ResourceStatus.NotFound)
            {
                // No.
                return base.NotFound ();                                    // 404 - Not Found
            }

            return base.NoContent ();                                       // 204 - No Content.
        }
    }
}
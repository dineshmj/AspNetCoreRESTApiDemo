using RESTApisDemo.DataAccess;
using RESTApisDemo.Models;

namespace RESTApisDemo.Business
{
    public sealed class CustomerBizFacade
        : ICustomerBizFacade
    {
        private readonly ICustomerBizValidator customerValidator;
        private readonly ICustomerDataAccess customerDataAccess;

        public CustomerBizFacade
            (
                ICustomerBizValidator customerBizValidator,
                ICustomerDataAccess customerDataAccess
            )
        {
            this.customerValidator = customerBizValidator;
            this.customerDataAccess = customerDataAccess;
        }

        // All customers.
        public IList<Customer>? GetAll ()
        {
            return
                this.customerDataAccess.GetAll ();
        }

        // Get specific customer.
        public Customer? GetBy (int id)
        {
            return
                this.customerDataAccess.GetBy (id);
        }

        // Create new.
        public ResourceStatus CreateNew (Customer newCustomer, out IDictionary<string, string>? validationFailures)
        {
            // Validate.
            validationFailures = this.customerValidator.Validate (newCustomer);

            // Did validation pass?
            if (validationFailures != null)
            {
                return ResourceStatus.ValidationFailure;
            }

            // Everything looks good. Create the new resource.
            return
                this.customerDataAccess.CreateNew (newCustomer);
        }

        // Update existing.
        public ResourceStatus UpdateExisting (Customer modifiedCustomer, out IDictionary<string, string>? validationFailures)
        {
            // Validate.
            validationFailures = this.customerValidator.Validate (modifiedCustomer);

            // Did validation pass?
            if (validationFailures?.Count > 0)
            {
                return ResourceStatus.ValidationFailure;
            }

            // Everything looks good. Updte the existing resource.
            return
                this.customerDataAccess.UpdateExisting (modifiedCustomer);
        }

        // Delete existing.
        public ResourceStatus DeleteBy (int id)
        {
            // Delete the existing resource.
            return
                this.customerDataAccess.Delete (id);
        }
    }
}
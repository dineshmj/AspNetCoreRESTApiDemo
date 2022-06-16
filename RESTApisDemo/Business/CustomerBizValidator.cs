using RESTApisDemo.Miscellaneous;
using RESTApisDemo.Models;

namespace RESTApisDemo.Business
{
    public sealed class CustomerBizValidator
        : ICustomerBizValidator
    {
        public IDictionary<string, string>? Validate (Customer customer)
        {
            var validationErrors = new Dictionary<string, string> ();

            // Null?
            if (null == customer)
            {
                validationErrors.Add (Constants.INSTANCE, "Customer instance cannot be NULL.");
                return validationErrors;
            }

            // First name.
            if (string.IsNullOrEmpty (customer.FirstName))
            {
                validationErrors.Add (nameof (customer.FirstName), "First name cannot be empty or NULL.");
            }

            // Last name.
            if (string.IsNullOrEmpty (customer.LastName))
            {
                validationErrors.Add (nameof (customer.LastName), "Last name cannot be empty or NULL.");
            }

            // Social Security Number.
            if (string.IsNullOrEmpty (customer.SSN))
            {
                validationErrors.Add (nameof (customer.SSN), "Social Security Number cannot be empty or NULL.");
            }

            // Customer must be at least 18 years old.
            if (customer.DateOfBirth.AddYears (18) > DateTime.Today)
            {
                validationErrors.Add (nameof (customer.DateOfBirth), "Customer must be at least 18 years old.");
            }

            // Are there validation failures?
            return
                validationErrors.Count > 0
                    ? validationErrors
                    : null;
        }
    }
}
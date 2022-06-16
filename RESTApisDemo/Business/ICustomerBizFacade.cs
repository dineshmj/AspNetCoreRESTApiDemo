using RESTApisDemo.Models;

namespace RESTApisDemo.Business
{
    public interface ICustomerBizFacade
    {
        Customer? GetBy (int id);
        IList<Customer>? GetAll ();
        ResourceStatus CreateNew (Customer newCustomer, out IDictionary<string, string>? validationFailures);
        ResourceStatus UpdateExisting (Customer modifiedCustomer, out IDictionary<string, string>? validationFailures);
        ResourceStatus DeleteBy (int id);
    }
}
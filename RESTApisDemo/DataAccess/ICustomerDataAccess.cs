using RESTApisDemo.Models;

namespace RESTApisDemo.DataAccess
{
    public interface ICustomerDataAccess
    {
        Customer? GetBy (int id);
        IList<Customer>? GetAll ();
        bool Exists (int id);
        ResourceStatus CreateNew (Customer customer);
        ResourceStatus UpdateExisting (Customer customer);
        ResourceStatus Delete (int id);
    }
}
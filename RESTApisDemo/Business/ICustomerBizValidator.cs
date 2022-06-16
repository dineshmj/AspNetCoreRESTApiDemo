using RESTApisDemo.Models;

namespace RESTApisDemo.Business
{
    public interface ICustomerBizValidator
    {
        IDictionary<string, string>? Validate (Customer customer);
    }
}
using RESTApisDemo.Models;

namespace RESTApisDemo.DataAccess
{
    public sealed class CustomerDataAccess
        : ICustomerDataAccess
    {
        private static readonly IList<Customer> customers
            = new List<Customer> ()
            {
                new Customer { FirstName = "Tom", LastName = "Hank",            Id = 0, SSN = "123-45-6789", DateOfBirth = new DateTime (1956, 7, 9) },
                new Customer { FirstName = "Tommy", LastName = "Lee Jones",     Id = 1, SSN = "234-56-7890", DateOfBirth = new DateTime (1946, 9, 15) },
                new Customer { FirstName = "Will", LastName = "Smith",          Id = 2, SSN = "345-67-8901", DateOfBirth = new DateTime (1968, 9, 25) },
                new Customer { FirstName = "Matthew", LastName = "McConaughey", Id = 3, SSN = "456-78-9012", DateOfBirth = new DateTime (1969, 11, 4) }
            };
        private static int customersCount = 4;

        public Customer? GetBy (int id)
        {
            return
                customers.FirstOrDefault (c => c.Id == id);
        }

        public IList<Customer> GetAll ()
        {
            return
                customers;
        }

        public bool Exists (int id)
        {
            return
                customers.Any (c => c.Id == id);
        }

        public ResourceStatus CreateNew (Customer newCustomer)
        {
            newCustomer.Id = customersCount++;
            customers.Add (newCustomer);

            return ResourceStatus.Created;
        }

        public ResourceStatus UpdateExisting (Customer modifiedCustomer)
        {
            var existingCustomer = this.GetBy (modifiedCustomer.Id);

            if (null == existingCustomer)
            {
                return ResourceStatus.NotFound;
            }

            existingCustomer.FirstName = modifiedCustomer.FirstName;
            existingCustomer.LastName = modifiedCustomer.LastName;
            existingCustomer.SSN = modifiedCustomer.SSN;
            existingCustomer.DateOfBirth = modifiedCustomer.DateOfBirth;

            return ResourceStatus.Updated;
        }

        public ResourceStatus Delete (int id)
        {
            var existingCustomer = this.GetBy (id);

            if (null == existingCustomer)
            {
                return ResourceStatus.NotFound;
            }

            customers.Remove (existingCustomer);
            return ResourceStatus.Deleted;
        }
    }
}
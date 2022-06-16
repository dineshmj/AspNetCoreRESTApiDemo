namespace RESTApisDemo.Models
{
    public sealed class Customer
    {
        public int Id { get; set; } 
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string SSN { get; set; } = string.Empty;
    }
}
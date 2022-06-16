namespace RESTApisDemo.Models
{
    public sealed class Credential
    {
        public string? FullName { get; set; }
        public string? UserId { get; set; }
        public string? Password { get; set; }
        public IList<string>? Roles { get; set; }
    }
}
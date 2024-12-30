namespace SupplierAPI.Domain.Models
{
    public class CreateUserModel
    {
        public string TenantName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}

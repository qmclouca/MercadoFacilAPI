namespace Domain.Entities
{
    public class User : BaseEntity
    {
        public User()
        {
            Addresses = new List<Address>();
            UserAddresses = new List<UserAddress>();
        }
        public List<Address>? Addresses {get; set;}
        public List<UserAddress>? UserAddresses { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Role { get; set; }        
      
    }
}
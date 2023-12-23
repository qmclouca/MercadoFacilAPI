namespace Domain.Entities
{
    public class User : BaseEntity
    {
        internal readonly object Addresses;

        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Role { get; set; }
        public List<UserAddress>? UserAddresses { get; set; }
        public User() { }
    }
}

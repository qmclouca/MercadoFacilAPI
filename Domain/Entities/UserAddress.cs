namespace Domain.Entities
{
    public class UserAddress: BaseEntity
    {
        public Guid UserId { get; set; }
        public User? User { get; set; }
        public Guid AddressId { get; set; }
        public Address? Address { get; set; }
    }
}

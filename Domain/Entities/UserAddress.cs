namespace Domain.Entities
{
    public class UserAddress: BaseEntity
    {
        public Guid UserId { get; set; }       
        public Guid AddressId { get; set; }        
    }
}

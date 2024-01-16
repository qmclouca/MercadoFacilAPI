namespace Domain.Entities
{
    public class BaseEntity
    {
        public Guid Id { get; set; }        
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool Active { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}

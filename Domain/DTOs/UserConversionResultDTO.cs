using Domain.Entities;

namespace Domain.DTOs
{
    public class UserConversionResultDTO
    {
        public Domain.Entities.User? user { get; set; }
        public List<Domain.Entities.Address> Addresses { get; set; }
        public List<UserAddress> UserAddresses { get; set; }
        public string ObservedShares { get; set; }

        public UserConversionResultDTO()
        {
            Addresses = new List<Domain.Entities.Address>();
            UserAddresses = new List<UserAddress>();
        }
    }
}

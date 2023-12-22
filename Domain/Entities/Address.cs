using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Address: BaseEntity
    {
        public string? Street { get; set; }
        public string? Number { get; set; }
        public string? Complement { get; set; }
        public string? Neighborhood { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
        public string? ZipCode { get; set; }
        public List<UserAddress>? UserAddresses { get; set; }
    }
}

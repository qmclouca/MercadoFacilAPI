using Domain.DTOs.Address;
using Domain.DTOs.User;
using Domain.Entities;

namespace UnitTests.Generators
{
    public class FakeUsers
    {
        public CreateUserDTO CreateFakeUserDTO()
        {
            var addressGenerator = CreateAddressGenerator();
            var userGenerator = new Faker<CreateUserDTO>()
                .RuleFor(u => u.Name, f => f.Person.FullName)
                .RuleFor(u => u.Email, f => f.Person.Email)
                .RuleFor(u => u.Password, f => f.Internet.Password())
                .RuleFor(u => u.Role, f => f.PickRandomParam("User", "Admin"))
                .RuleFor(u => u.Addresses, f => addressGenerator.Generate(3));
            return userGenerator.Generate();
        }

        public User CreateFakeUser()
        {
            var addressGenerator = new Faker<Address>()
                .RuleFor(a => a.Street, f => f.Address.StreetName())
                .RuleFor(a => a.Number, f => f.Address.BuildingNumber())
                .RuleFor(a => a.Complement, f => f.Address.SecondaryAddress())
                .RuleFor(a => a.Neighborhood, f => f.Address.County())
                .RuleFor(a => a.City, f => f.Address.City())
                .RuleFor(a => a.State, f => f.Address.StateAbbr())
                .RuleFor(a => a.Country, f => f.Address.Country())
                .RuleFor(a => a.ZipCode, f => f.Address.ZipCode())
                .RuleFor(a => a.District, f => f.Address.CitySuffix());
            
            var userGenerator = new Faker<User>()
                .RuleFor(u => u.Id, f => f.Random.Guid()) 
                .RuleFor(u => u.Name, f => f.Person.FullName)
                .RuleFor(u => u.Email, f => f.Person.Email)
                .RuleFor(u => u.Password, f => f.Internet.Password())
                .RuleFor(u => u.Role, f => f.PickRandomParam("User", "Admin"))
                .RuleFor(u => u.CreatedAt, f => f.Date.Past())
                .RuleFor(u => u.Active, f => f.Random.Bool())
                .RuleFor(u => u.IsDeleted, f => f.Random.Bool())
                .CustomInstantiator(f =>
                {
                    var user = new User();
                    var addresses = addressGenerator.Generate(3); 
                    foreach (var address in addresses)
                    {
                        address.Id = Guid.NewGuid(); 
                        user.Addresses!.Add(address);

                        var userAddress = new UserAddress
                        {
                            UserId = user.Id,
                            AddressId = address.Id,
                            Active = true,
                            IsDeleted = false
                        };
                        user.UserAddresses!.Add(userAddress);
                    }
                    return user;
                });

            User user = userGenerator.Generate(); // Generate the user
            return user;
        }


        internal IEnumerable<User> CreateMany(int v)
        {
            List<User> users = new List<User>();

            for (int i = 0; i < v; i++)
            {
                users.Add(CreateFakeUser());
            }
            return users;
        }

        private Faker<CreateAddressDTO> CreateAddressGenerator()
        {
            return new Faker<CreateAddressDTO>()
                .RuleFor(a => a.Street, f => f.Address.StreetName())
                .RuleFor(a => a.Number, f => f.Address.BuildingNumber())
                .RuleFor(a => a.Complement, f => f.Address.SecondaryAddress())
                .RuleFor(a => a.District, f => f.Address.County())
                .RuleFor(a => a.City, f => f.Address.City())
                .RuleFor(a => a.State, f => f.Address.StateAbbr())
                .RuleFor(a => a.Country, f => f.Address.Country())
                .RuleFor(a => a.ZipCode, f => f.Address.ZipCode())
                .RuleFor(a => a.Neighborhood, f => f.Address.Direction());
        }
    }   
}
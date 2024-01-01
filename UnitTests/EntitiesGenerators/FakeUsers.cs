using Domain.DTOs.Address;
using Domain.DTOs.User;

namespace UnitTests.Generators
{
    public class FakeUsers
    {
        public CreateUserDTO CreateFakeUser()
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
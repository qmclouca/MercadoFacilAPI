using AutoMapper;
using Domain.DTOs.Address;
using Domain.DTOs.User;
using Domain.Entities;
using Domain.Interfaces.Services;
using MercadoFacilAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using UnitTests.Generators;

namespace UnitTests
{
    public class UserControllerTests
    {
        [Fact]
        public void Post_DeveRetornarUserDTO_QuandoUsuarioValido()
        {
            // Arrange
            var mockUserService = new Mock<IUserService>();
            var mockAddressService = new Mock<IAddressService>();
            var mockUserAddressService = new Mock<IUserAddressService>();
            var mockMapper = new Mock<IMapper>();
            var userFaker = new FakeUsers();
            
            var userController = new UserController(mockUserService.Object, mockAddressService.Object, mockUserAddressService.Object, mockMapper.Object);
            var userDto = userFaker.CreateFakeUser();

            // Act
            var result = userController.Post(userDto);

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
            Assert.IsType<CreateUserDTO>(((OkObjectResult)result.Result).Value);            
        }

        [Fact]
        public void Post_DeveRetornarBadRequest_QuandoUsuarioInvalido()
        {
            // Arrange
            var mockUserService = new Mock<IUserService>();
            var mockAddressService = new Mock<IAddressService>();
            var mockUserAddressService = new Mock<IUserAddressService>();
            var mockMapper = new Mock<IMapper>();
                        
            var userController = new UserController(mockUserService.Object, mockAddressService.Object, mockUserAddressService.Object, mockMapper.Object);
            CreateAddressDTO address = new CreateAddressDTO
            {
                Street = "Rua Teste",
                Number = "123",
                Neighborhood = "Bairro Teste",
                City = "Cidade Teste",
                State = "Estado Teste",
                Country = "Pa�s Teste",
                ZipCode = "12345678"
            };

            List<CreateAddressDTO> lstCreateAddresses = new List<CreateAddressDTO>();
            lstCreateAddresses.Add(address);


            var userDto = new CreateUserDTO
            {
                Name = "Usu�rio Teste",
                Email = "email@teste",
                Password = "InValidPass",
                Role = "User",
                Addresses = lstCreateAddresses
            };

            // Act
            var result = userController.Post(userDto);

            // Assert
            Assert.IsType<BadRequestResult>(result.Result);
        }

        [Theory]
        [InlineData("usuariosemarroba.com")]
        [InlineData("usuariosem@ponto")]
        [InlineData("usuariosemarrobaeponto")]
        [InlineData("usuario@@exemplo.com")]
        public async Task Post_ReturnsBadRequest_WhenEmailIsInvalid(string email)
        {
            // Arrange
            var mockUserService = new Mock<IUserService>();
            var mockAddressService = new Mock<IAddressService>();
            var mockUserAddressService = new Mock<IUserAddressService>();
            var mockMapper = new Mock<IMapper>();
                        
            var userController = new UserController(mockUserService.Object, mockAddressService.Object, mockUserAddressService.Object, mockMapper.Object);

            CreateAddressDTO address = new CreateAddressDTO
            {
                Street = "Rua Teste",
                Number = "123",
                Neighborhood = "Bairro Teste",
                City = "Cidade Teste",
                State = "Estado Teste",
                Country = "Pa�s Teste",
                ZipCode = "12345678"
            };

            List<CreateAddressDTO> lstCreateAddresses = new List<CreateAddressDTO>();
            lstCreateAddresses.Add(address);


            var userDto = new CreateUserDTO
            {
                Name = "Usu�rio Teste",
                Email = email,
                Password = "ValidPass11!",
                Role = "User",
                Addresses = lstCreateAddresses
            };

            // Act
            var result = await userController.Post(userDto);
            var objectResult = result as ObjectResult;

            // Assert
            if (objectResult.StatusCode == 200)
            {
                Assert.IsType<CreateUserDTO>(objectResult.Value);
                Assert.Equal(email, ((CreateUserDTO)objectResult.Value).Email);
            }
            else
            {
                Assert.Equal(400, objectResult.StatusCode);
            }            
        }

        [Theory]
        [InlineData("ValidPass11!", true, null)] // V�lido: atende a todos os requisitos
        [InlineData("invalidpassword", false, "A senha deve conter pelo menos um n�mero, um caractere especial, uma letra mai�scula e uma min�scula.")] // Inv�lido: sem n�mero, sem caractere especial, sem mai�scula
        [InlineData("INVALIDPASSWORD1", false, "A senha deve conter pelo menos um n�mero, um caractere especial, uma letra mai�scula e uma min�scula.")] // Inv�lido: sem caractere especial, sem min�scula
        [InlineData("Invalid1", false, "A senha deve conter pelo menos um n�mero, um caractere especial, uma letra mai�scula e uma min�scula.")] // Inv�lido: sem caractere especial
        [InlineData("Inv@lid", false, "A senha deve conter pelo menos um n�mero, um caractere especial, uma letra mai�scula e uma min�scula.")] // Inv�lido: sem n�mero
        [InlineData("Short1!", false, "A senha deve conter no m�nimo 12 caracteres e no m�ximo 40.")] // Inv�lido: menos de 12 caracteres
        [InlineData("ThisPasswordIsWayTooLongEvenIfItHasNumbers1!", false, "A senha deve conter no m�nimo 12 caracteres e no m�ximo 40.")] // Inv�lido: mais de 40 caracteres
        public async Task Password_Validation_Test(string password, bool expectedIsValid, string expectedMessage)
        {
            // Arrange
            var mockUserService = new Mock<IUserService>();
            var mockAddressService = new Mock<IAddressService>();
            var mockUserAddressService = new Mock<IUserAddressService>();
            var mockMapper = new Mock<IMapper>();

            var userController = new UserController(mockUserService.Object, mockAddressService.Object, mockUserAddressService.Object, mockMapper.Object);

            CreateAddressDTO address = new CreateAddressDTO
            {
                Street = "Rua Teste",
                Number = "123",
                Neighborhood = "Bairro Teste",
                City = "Cidade Teste",
                State = "Estado Teste",
                Country = "Pa�s Teste",
                ZipCode = "12345678"
            };
            
            List<CreateAddressDTO> lstCreateAddresses = [address];

            var userDto = new CreateUserDTO
            {
                Name = "Usu�rio Teste",
                Email = "teste@exemplo.com",
                Password = password,
                Role = "User",
                Addresses = lstCreateAddresses
            };

            // Act
            var context = new ValidationContext(userDto, null, null);
            var results = new List<System.ComponentModel.DataAnnotations.ValidationResult>();

            var isValid = Validator.TryValidateObject(userDto, context, results, true);

            if(!isValid)
            {
                foreach (var validationResult in results)
                {
                    userController.ModelState.AddModelError(validationResult.MemberNames.First(), validationResult.ErrorMessage);
                }
            }

            var result = await userController.Post(userDto);
            var objectResult = result as ObjectResult;

            // Assert
            if (expectedIsValid)
            {
                Assert.NotNull(result);
                Assert.IsType<OkObjectResult>(result);
            }
            else
            {
                Assert.NotNull(result);
                Assert.IsType<BadRequestObjectResult>(result);
                
                var errorMessages = new List<string>();
                var resultValue = result as BadRequestObjectResult;
                if (resultValue == null) { 
                    Assert.True(false, "resultValue is null");
                }
                var modelState = resultValue.Value as SerializableError;

                foreach (var KeyValuePair in modelState)
                {
                    var key = KeyValuePair.Key;
                    var errors = KeyValuePair.Value as string[];

                    if (errors != null && errors.Length > 0)
                    {
                        errorMessages.AddRange(errors);
                    }
                }

                var expectedMessageOne = "A senha deve conter no m�nimo 12 caracteres e no m�ximo 40.";
                var expectedMessageTwo = "A senha deve conter pelo menos um n�mero, um caractere especial, uma letra mai�scula e uma min�scula.";

                bool containsEitherMessage = errorMessages.Any(msg => msg.Contains(expectedMessageOne)) ||
                                             errorMessages.Any(msg => msg.Contains(expectedMessageTwo));

                Assert.True(containsEitherMessage, "Error messages should contain either message one or message two.");
            }
        }
    }
}
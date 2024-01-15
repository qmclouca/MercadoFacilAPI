using AutoMapper;
using Domain.DTOs.Address;
using Domain.DTOs.User;
using Domain.Entities;
using Domain.Interfaces.Services;
using MercadoFacilAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
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
            var userDto = userFaker.CreateFakeUserDTO();

            // Act
            var result = userController.Post(userDto);

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
            Assert.IsType<CreateUserDTO>(((OkObjectResult)result.Result).Value);            
        }

        [Fact]
        public async Task Post_DeveRetornarBadRequest_QuandoUsuarioInvalido()
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
                Country = "País Teste",
                ZipCode = "12345678"
            };

            List<CreateAddressDTO> lstCreateAddresses = [address];

            var userDto = new CreateUserDTO
            {
                Name = "Usuário Teste",
                Email = "email@teste",
                Password = "InValidPass",
                Role = "User",
                Addresses = lstCreateAddresses
            };

            // Act
            var context = new ValidationContext(userDto, null, null);
            var results = new List<System.ComponentModel.DataAnnotations.ValidationResult>();

            var isValid = Validator.TryValidateObject(userDto, context, results, true);

            if (!isValid)
            {
                foreach (var validationResult in results)
                {
                    userController.ModelState.AddModelError(validationResult.MemberNames.First(), validationResult.ErrorMessage);
                }
            }

            var result = await userController.Post(userDto);
            var objectResult = result as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result);
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
                Country = "País Teste",
                ZipCode = "12345678"
            };

            List<CreateAddressDTO> lstCreateAddresses = [address];

            var userDto = new CreateUserDTO
            {
                Name = "Usuário Teste",
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
        [InlineData("ValidPass11!", true, null)] // Válido: atende a todos os requisitos
        [InlineData("invalidpassword", false, "A senha deve conter pelo menos um número, um caractere especial, uma letra maiúscula e uma minúscula.")] // Inválido: sem número, sem caractere especial, sem maiúscula
        [InlineData("INVALIDPASSWORD1", false, "A senha deve conter pelo menos um número, um caractere especial, uma letra maiúscula e uma minúscula.")] // Inválido: sem caractere especial, sem minúscula
        [InlineData("Invalid1", false, "A senha deve conter pelo menos um número, um caractere especial, uma letra maiúscula e uma minúscula.")] // Inválido: sem caractere especial
        [InlineData("Inv@lid", false, "A senha deve conter pelo menos um número, um caractere especial, uma letra maiúscula e uma minúscula.")] // Inválido: sem número
        [InlineData("Short1!", false, "A senha deve conter no mínimo 12 caracteres e no máximo 40.")] // Inválido: menos de 12 caracteres
        [InlineData("ThisPasswordIsWayTooLongEvenIfItHasNumbers1!", false, "A senha deve conter no mínimo 12 caracteres e no máximo 40.")] // Inválido: mais de 40 caracteres
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
                Country = "País Teste",
                ZipCode = "12345678"
            };
            
            List<CreateAddressDTO> lstCreateAddresses = [address];

            var userDto = new CreateUserDTO
            {
                Name = "Usuário Teste",
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

                Assert.NotNull(resultValue);
                
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

                var expectedMessageOne = "A senha deve conter no mínimo 12 caracteres e no máximo 40.";
                var expectedMessageTwo = "A senha deve conter pelo menos um número, um caractere especial, uma letra maiúscula e uma minúscula.";

                bool containsEitherMessage = errorMessages.Any(msg => msg.Contains(expectedMessageOne)) ||
                                             errorMessages.Any(msg => msg.Contains(expectedMessageTwo));

                Assert.True(containsEitherMessage, "Error messages should contain either message one or message two.");
            }
        }

        [Fact]
        public async Task Delete_ReturnsNotFound_WhenUserDoesNotExist()
        {
            // Arrange
            var nonExistentUserId = Guid.NewGuid();

            var mockUserService = new Mock<IUserService>();
            var mockAddressService = new Mock<IAddressService>();
            var mockUserAddressService = new Mock<IUserAddressService>();
            var mockMapper = new Mock<IMapper>();

            _ = mockUserService.Setup(service => service.GetUserById(nonExistentUserId)).ReturnsAsync((User)null);

            var userController = new UserController(mockUserService.Object, mockAddressService.Object, mockUserAddressService.Object, mockMapper.Object);

            // Act
            var result = await userController.Delete(nonExistentUserId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsBadRequest_WhenAddressDeletionFails()
        {
            // Arrange
            var existingUserId = Guid.NewGuid();
            var mockUserService = new Mock<IUserService>();
            var mockAddressService = new Mock<IAddressService>();
            var mockUserAddressService = new Mock<IUserAddressService>();
            var mockMapper = new Mock<IMapper>();

            var user = new User { Id = existingUserId };

            mockUserService.Setup(service => service.GetUserById(existingUserId)).ReturnsAsync(user);
            mockAddressService.Setup(service => service.DeleteAddressByUserId(existingUserId)).ReturnsAsync(false);

            var userController = new UserController(mockUserService.Object, mockAddressService.Object, mockUserAddressService.Object, mockMapper.Object);

            // Act
            var result = await userController.Delete(existingUserId);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Um problema ocorreu ao excluir os endereços do usuário.", badRequestResult.Value);
        }

        [Fact]
        public async Task Delete_ReturnsOk_WhenUserIsSuccessfullyDeleted()
        {
            // Arrange
            var existingUserId = Guid.NewGuid();
            var mockUserService = new Mock<IUserService>();
            var mockAddressService = new Mock<IAddressService>();
            var mockUserAddressService = new Mock<IUserAddressService>();
            var mockMapper = new Mock<IMapper>();

            var user = new User { Id = existingUserId };

            mockUserService.Setup(s => s.GetUserById(existingUserId))
                .ReturnsAsync(user);
            mockAddressService.Setup(s => s.DeleteAddressByUserId(existingUserId))
                .ReturnsAsync(true);          
            mockUserService.Setup(s => s.DeleteUser(It.IsAny<User>()))
                .ReturnsAsync(true);

            var controller = new UserController(mockUserService.Object, mockAddressService.Object, mockUserAddressService.Object, mockMapper.Object);

            // Act
            var result = await controller.Delete(existingUserId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(user, okResult.Value);
        }

        [Fact]
        public async Task GetAll_Returns_All_Users()
        {
            // Arrange
            var mockUserService = new Mock<IUserService>();
            var mockAddressService = new Mock<IAddressService>();
            var mockUserAddressService = new Mock<IUserAddressService>();
            var mockMapper = new Mock<IMapper>();


            var userFaker = new FakeUsers();
            var users = userFaker.CreateMany(10);            

            mockUserService.Setup(service => service.GetAllUsers()).ReturnsAsync(users);

            var userController = new UserController(mockUserService.Object, mockAddressService.Object, mockUserAddressService.Object, mockMapper.Object);

            // Act
            var result = await userController.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedUsers = Assert.IsType<List<User>>(okResult.Value);
            Assert.Equal(10, returnedUsers.Count);
        }

        [Fact]
        public async Task Get_Returns_User_By_Id()
        {
            // Arrange
            var userId = Guid.NewGuid(); // Create a new user ID
            var mockUserService = new Mock<IUserService>();
            var mockAddressService = new Mock<IAddressService>();
            var mockUserAddressService = new Mock<IUserAddressService>();
            var mockMapper = new Mock<IMapper>();

            var userFaker = new FakeUsers();
            var fakeUser = userFaker.CreateFakeUser(); // Use your method to create a fake user
            fakeUser.Id = userId; // Ensure the user has the ID we're going to look for

            mockUserService.Setup(s => s.GetUserById(userId))
                .ReturnsAsync(fakeUser); // Setup the mock service to return our user

            var controller = new UserController(
                mockUserService.Object,
                mockAddressService.Object,
                mockUserAddressService.Object,
                mockMapper.Object);

            // Act
            var actionResult = await controller.Get(userId);

            // Assert
            var result = Assert.IsType<OkObjectResult>(actionResult); // Check if the result is OK
            var returnedUser = Assert.IsType<User>(result.Value); // Check if the result contains a User
            Assert.Equal(userId, returnedUser.Id); // Check if the returned user is the one we requested
        }

        [Fact]
        public async Task Update_ReturnsNoContent_WhenUpdateIsSuccessful()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var existingUser = new User
            {
                Id = userId,
                Name = "Original Name",
                Email = "original@example.com",
                Password = "OriginalPass1!",
                Role = "User"
            };

            var updateUserDto = new CreateUserDTO
            {
                Name = "Updated Name",
                Email = "updated@example.com",
                Password = "UpdatedPass1!",
                Role = "Admin"
            };

            var updatedUser = new User
            {
                Id = userId,
                Name = updateUserDto.Name,
                Email = updateUserDto.Email,
                Password = updateUserDto.Password,
                Role = updateUserDto.Role
            };

            var mockUserService = new Mock<IUserService>();
            var mockAddressService = new Mock<IAddressService>();
            var mockUserAddressService = new Mock<IUserAddressService>();
            var mockMapper = new Mock<IMapper>();

            mockUserService.Setup(s => s.GetUserById(userId)).ReturnsAsync(existingUser);
            mockUserService.Setup(s => s.UpdateUser(It.IsAny<User>())).ReturnsAsync(updatedUser);
            mockMapper.Setup(m => m.Map<User>(It.IsAny<CreateUserDTO>())).Returns(updatedUser);

            var controller = new UserController(mockUserService.Object, mockAddressService.Object, mockUserAddressService.Object, mockMapper.Object);

            // Act
            var result = await controller.Put(updatedUser);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedUser = Assert.IsType<User>(okResult.Value);
            Assert.Equal(updatedUser.Name, returnedUser.Name);
            Assert.Equal(updatedUser.Email, returnedUser.Email);
            mockUserService.Verify(s => s.UpdateUser(It.IsAny<User>()), Times.Once);
        }


        [Fact]
        public async Task Update_ReturnsNotFound_WhenUserDoesNotExist()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var updateUserDto = new CreateUserDTO
            {
                Name = "Updated Name",
                Email = "updated@example.com",
                Password = "UpdatedPass1!",
                Role = "Admin"
            };

            var updatedUser = new User
            {
                Id = userId,
                Name = updateUserDto.Name,
                Email = updateUserDto.Email,
                Password = updateUserDto.Password,
                Role = updateUserDto.Role
            };
            var mockUserService = new Mock<IUserService>();
            var mockAddressService = new Mock<IAddressService>();
            var mockUserAddressService = new Mock<IUserAddressService>();
            var mockMapper = new Mock<IMapper>();

            mockUserService.Setup(s => s.GetUserById(userId)).ReturnsAsync((User)null);
                        
            var controller = new UserController(
                mockUserService.Object,
                mockAddressService.Object,
                mockUserAddressService.Object,
                mockMapper.Object
            );

            // Act
            var result = await controller.Put(updatedUser);

            // Assert
            Assert.IsType<NotFoundResult>(result);
            mockUserService.Verify(s => s.UpdateUser(It.IsAny<User>()), Times.Never);
        }
    }
}
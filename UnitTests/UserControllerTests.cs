using AutoMapper;
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
            var userDto = new CreateUserDTO();            

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
        public async void Post_ReturnsBadRequest_WhenEmailIsInvalid(string email)
        {
            // Arrange
            var mockUserService = new Mock<IUserService>();
            var mockAddressService = new Mock<IAddressService>();
            var mockUserAddressService = new Mock<IUserAddressService>();
            var mockMapper = new Mock<IMapper>();
                        
            var userController = new UserController(mockUserService.Object, mockAddressService.Object, mockUserAddressService.Object, mockMapper.Object);
            var userDto = new CreateUserDTO
            {
                Name = "Usu�rio Teste",
                Email = email,
                Password = "ValidPass11!",
                Role = "User"
            };

            // Act
            var result = await userController.Post(userDto);            

            // Assert            
            var badRequestResult = result as BadRequestResult;
            Assert.Equal(400, badRequestResult.StatusCode);
        }

        [Theory]
        [InlineData("ValidPass11!", true)] // V�lido: atende a todos os requisitos
        [InlineData("invalidpassword", false)] // Inv�lido: sem n�mero, sem caractere especial, sem mai�scula
        [InlineData("INVALIDPASSWORD1", false)] // Inv�lido: sem caractere especial, sem min�scula
        [InlineData("Invalid1", false)] // Inv�lido: sem caractere especial
        [InlineData("Inv@lid", false)] // Inv�lido: sem n�mero
        [InlineData("Short1!", false)] // Inv�lido: menos de 12 caracteres
        [InlineData("ThisPasswordIsWayTooLongEvenIfItHasNumbers1!", false)] // Inv�lido: mais de 40 caracteres
        public async void Password_Validation_Test(string password, bool expectedIsValid)
        {
            // Arrange
            var mockUserService = new Mock<IUserService>();
            var mockAddressService = new Mock<IAddressService>();
            var mockUserAddressService = new Mock<IUserAddressService>();
            var mockMapper = new Mock<IMapper>();

            var userController = new UserController(mockUserService.Object, mockAddressService.Object, mockUserAddressService.Object, mockMapper.Object);
            
            var userDto = new CreateUserDTO
            {
                Name = "Usu�rio Teste",
                Email = "teste@exemplo.com",
                Password = password,
                Role = "User"
            };

            // Act
            var result = await userController.Post(userDto);

            // Assert
            if (expectedIsValid)
            {
                Assert.IsType<CreatedAtActionResult>(result);
            }
            else
            {
                var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
                Assert.Equal(400, badRequestResult.StatusCode);
            }
        }
    }
}
using AutoMapper;
using Domain.DTOs.User;
using Domain.Interfaces.Services;
using MercadoFacilAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
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

    }
}
using AutoMapper;
using ImageBase.WebApp.Controllers;
using ImageBase.WebApp.Data.Dtos.AuthenticationDto;
using ImageBase.WebApp.Data.Models.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ImageBase.WebApp.UnitTests.FakeManagers;

namespace ImageBase.WebApp.UnitTests
{
    public class AuthenticationControllerTests
    {
        private AuthenticationController _authenticationController;
        private Mock<FakeSignInManager> _mockSignInManager;
        private Mock<FakeUserManager> _mockUserManager;

        public AuthenticationControllerTests()
        {
            var _loggerMock = new Mock<ILogger<AuthenticationController>>();
            var _mapperMock = new Mock<IMapper>();
            var _fakeRoleManager = new FakeRoleManager();
            _mockSignInManager = new Mock<FakeSignInManager>();
            _mockUserManager = new Mock<FakeUserManager>();

            _authenticationController =
                new AuthenticationController(_mockUserManager.Object, _fakeRoleManager, _mockSignInManager.Object, _loggerMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task LoginActionReturnsViewResultWithLoginModel_OnFailedSignIn() 
        {
            var userDto = new LoginUserDto()
            {
                Email = "random@mail.com",
                Password = "password123456?"
            };
            _mockSignInManager.Setup(exp => exp.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), true, false))
                .Returns(Task.FromResult(Microsoft.AspNetCore.Identity.SignInResult.Failed));

            var act = await _authenticationController.Login(userDto);

            var result = Assert.IsType<ViewResult>(act);            
            Assert.Equal(userDto, result.Model);
        }

        [Fact]
        public async Task LoginActionReturnsARedirect_OnSuccededSignIn()
        {
            var userDto = new LoginUserDto();
            _mockSignInManager.Setup(exp => exp.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), true, false))
                .Returns(Task.FromResult(Microsoft.AspNetCore.Identity.SignInResult.Success));

            var act = await _authenticationController.Login(userDto);

            var result = Assert.IsType<RedirectToActionResult>(act);
            Assert.Equal("Index", result.ActionName);
        }

        [Fact]
        public async Task LoginActionReturnsViewResultWithInvalidModel()
        {
            var userDto = new LoginUserDto();            
            _authenticationController.ModelState.AddModelError("fakeError", "fakeError");

            var act = await _authenticationController.Login(userDto);

            _mockSignInManager.Verify(s => s.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), true, false), Times.Never);
            var result = Assert.IsType<ViewResult>(act);
            Assert.Equal(userDto, result.Model);
        }

        [Fact]
        public async Task LoginActionReturnsARedirectResult_IfReturnUrlIsNotNull()
        {
            var userDto = new LoginUserDto();
            string someString = "somestring";
            _mockSignInManager.Setup(exp => exp.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), true, false))
                .Returns(Task.FromResult(Microsoft.AspNetCore.Identity.SignInResult.Success));

            var act = await _authenticationController.Login(userDto, someString);

            var result = Assert.IsType<RedirectResult>(act);
        }

        [Fact]
        public async Task RegisterActionReturnsViewResult_OnUserExcistingWithoutCreating()
        {
            var userDto = new RegisterUserDto();
            _mockUserManager.Setup(exp => exp.FindByNameAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(new User()));

            var act = await _authenticationController.Register(userDto);

            _mockUserManager.Verify(s => s.CreateAsync(It.IsAny<User>(), It.IsAny<string>()), Times.Never);
            var result = Assert.IsType<ViewResult>(act);
            Assert.Equal(userDto, result.Model);
        }

        [Fact]
        public async Task RegisterActionReturnsARedirect_OnCreateSucceded()
        {
            var userDto = new RegisterUserDto();
            _mockUserManager.Setup(exp => exp.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
                .Returns(Task.FromResult(IdentityResult.Success));

            var act = await _authenticationController.Register(userDto);

            _mockUserManager.Verify(s => s.AddToRoleAsync(It.IsAny<User>(), It.IsAny<string>()));
            var result = Assert.IsType<RedirectToActionResult>(act);
            Assert.Equal("Index", result.ActionName);
        }

        [Fact]
        public async Task RegisterActionReturnsVewResultWithRegisterModel_OnCreateFailed()
        {
            var userDto = new RegisterUserDto();
            _mockUserManager.Setup(exp => exp.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
                .Returns(Task.FromResult(IdentityResult.Failed()));

            var act = await _authenticationController.Register(userDto);

            var result = Assert.IsType<ViewResult>(act);
            Assert.Equal(userDto, result.Model);
        }

        [Fact]
        public async Task LogoutActionReturnsARedirect()
        {
            var act = await _authenticationController.Logout();

            var result = Assert.IsType<RedirectToActionResult>(act);
            Assert.NotNull(result);
        }

        [Fact]
        public void LoginReturnsViewResultWithUrl()
        {
            string returnUrl = "url";

            var act = _authenticationController.Login(returnUrl) as ViewResult;

            var result = Assert.IsType<ViewResult>(act);
            Assert.Equal(returnUrl, act.ViewData["ReturnUrl"]);
        }

    }   
}

using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoList.Application.Models;
using TodoList.Domain;
using TodoList.Shared;
using TodoList.Tests.Fixture;
using Xunit;
using FluentAssertions;

namespace TodoList.Tests.User
{
    public class AuthenticateUserTests : IClassFixture<UserFixture>
    {
        private readonly UserFixture fixture;

        public AuthenticateUserTests(UserFixture fixture)
        {
            this.fixture = fixture;
        }

        [Theory, MemberData(nameof(ValidData))]
        public async Task AuthenticateWithSuccess(LoginModel loginModel)
        {
            // Arrange
            var user = fixture.Mapper.Map<Domain.Entities.User>(loginModel);
            user.Profile = EProfile.Administrator;
            user.Id = 1;

            fixture.UserRepository.Setup(x => x
            .AuthenticateAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(() => user);

            // Act
            var result = await fixture.UserService.Authenticate(loginModel);

            // Assert
            result.Success.Should().BeTrue();
            result.Data.Should().NotBeNull();
            result.Data.Id.Should().NotBe(null);
        }

        [Theory, MemberData(nameof(InvalidData))]
        public async Task AuthenticateWithInvalidData(LoginModel loginModel)
        {
            // Arrange
            var user = fixture.Mapper.Map<Domain.Entities.User>(loginModel);

            fixture.UserRepository.Setup(x => x
            .AuthenticateAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(() => default);

            // Act
            var result = await fixture.UserService.Authenticate(loginModel);

            // Assert
            result.Success.Should().BeFalse();
            result.Data.Should().BeNull();
        }

        public static IEnumerable<object[]> ValidData
        {
            get
            {
                return new[]
                {
                    new object[]
                    {
                        new LoginModel()
                        {
                            Email = "test_adm@ubistart.com",
                            Password = Md5HashExtensions.CreateMD5("123456")
                        }
                    }
                };
            }
        }

        public static IEnumerable<object[]> InvalidData
        {
            get
            {
                return new[]
                {
                    //Wrong password
                    new object[]
                    {                        
                        new LoginModel()
                        {
                            Email = "test_adm@ubistart.com",
                            Password = "abcdef"
                        }
                    },
                    //Wrog email
                    new object[]
                    {
                        new LoginModel()
                        {
                            Email = "test_client@ubistart.com",
                            Password = "123456"
                        }
                    },
                    //Empty email
                    new object[]
                    {
                        new LoginModel()
                        {
                            Email = "test_client@ubistart.com",
                            Password = ""
                        }
                    },
                    //Empt,y password
                    new object[]
                    {
                        new LoginModel()
                        {
                            Email = "",
                            Password = "123456"
                        }
                    }
                };
            }
        }
    }    
}

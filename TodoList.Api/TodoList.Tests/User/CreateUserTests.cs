using FluentAssertions;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoList.Application.Models;
using TodoList.Tests.Fixture;
using Xunit;

namespace TodoList.Tests.User
{
    public class CreateUserTests : IClassFixture<UserFixture>
    {
        private readonly UserFixture fixture;
        public CreateUserTests(UserFixture fixture)
        {
            this.fixture = fixture;
        }

        [Theory, MemberData(nameof(ValidData))]
        public async Task CreateWithValidData(UserRegisterModel userRegisterModel)
        {
            // Act
            var result = await fixture.UserService.CreateUserAsync(userRegisterModel);

            // Assert
            result.Success.Should().BeTrue();
            result.Data.Should().NotBeNull();
        }

        [Theory, MemberData(nameof(InvalidData))]
        public async Task CreateWithInvalidData(UserRegisterModel userRegisterModel)
        {
            // Act
            var result = await fixture.UserService.CreateUserAsync(userRegisterModel);

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
                        new UserRegisterModel()
                        {
                            Email = "test@ubistart.com",
                            Password = "123456"
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
                    new object[]
                    {
                        //No email
                        new UserRegisterModel()
                        {
                            Email = null,
                            Password = "123456"
                        }
                    },
                    new object[]
                    {
                        //Empty email
                        new UserRegisterModel()
                        {
                            Email = "",
                            Password = "123456"
                        }
                    },
                    new object[]
                    {
                        //No password
                        new UserRegisterModel()
                        {
                            Email = "test@ubistart.com",
                            Password = null
                        }
                    },
                    new object[]
                    {
                        //Empty password
                        new UserRegisterModel()
                        {
                            Email = null,
                            Password = "123456"
                        }
                    },
                    new object[]
                    {
                        //Invalid email
                        new UserRegisterModel()
                        {
                            Email = "test",
                            Password = "123456"
                        }
                    },
                    new object[]
                    {
                        //Invalid password
                        new UserRegisterModel()
                        {
                            Email = "test@ubistart.com",
                            Password = "1"
                        }
                    }
                };
            }
        }

    }
}
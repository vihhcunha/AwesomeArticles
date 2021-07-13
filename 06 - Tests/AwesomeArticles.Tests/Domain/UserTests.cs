using AwesomeArticles.Domain.Entities;
using AwesomeArticles.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AwesomeArticles.Tests.Domain
{
    public class UserTests
    {
        [Fact(DisplayName = "01 - Build - Must build user correctly")]
        [Trait("Category", "User")]
        public void User_BuildUser_MustBuildCorrectly()
        {
            //Arrange & Act
            var nome = "Vinicius";
            var email = "vinicius.cunha@tests.com";
            var user = new User(nome, email, "test");

            //Assert
            Assert.IsType<User>(user);
            Assert.Equal(nome, user.Name);
            Assert.Equal(email, user.Email);
        }

        [Fact(DisplayName = "01 - Build - Must build user correctly")]
        [Trait("Category", "User")]
        public void User_BuildInvalidUser_MustNotBuild()
        {
            //Arrange & Act & Assert
            Assert.Throws<DomainException>(() => new User("", "email@teste.com", "test"));
            Assert.Throws<DomainException>(() => new User("Vinicius", "", "test"));
        }
    }
}

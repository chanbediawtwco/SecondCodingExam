using Moq;
using SecondCodingExam.Dto;
using SecondCodingExam.Models;
using SecondCodingExam.Services;
using SecondCodingExam.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondCodingExam.Tests
{
    public class AccountServiceTests
    {
        [Fact]
        public void GetUserById()
        {
            var MockAccountService = new AccountService(null, null, null, null, null);
            var result = MockAccountService.GetUserById(1);
            Assert.NotNull(result);
        }
        [Fact]
        public void Null_Return_Of_GetUserById()
        {
            var MockAccountService = new AccountService(null, null, null, null, null);
            var result = MockAccountService.GetUserById(0);
            Assert.Equal(result.Status, TaskStatus.Faulted);
        }
        [Fact]
        public void GetUserFullname()
        {
            var MockAccountService = new AccountService(null, null, null, null, null);
            User User = new User
            {
                Firstname = "TestFirstname",
                Lastname = "TestLastname",
            };
            var result = MockAccountService.GetUserFullname(User).Result;
            Assert.Equal(result, "TestFirstname TestLastname");
        }
        [Fact]
        public void Not_Equal_GetUserFullname()
        {
            var MockAccountService = new AccountService(null, null, null, null, null);
            User User = new User
            {
                Firstname = "TestFirstname",
                Lastname = "TestLastname",
            };
            var result = MockAccountService.GetUserFullname(User).Result;
            Assert.NotEqual(result, "Test");
        }
    }
}

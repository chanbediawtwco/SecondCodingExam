using Microsoft.AspNetCore.Mvc;
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
    public class JwtServiceTests
    {
        [Fact] 
        public async Task GenerateToken()
        {
            var mockJwtService = new Mock<IJwtService>();
            var User = new User 
            { 
                Id = 1,
                Email = "christhian.bedia@wtwco.com"
            };
            // Set up mock behavior for GenerateToken method
            mockJwtService.Setup(service => service.GenerateToken(It.IsAny<User>()))
                .ReturnsAsync("testToken");

            var Result = await mockJwtService.Object.GenerateToken(User);

            Assert.Equal("testToken", Result);
        }

        [Fact]
        public async Task Failed_GenerateToken()
        {
            var mockJwtService = new Mock<IJwtService>();
            var User = new User
            {
                Id = 1,
                Email = "christhian.bedia@wtwco.com"
            };
            // Set up mock behavior for GenerateToken method
            mockJwtService.Setup(service => service.GenerateToken(It.IsAny<User>()))
                .ReturnsAsync("testToken");

            var Result = await mockJwtService.Object.GenerateToken(User);

            Assert.NotEqual("testTokens", Result);
        }
    }
}

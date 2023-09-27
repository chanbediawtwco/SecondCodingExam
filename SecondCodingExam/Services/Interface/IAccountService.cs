using SecondCodingExam.Dto;
using SecondCodingExam.Models;

namespace SecondCodingExam.Services.Interface
{
    public interface IAccountService
    {
        public Task<User> GetUserById(int UserId);
        public Task<string> GetUserFullname(User User);
        public Task RegisterUser(UserRegistrationDto User);
        public Task<ResponseTokenDto> Login(LoginDto UserLogin);
    }
}

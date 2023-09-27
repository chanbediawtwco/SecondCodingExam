using SecondCodingExam.Models;

namespace SecondCodingExam.Services.Interface
{
    public interface IJwtService
    {
        public Task<int> GetUserIdFromToken();
        public Task<string> GenerateToken(User User);
    }
}
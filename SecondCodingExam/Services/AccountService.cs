using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using SecondCodingExam.Data;
using SecondCodingExam.Dto;
using SecondCodingExam.Models;
using SecondCodingExam.Services.Interface;
using System.Security.Cryptography;
using System.Text;

namespace SecondCodingExam.Services
{
    public class AccountService : IAccountService
    {
        private readonly IMapper _mapper;
        private readonly IJwtService _jwtService;
        private readonly SecondCodingExamDbContext _context;
        private readonly IValidator<LoginDto> _loginValidator;
        private readonly IValidator<UserRegistrationDto> _userRegistrationValidator;

        public AccountService(
            IMapper mapper, 
            IJwtService jwtService,
            SecondCodingExamDbContext context,
            IValidator<LoginDto> loginValidator,
            IValidator<UserRegistrationDto> userRegistrationValidator)
        {
            _mapper = mapper;
            _context = context;
            _jwtService = jwtService;
            _loginValidator = loginValidator;
            _userRegistrationValidator = userRegistrationValidator;
        }
        public async Task<User> GetUserById(int UserId)
        {
            User? User = await _context.Users
            .Where(User => User.Id == UserId)
            .FirstOrDefaultAsync();
            if (User == null) throw new Exception(Constants.UserNotFound);
            return User;
        }
        public async Task<string> GetUserFullname(User User)
            => await Task.FromResult($"{User.Firstname} {User.Lastname}");
        public async Task<ResponseTokenDto> Login(LoginDto UserLogin)
        {
            ValidationResult LoginValidator = await _loginValidator.ValidateAsync(UserLogin);
            if (!LoginValidator.IsValid) throw new Exception(Constants.InvalidInput);
            var DbUser = await GetUserProfile(UserLogin.Email, UserLogin.Password);
            if (DbUser == null) throw new Exception(Constants.UserNotFound);
            return await GenerateUserToken(DbUser);
        }
        public async Task RegisterUser(UserRegistrationDto User)
        {
            ValidationResult RegistrationValidator = await _userRegistrationValidator.ValidateAsync(User);
            if (!RegistrationValidator.IsValid) throw new Exception(Constants.InvalidInput);
            if (await GetUserProfile(User.Email, User.Password) != null) throw new Exception(Constants.UserExists);
            _context.Users.Add(await MapRegistrationToUser(User));
            await _context.SaveChangesAsync();
        }
        // Map the user registration dto to user model with hashed password
        private async Task<User> MapRegistrationToUser(UserRegistrationDto User)
        {
            User NewUser = await Task.FromResult(_mapper.Map<User>(User));
            NewUser.Password = Sha256(User.Password);
            return NewUser;
        }
        private async Task<User?> GetUserProfile(string Email, string Password)
            => await _context.Users
                .Where(User => User.Email == Email && User.Password == Sha256(Password))
                .FirstOrDefaultAsync();
        private async Task<ResponseTokenDto> GenerateUserToken(User DbUser)
            => await Task.FromResult(new ResponseTokenDto { Token = await _jwtService.GenerateToken(DbUser) });
        // Hash password
        private string Sha256(string Password)
        {
            var SHA = SHA256.Create();
            var PasswordSalt = $"{Password}";
            var Bytes = SHA.ComputeHash(Encoding.UTF8.GetBytes(PasswordSalt));
            var BytesHash = SHA.ComputeHash(Bytes);
            var Hash = Convert.ToBase64String(BytesHash);
            return Hash;
        }
    }
}

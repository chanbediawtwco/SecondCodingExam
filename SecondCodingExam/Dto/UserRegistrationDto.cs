using System.ComponentModel.DataAnnotations;

namespace SecondCodingExam.Dto
{
    public class UserRegistrationDto
    {
        [Required]
        public string Firstname { get; set; } = null!;

        [Required]
        public string Lastname { get; set; } = null!;

        [Required]
        public string Email { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;
    }
}

using SecondCodingExam.Models;

namespace SecondCodingExam.Dto
{
    public class CustomerDto
    {
        public int? Id { get; set; }

        public int? BenefitId { get; set; }

        public string? Firstname { get; set; } = null!;

        public string? Lastname { get; set; } = null!;

        public int? BasicSalary { get; set; }

        public DateTime Birthdate { get; set; }
    }
}
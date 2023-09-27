using SecondCodingExam.Models;

namespace SecondCodingExam
{
    public class Constants
    {
        public const string Benefit = "Benefit";
        public const string BenefitsHistory = "BenefitsHistory";
        public const string Calculation = "Calculation";
        public const string CalculationsHistory = "CalculationsHistory";
        public const string Customer = "Customer";
        public const string CustomersBenefitsHistory = "CustomersBenefitsHistory";
        public const string CustomersCurrentBenefit = "CustomersCurrentBenefit";
        public const string CustomersHistory = "CustomersHistory";
        public const string User = "User";

        // SQL database connection string
        public const string DefaultConnectionString = "DefaultConnection";

        // Cors policy origin name
        public const string CorsOrigin = "SecondCodingExamOrigins";

        // JWT token issuer settings
        public const string JwtIssuer = "Jwt:Issuer";
        public const string JwtAudience = "Jwt:Audience";
        public const string JwtKey = "Jwt:Key";
        public const string NameIdentifier = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier";

        // Http context accessor
        public const string Authorization = "Authorization";
        public const string Bearer = "Bearer ";

        // Validators error messages
        public const string MissingEmail = "Missing email information";
        public const string MissingPassword = "Missing password";
        public const string MissingFirstname = "Missing firstname";
        public const string MissingLastname = "Missing lastname";
        public const string MissingBenefit = "Missing benefit";
        public const string MissingSalary = "Missing salary";
        public const string MissingBirthdate = "Missing birthdate";
        public const string MissingGuaranteedIssue = "Missing guaranteed issue";
        public const string MissingMaxAgeLimit = "Missing max age limit";
        public const string MissingMinAgeLimit = "Missing min age limit";
        public const string MissingMaxRange = "Missing max range";
        public const string MissingMinRange = "Missing min range";
        public const string MissingIncrements = "Missing increments";

        // Http error message
        public const string InvalidInput = "Some information are invalid";
        public const string UserNotFound = "User not found";
        public const string UserExists = "User exists";
        public const string CustomerNotFound = "Customer not found";
        public const string NoChangesFound = "No changes found";
        public const string NoBenefitFound = "No benefit found";

        // Pagination settings
        public const int PageSize = 10;

        // Calculation option
        public const string ForApproval = "For Approval";
    }
}

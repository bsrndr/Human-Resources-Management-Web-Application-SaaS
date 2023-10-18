using System.ComponentModel.DataAnnotations;

namespace Presentation.Models.ModelMetaDatas
{
    public class ConfirmationNumberVmMetaData
    {
        public int Id { get; set; }
        public int ConformationNumber { get; set; }

        [Required(ErrorMessage = "Password can not be empty.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "Password must contain at least 8 characters including one uppercase letter, one lowercase letter, one digit, and one special character.")]
        public string Password { get; set; }
    }
}

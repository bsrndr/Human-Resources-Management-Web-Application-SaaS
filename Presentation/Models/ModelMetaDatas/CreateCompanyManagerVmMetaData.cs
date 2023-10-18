using Presentation.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace Presentation.Models.ModelMetaDatas
{
    public class CreateCompanyManagerVmMetaData
    {
        [Required(ErrorMessage = "Employee name is required")]
        [MaxLength(25, ErrorMessage = "Max length is 25 characters")]
        [RegularExpression(@"^[a-zA-ZığüşöçİĞÜŞÖÇ\s]*$", ErrorMessage = "Not valid")]
        public string FirstName { get; set; }
        [MaxLength(25, ErrorMessage = "Max length is 25 characters")]
        [RegularExpression(@"^[a-zA-ZığüşöçİĞÜŞÖÇ\s]*$", ErrorMessage = "Not valid")]
        public string? SecondFirstName { get; set; }
        [Required(ErrorMessage = "Employee last name is required")]
        [MaxLength(25, ErrorMessage = "Max length is 25 characters")]
        [RegularExpression(@"^[a-zA-ZığüşöçİĞÜŞÖÇ\s]*$", ErrorMessage = "Not valid")]
        public string LastName { get; set; }
        [MaxLength(25, ErrorMessage = "Max length is 25 characters")]
        [RegularExpression(@"^[a-zA-ZığüşöçİĞÜŞÖÇ\s]*$", ErrorMessage = "Not valid")]
        public string? SecondLastName { get; set; }
        [Required(ErrorMessage = "Employee last name is required")]
        [RegularExpression(@"^[0-9]{11}$", ErrorMessage = "Enter an 11-digit value consisting only of numbers.")]
        public string Tc { get; set; }
        [Range(1.00, double.MaxValue, ErrorMessage = "Salary must be greater than 0.")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Invalid salary format. For example: 12345.67")]
        public decimal Salary { get; set; }
        //[Range(typeof(DateTime), "1900-01-01", "2023-12-08", ErrorMessage = "Birth date must be between 1900 and 2023")]
        [RegularExpression(@"^(19\d{2}|20(?:0[0-4]|5[0-5]))-(0[1-9]|1[0-2])-(0[1-9]|[12]\d|3[01])$", ErrorMessage = "Invalid date")]
        public DateTime BirthDate { get; set; }
        [Required(ErrorMessage = "Place of Birth is required")]
        [MaxLength(20, ErrorMessage = "Max length is 20 characters")]
        public string PlaceOfBirth { get; set; }
        //[RegularExpression(@"^(2000|20(?:0[0-4]|5[0-5]))-(0[1-9]|1[0-2])-(0[1-9]|[12]\d|3[01])$", ErrorMessage = "Invalid date")]
        public DateTime DateOfRecruitment { get; set; }
        [Required(ErrorMessage = "Title is required")]
        [MaxLength(30, ErrorMessage = "Max length is 30 characters")]
        [RegularExpression(@"^[a-zA-ZığüşöçİĞÜŞÖÇ\s]*$", ErrorMessage = "Not valid")]
        public string Title { get; set; }
        public int DepartmentId { get; set; }
        [Required(ErrorMessage = "Address is required")]
        [MaxLength(250, ErrorMessage = "Max length is 250 characters")]
        [MinLength(3, ErrorMessage = "Min length is 3 characters")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Phone number cant be empty")]
        [RegularExpression(@"^(0[2-9][0-9]{2}-?[2-9][0-9]{2}-[0-9]{2}-[0-9]{2}|05[0-9]-?[2-9][0-9]{2}-[0-9]{2}-[0-9]{2})$", ErrorMessage = "True format : 05xx-xxx-xx-xx  0xxx-xxx-xx-xx ")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email { get; set; }
        public string ImagePath { get; set; }
        public IFormFile PostedPath { get; set; }

        public Status Status { get; set; }

        public string Gender { get; set; }

        public int CompanyId { get; set; }
    }
}

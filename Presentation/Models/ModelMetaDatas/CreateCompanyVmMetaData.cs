using Presentation.Models.Enums;
using Presentation.Utilities;
using System.ComponentModel.DataAnnotations;

namespace Presentation.Models.ModelMetaDatas
{
    public class CreateCompanyVmMetaData
    {
        [Required(ErrorMessage = "Company name is required")]
        [MaxLength(50, ErrorMessage = "Max length is 50 characters")]
        [MinLength(3, ErrorMessage = "Min length is 3 characters")]
        public string CompanyName { get; set; }


        [Required(ErrorMessage = "Company title is required")]
        [MaxLength(250, ErrorMessage = "Max length is 250 characters")]
        [MinLength(3, ErrorMessage = "Min length is 3 characters")]
        [RegularExpression(@"^[^\d]+$", ErrorMessage = "Enter a string that does not consist only of numbers.")]
        public string CompantTitle { get; set; }


        [Required(ErrorMessage = "MERSIS is required")]
        [RegularExpression(@"^0\d{10}\d{2}(000)$", ErrorMessage = "Invalid MERSIS number. For example:0111111111111000")]
        public string Mersis { get; set; }


        [Required(ErrorMessage = "Tax number is required")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Tax number is not valid. Must be 10 characters long.")]
        public string TaxNumber { get; set; }


        [Required(ErrorMessage = "Tax office is required")]
        [MaxLength(50, ErrorMessage = "Max length is 50 characters")]
        [RegularExpression(@"^[a-zA-Z]*$", ErrorMessage = "Tax office is not valid")]
        [MinLength(3, ErrorMessage = "Min length is 3 characters")]
        public string TaxOffice { get; set; }

        public string ImagePath { get; set; }


        [Required(ErrorMessage = "Logo is required")]
        [MaxFileSize(10 * 1024 * 1024)]
        [AllowedExtensions(new string[] { ".jpg", ".png", ".jpeg" })]
        public IFormFile Logo { get; set; }

        [Required(ErrorMessage = "Phone is required")]
        [RegularExpression(@"^(0[2-9][0-9]{2}-?[2-9][0-9]{2}-[0-9]{2}-[0-9]{2}|05[0-9]-?[2-9][0-9]{2}-[0-9]{2}-[0-9]{2})$", ErrorMessage = "True format : 05xx-xxx-xx-xx  0xxx-xxx-xx-xx ")]
        public string PhoneNumber { get; set; }


        [Required(ErrorMessage = "Address is required")]
        [MaxLength(250, ErrorMessage = "Max length is 250 characters")]
        [MinLength(3, ErrorMessage = "Address is too short")]
        public string Address { get; set; }


        [Required(ErrorMessage = "E-mail is required")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "input is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Number of employees must be at least 1")]
        public int NumberOfEmployees { get; set; }

        [Required(ErrorMessage = "Date of establishment is required")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yy}", ApplyFormatInEditMode = true)]
        //[Range(typeof(DateTime), "01-01-1900", "31-12-2099", ErrorMessage = "Date is out of range")]
        public DateTime DateOfEstablishment { get; set; }


        [Required(ErrorMessage = "Date of establishment is required")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yy}", ApplyFormatInEditMode = true)]
        //[Range(typeof(DateTime), "2000-01-01", "2099-12-31", ErrorMessage = "Date is out of range")]
        public DateTime ContratStartDate { get; set; }

        [Required(ErrorMessage = "Date of establishment is required")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yy}", ApplyFormatInEditMode = true)]
        //[Range(typeof(DateTime), "2000-01-01", "2099-12-31", ErrorMessage = "Date is out of range")]
        public DateTime ContratEndDate { get; set; }

        public Status Status { get; set; }
    }
}

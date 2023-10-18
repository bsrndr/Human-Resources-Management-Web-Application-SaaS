using Presentation.Utilities;
using System.ComponentModel.DataAnnotations;

namespace Presentation.Models.ModelMetaDatas
{
    public class UpdateCompanyManagerVmMetaData
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string? SecondFirstName { get; set; }
        public string LastName { get; set; }
        public string? SecondLastName { get; set; }
        public string Email { get; set; }



        [Required(ErrorMessage = "Address can not be empty")]
        [MinLength(3, ErrorMessage = "Address is not valid")]
        [MaxLength(250, ErrorMessage = "Address too long")]
        public string Address { get; set; }



        [Required(ErrorMessage = "Phone number cant be empty")]
        [RegularExpression(@"^(0[2-9][0-9]{2}-?[2-9][0-9]{2}-[0-9]{2}-[0-9]{2}|05[0-9]-?[2-9][0-9]{2}-[0-9]{2}-[0-9]{2})$", ErrorMessage = "True format : 05xx-xxx-xx-xx  0xxx-xxx-xx-xx ")]
        public string PhoneNumber { get; set; }

        public string ImagePath { get; set; }



        //[Required(ErrorMessage = "User photo required")]
        [MaxFileSize(5 * 1024 * 1024)]
        [AllowedExtensions(new string[] { ".jpg", ".png", "jpeg" })]
        public IFormFile? PostedPath { get; set; }
    }
}

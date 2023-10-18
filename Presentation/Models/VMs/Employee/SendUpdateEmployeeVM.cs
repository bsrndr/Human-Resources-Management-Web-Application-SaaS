namespace Presentation.Models.VMs.Employee
{
    public class SendUpdateEmployeeVM
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string? ImagePath { get; set; }
    }
}

namespace Presentation.Models.VMs.Leave
{
    public class LeaveTypeVM
    {
        public int Id { get; set; }
        public string LeaveType { get; set; }
        public int MaxNumberOfDays { get; set; }
        public string Description { get; set; }
    }
}

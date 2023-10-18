namespace Presentation.Models.VMs.Leave
{
    public class CreateLeaveVM
    {
        public int LeaveId { get; set; }
        public int UserId { get; set; }
        public int NumberOfRequestedDays { get; set; }
        public DateTime StartDate { get; set; }
    }
}

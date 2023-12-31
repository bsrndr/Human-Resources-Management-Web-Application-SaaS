﻿namespace Presentation.Models.VMs.Leave
{
    public class LeaveAppUserVM
    {
        public int Id { get; set; }
        public int NumberOfRequestedDays { get; set; }
        public string LeaveStatus { get; set; }
        public string LeaveType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime DateOfRequest { get; set; }
        public DateTime? DateofResponse { get; set; }
    }
}

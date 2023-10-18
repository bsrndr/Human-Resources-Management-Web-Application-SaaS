﻿using Presentation.Models.Enums;

namespace Presentation.Models.VMs.SiteManager
{
    public class DetailsVM
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string SecondFirstName { get; set; }
        public string LastName { get; set; }
        public string SecondLastName { get; set; }
        public decimal Salary { get; set; }
        public string Title { get; set; }
        public DateTime BirthDate { get; set; }
        public string PlaceOfBirth { get; set; }
        public string Tc { get; set; }
        public Status Status { get; set; }
        public DateTime DateOfRecruitment { get; set; }
        public string DepartmentName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string ImagePath { get; set; }
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
    }
}

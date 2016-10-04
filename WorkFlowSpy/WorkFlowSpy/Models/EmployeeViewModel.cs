using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WorkFlowSpy.Models
{
    public class EmployeeViewModel
    {
        public EmployeeViewModel():this (false)
        {
        }
        public EmployeeViewModel(bool terminated)
        {
            Terminated = false;
        }
        public int Id { get; set; }
        public Guid GUID { get; set; }
        [Display(Name = "Holder")]
        [Required]
        public string HolderCode { get; set; }
        [Display(Name = "First name")]
        [Required]
        public string FirstName { get; set; }
        [Display(Name = "Last name")]
        [Required]
        public string LastName { get; set; }
        [Display(Name = "Hire date")]
        public DateTime? HiredDate { get; set; }
        public string Position { get; set; }
        public bool Terminated { get; set; }
        public string IdentityId { get; set; }
        [Display(Name = "User first name")]
        public string IdentityFirstName { get; set; }
        [Display(Name = "User last name")]
        public string IdentityLastName { get; set; }
        public Dictionary<string,string> IdentityInfo { get; set; }
    }
}
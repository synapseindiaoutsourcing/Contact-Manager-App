using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManager.Core.DTO
{
    public class ContactsDTO
    {
        public int? ContactId { get; set; }
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }
        [Required]
        [RegularExpression("^[0-9]{10}$", ErrorMessage = "Please enter valid Phone number")]
        public string? PhoneNumber { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string? EmailAddress { get; set; }
        public DateTime? DOB { get; set; }
        public string? CompanyName { get; set; }
        public string? Lat { get; set; }
        public string? Long { get; set; }
        [Required]
        public string? Address { get; set; }
    }
}

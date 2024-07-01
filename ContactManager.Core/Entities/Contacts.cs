using ContactManager.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManager.Core.Entities
{
    public class Contacts : BaseEntity, IAggregateRoot
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? EmailAddress { get; set; }
        public DateTime? DOB { get; set; }
        public string? CompanyName { get; set; }
        public string? Lat { get; set; }
        public string? Long { get; set; }
        public string? Address { get; set; }
    }
}

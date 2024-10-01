using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleManager.Dto.Requests
{
    public class PersonRequest
    {
        [Display(Name = "First Name")]
        [Required]
        public required string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required]

        public required string LastName { get; set; }

        public string? Email { get; set; }

        public int? OrganizationId { get; set; }
    }

}


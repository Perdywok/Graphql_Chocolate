using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphql.Domain.Entities
{
    public class Contact
    {
        public int Id { get; set; }

        [Required]
        public string Email { get; set; }

        public string Address { get; set; }

        public string PhoneNumber { get; set; }
    }
}

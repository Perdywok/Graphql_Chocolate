using GraphqlDomain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Graphql.Domain.Entities
{
    public class Contact
    {
        public int Id { get; set; }

        [Required]
        public string Email { get; set; }

        public string? Address { get; set; }

        public string? PhoneNumber { get; set; }

        public int? UserId { get; set; }

        public User? User { get; set; }
    }
}

using Graphql.Domain.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GraphqlDomain.Entities
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        public int? ContactId { get; set; }

        public Contact? Contact { get; set; }

        public ICollection<Role> Roles { get; set; }
    }
}

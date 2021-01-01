using GraphqlDomain.Entities;
using System.Collections.Generic;

namespace Graphql.Domain.Entities
{
    public class Role
    {
        public int Id { get; set; }

        public ICollection<User> Users { get; set; }
    }
}

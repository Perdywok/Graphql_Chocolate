using Graphql.Business.Common;
using Graphql.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphql.Business.Contacts
{
    public class AddContactPayload : Payload
    {
        public AddContactPayload(Contact contact)
        {
            Contact = contact;
        }

        public AddContactPayload(BadRequestError error)
            : base(new[] { error })
        {
        }

        public Contact? Contact { get; init; }
    }
}

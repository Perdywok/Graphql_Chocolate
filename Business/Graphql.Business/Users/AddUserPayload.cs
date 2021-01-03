using Graphql.Business.Common;
using GraphqlDomain.Entities;

namespace Graphql.Business.Users
{
    public class AddUserPayload : Payload
    {
        public AddUserPayload(User user)
        {
            User = user;
        }

        public AddUserPayload(BadRequestError error)
            : base(new[] { error })
        {
        }

        public User? User { get; init; }
    }
}

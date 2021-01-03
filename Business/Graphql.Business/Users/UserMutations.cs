using Graphql.Business.Common;
using Graphql.Business.Extensions;
using GraphqlDomain;
using GraphqlDomain.Entities;
using HotChocolate;
using HotChocolate.Types;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Graphql.Business.Users
{
    [ExtendObjectType(Name = "Mutation")]
    public class UserMutations
    {
        [UseApplicationDbContext]
        public async Task<AddUserPayload> AddUserAsync(
            AddUserInput input,
            [ScopedService] ApplicationDbContext context,
            CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(input.Name))
            {
                return new AddUserPayload(
                    new BadRequestError("The name cannot be empty.", "NAME_EMPTY"));
            }

            if (string.IsNullOrEmpty(input.Surname))
            {
                return new AddUserPayload(
                    new BadRequestError("The surname cannot be empty.", "SURNAME_EMPTY"));
            }

            var user = new User
            {
                Name = input.Name,
                Surname = input.Surname,
            };

            context.Users.Add(user);
            await context.SaveChangesAsync(cancellationToken);

            return new AddUserPayload(user);
        }
    }
}

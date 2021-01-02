using Graphql.Business.Extensions;
using Graphql.Domain.DataLoaders;
using GraphqlDomain;
using GraphqlDomain.Entities;
using HotChocolate;
using HotChocolate.Types;
using HotChocolate.Types.Relay;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Graphql.Business.Orders
{
    [ExtendObjectType(Name = "Query")]
    public class UserQueries
    {
        [UseApplicationDbContext]
        [UsePaging]
        public IQueryable<User> GetUsers(
            [ScopedService] ApplicationDbContext context) =>
            context.Users.OrderBy(u => u.Name);

        public Task<User> GetUserByIdAsync(
            [ID(nameof(User))] int id,
            UserByIdDataLoader userById,
            CancellationToken cancellationToken) =>
            userById.LoadAsync(id, cancellationToken);
    }
}

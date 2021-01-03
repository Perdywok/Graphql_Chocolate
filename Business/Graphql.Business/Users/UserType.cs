using Graphql.Business.Extensions;
using Graphql.Domain.DataLoaders;
using Graphql.Domain.Entities;
using GraphqlDomain;
using GraphqlDomain.Entities;
using HotChocolate;
using HotChocolate.Resolvers;
using HotChocolate.Types;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Graphql.Business.Users
{
    public class UserType : ObjectType<User>
    {
        protected override void Configure(IObjectTypeDescriptor<User> descriptor)
        {
            descriptor
                .ImplementsNode()
                .IdField(t => t.Id)
                .ResolveNode((ctx, id) => ctx.DataLoader<UserByIdDataLoader>().LoadAsync(id, ctx.RequestAborted));

            descriptor
                .Field(t => t.Roles)
                .ResolveWith<UserResolvers>(t => t.GetRolesAsync(default!, default!, default!, default))
                .UseDbContext<ApplicationDbContext>()
                .Name("roles");

            descriptor
                .Field(t => t.Contact)
                .ResolveWith<UserResolvers>(t => t.GetContactAsync(default!, default!, default!, default))
                .UseDbContext<ApplicationDbContext>()
                .Name("contact");
        }

        // TODO add resolve of "Contact" entity
        private class UserResolvers
        {
            [UseApplicationDbContext]
            public async Task<IEnumerable<Role>> GetRolesAsync(
                User User,
                [ScopedService] ApplicationDbContext dbContext,
                RoleByIdDataLoader roleById,
                CancellationToken cancellationToken)
            {
                int[] rolesIds = await dbContext.Users
                    .Where(s => s.Id == User.Id)
                    .Include(s => s.Roles)
                    .SelectMany(s => s.Roles.Select(t => t.Id))
                    .ToArrayAsync(cancellationToken: cancellationToken);

                return await roleById.LoadAsync(rolesIds, cancellationToken);
            }

            [UseApplicationDbContext]
            public async Task<Contact> GetContactAsync(
                  User user,
                  [ScopedService] ApplicationDbContext dbContext,
                  ContactByIdDataLoader contactById,
                  CancellationToken cancellationToken)
            {
                if (!user.ContactId.HasValue)
                {
                    return null;
                }

                return await contactById.LoadAsync(user.ContactId.Value, cancellationToken);
            }
        }
    }
}

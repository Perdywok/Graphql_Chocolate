using Graphql.Business.Common;
using Graphql.Business.Extensions;
using Graphql.Domain.Entities;
using GraphqlDomain;
using GraphqlDomain.Entities;
using HotChocolate;
using HotChocolate.Types;
using Microsoft.EntityFrameworkCore;
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

            if (input.RoleIds != null && input.RoleIds.Any())
            {
                var rolesPresentInDb = await context.Roles.Where(r => input.RoleIds.Contains(r.Id))
                    .ToListAsync(cancellationToken);

                if (rolesPresentInDb.Count != input.RoleIds.Count)
                {
                    return new AddUserPayload(
                    new BadRequestError("Some of roles don't exist in db.", "ROLES_DONT_EXIST"));
                }
            }

            if (input.ContactId.HasValue)
            {
                var contactPresentsInDb = await context.Contacts.AnyAsync(c => c.Id == input.ContactId);
                if (!contactPresentsInDb)
                {
                    return new AddUserPayload(
                    new BadRequestError("Contact doesn't exist in db. ", "CONTACT_DOESNT_EXIST"));
                }
            }

            var user = new User
            {
                Name = input.Name,
                Surname = input.Surname,
                ContactId = input.ContactId
            };

            context.Users.Add(user);
            
            await context.SaveChangesAsync(cancellationToken);

            if (input.RoleIds != null && input.RoleIds.Any())
            {
                foreach (var roleId in input.RoleIds)
                {
                    context.Add(new UserRole
                    {
                        UserId = user.Id,
                        RoleId = roleId
                    });
                }

                await context.SaveChangesAsync(cancellationToken);
            }

            return new AddUserPayload(user);
        }
    }
}

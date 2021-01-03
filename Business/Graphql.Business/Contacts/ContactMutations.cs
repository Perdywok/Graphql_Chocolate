using Graphql.Business.Common;
using Graphql.Business.Extensions;
using Graphql.Domain.Entities;
using GraphqlDomain;
using HotChocolate;
using HotChocolate.Types;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Graphql.Business.Contacts
{
    [ExtendObjectType(Name = "Mutation")]
    public class ContactMutations
    {
        [UseApplicationDbContext]
        public async Task<AddContactPayload> AddContactAsync(
            AddContactInput input,
            [ScopedService] ApplicationDbContext context,
            CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(input.Email))
            {
                return new AddContactPayload(
                    new BadRequestError("The email cannot be empty.", "EMAIL_EMPTY"));
            }

            if (input.UserId.HasValue)
            {
                var userExists = await context.Users.AnyAsync(u => u.Id == input.UserId, cancellationToken);
                if (!userExists)
                {
                    return new AddContactPayload(
                    new BadRequestError("The user with provided id doesn't exist.", "USER_EMPTY"));
                }
            }

            var contact = new Contact
            {
                Email = input.Email,
                Address = input.Address,
                PhoneNumber = input.PhoneNumber,
                UserId = input.UserId
            };

            context.Contacts.Add(contact);
            try
            {
                await context.SaveChangesAsync(cancellationToken);
            }
            catch (System.Exception e)
            {

                throw;
            }
            //await context.SaveChangesAsync(cancellationToken);

            return new AddContactPayload(contact);
        }
    }
}

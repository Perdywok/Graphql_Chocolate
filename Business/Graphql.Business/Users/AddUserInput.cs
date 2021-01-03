using System.Collections.Generic;

namespace Graphql.Business.Users
{
    public record AddUserInput(
        string Name,
        string Surname,
        int? ContactId,
        IReadOnlyList<int>? RoleIds);
}

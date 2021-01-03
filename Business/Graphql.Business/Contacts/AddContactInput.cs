namespace Graphql.Business.Contacts
{
    public record AddContactInput(
        string Email,
        string? Address,
        string? PhoneNumber,
        int? UserId);
}

using Graphql.Business.Orders;
using Graphql.Business.Users;
using Graphql.Domain.DataLoaders;
using HotChocolate;
using System.Threading.Tasks;
using Xunit;
using Microsoft.Extensions.DependencyInjection;
using GraphqlDomain;
using Microsoft.EntityFrameworkCore;
using HotChocolate.Execution;
using Snapshooter.Xunit;

namespace Graphql.Tests
{
    public class UserTests
    {
        [Fact]
        public async Task User_Schema_Changed()
        {
            // arrange
            // act
            ISchema schema = await new ServiceCollection()
                .AddPooledDbContextFactory<ApplicationDbContext>(
                options => options.UseSqlite("Data Source=graphql.db"))
                .AddGraphQLServer()
                .AddQueryType(d => d.Name("Query"))
                    .AddTypeExtension<UserQueries>()
                .AddType<UserType>()
                .EnableRelaySupport()
                .AddFiltering()
                .AddSorting()
                .AddInMemorySubscriptions()
                .AddDataLoader<UserByIdDataLoader>()
                .AddDataLoader<ContactByIdDataLoader>()
                .AddDataLoader<RoleByIdDataLoader>()
                .BuildSchemaAsync();

            var sc = schema.Print();
            // assert
            schema.Print().MatchSnapshot();
        }
    }
}

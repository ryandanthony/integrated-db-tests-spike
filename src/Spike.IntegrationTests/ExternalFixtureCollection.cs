using Xunit;

namespace Spike.IntegrationTests
{
    [CollectionDefinition("SqlServerFixture")]
    public class SqlServerFixtureCollection : ICollectionFixture<SqlServerFixture>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.
    }
}
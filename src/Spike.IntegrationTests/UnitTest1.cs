using System;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using Dapper;

namespace Spike.IntegrationTests
{
    [Collection("SqlServerFixture")]
    public class UnitTest1 : IDisposable
    {
        private readonly SqlServerFixture _sqlServerFixture;
        private readonly ITestOutputHelper _testOutputHelper;

        public UnitTest1(SqlServerFixture sqlServerFixture, ITestOutputHelper testOutputHelper)
        {
            _sqlServerFixture = sqlServerFixture;
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public async Task Test1()
        {
            //_testOutputHelper.WriteLine(_sqlServerFixture.ConnectionString);

            using (var connection = new SqlConnection(_sqlServerFixture.ConnectionString))
            {
                await connection.OpenAsync();
                var query = @"
                                SELECT 
                                      *
                                  FROM Person";

                var person = await connection.QueryFirstOrDefaultAsync<Person>(query);

                Assert.Equal("Jim", person?.FirstName);
                Assert.Equal("Bob", person?.LastName);
                _testOutputHelper.WriteLine($"{person?.FirstName} {person?.LastName}");

            }

        }

        public void Dispose()
        {
            _sqlServerFixture.Cleanup();
        }
    }

    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime HireDate { get; set; }
        public DateTime EnrollmentDate { get; set; }

    }

  
}

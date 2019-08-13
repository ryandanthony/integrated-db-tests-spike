using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Spike.Fixtures;

namespace Spike.IntegrationTests
{
    public class SqlServerFixture : DockerFixtureBase
    {
        private string _user = "sa";
        private string _password = "c3kgC5#Adfl*";
        private string _dbName = "spike-db";
        
        protected override async Task InitializeImpl()
        {
            int port;
            if (AutoProvision)
            {
                //https://hub.docker.com/_/microsoft-mssql-server?tab=description
                var sqlSettings = await InitContainer(new ContainerInitConfiguration()
                {
                    DockerImage = "mcr.microsoft.com/mssql/server:2017-latest",
                    PostBuildDelayMs = 10000,
                    ContainerNamePrefix = $"{_dbName}",
                    Ports = new List<string>()
                    {
                        "1433/tcp"
                    },
                    EnvironmentVariables = new List<string>()
                    {
                        "ACCEPT_EULA=Y",
                        $"SA_PASSWORD={_password}",
                        "MSSQL_PID=Developer",
                    }
                });
                port = sqlSettings.BoundPorts.First().HostPort;

                await InitContainer(new ContainerInitConfiguration()
                {
                    DockerImage = $"{_dbName}-createdb:integration",
                    ContainerNamePrefix = $"{_dbName}-create-db",
                    EnvironmentVariables = new List<string>()
                    {
                        $"DB_DBNAME=master",
                        $"DB_USER={_user}",
                        $"DB_PASSWORD={_password}",
                        $"DB_HOST={HostIp}",
                        $"DB_PORT={port}",
                    }
                });

                await InitContainer(new ContainerInitConfiguration()
                {
                    DockerImage = $"{_dbName}-migrate:integration",
                    ContainerNamePrefix = $"{_dbName}-db-migrate",
                    EnvironmentVariables = new List<string>()
                    {
                        $"DB_DBNAME={_dbName}",
                        $"DB_USER={_user}",
                        $"DB_PASSWORD={_password}",
                        $"DB_HOST={HostIp}",
                        $"DB_PORT={port}",
                    }
                });
            }
            else
            {
                port = 1433;
            }

            ConnectionString = $"Server={HostIp},{port};Database={_dbName};User Id={_user};Password={_password};";
        }

        public string ConnectionString { get; set; }

        public void Cleanup()
        {
            //Todo Delete records or what ever you need to do between runs...
        }
    }
}
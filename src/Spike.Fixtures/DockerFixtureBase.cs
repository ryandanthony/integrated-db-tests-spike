using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Docker.DotNet;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace Spike.Fixtures
{
    public abstract class DockerFixtureBase : IAsyncLifetime
    {
        protected static readonly Random Random;
        private DockerClient _client;
        private readonly List<ContainerInstance> _containerInstances = new List<ContainerInstance>();
        static DockerFixtureBase()
        {
            Random = new Random();
        }
        
        protected string HostName { get; private set; }
        protected bool AutoProvision { get; private set; }
        protected string HostIp { get; private set; }

        public async Task InitializeAsync()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables();

            var configuration = builder.Build();

            HostName = configuration["DOCKER_TEST_HOST"] ?? "dockerhost";
            HostIp = Dns.GetHostEntry(HostName).AddressList.FirstOrDefault(address => address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)?.ToString();
            AutoProvision = configuration["autoProvision"] == "true";
            if (AutoProvision)
            {
                var dockerInstanceUri = configuration["DOCKER_TEST_URI"];
                //if no remoteDockerInstanceUri, then assume local
                if (string.IsNullOrEmpty(dockerInstanceUri))
                {
                    if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                    {
                        dockerInstanceUri = "unix:///var/run/docker.sock";
                    }
                    else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                    {
                        dockerInstanceUri = "npipe://./pipe/docker_engine";
                    }
                }

                if (string.IsNullOrWhiteSpace(dockerInstanceUri))
                {
                    throw new Exception("Unable to configure docker. Maybe unsupported OS");
                }

                using (var dockerClientConfiguration = new DockerClientConfiguration(new Uri(dockerInstanceUri)))
                {
                    _client = dockerClientConfiguration.CreateClient();
                }
            }

            await InitializeImpl();
        }




        protected abstract Task InitializeImpl();

        protected async Task<DockerFixtureSettings> InitContainer(ContainerInitConfiguration fixtureConfiguration)
        {
            if (AutoProvision)
            {
                var containerInstance = new ContainerInstance(_client, fixtureConfiguration);
                await containerInstance.Initialize();
                _containerInstances.Add(containerInstance);
                return containerInstance.Settings;
            }

            return null;
        }

        public async Task DisposeAsync()
        {
            if (_client != null)
            {
                foreach (var containerInstance in _containerInstances)
                {
                    await Task.Delay(1);
                    //await containerInstance.Dispose();
                }

                _client.Dispose();
            }
        }
    }
}
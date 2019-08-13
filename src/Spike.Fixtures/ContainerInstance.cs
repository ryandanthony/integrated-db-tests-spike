using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Docker.DotNet;
using Docker.DotNet.Models;

namespace Spike.Fixtures
{
    public class ContainerInstance 
    {
        private string _containerName;
        protected static readonly Random Random = new Random();
        
        private readonly DockerClient _client;
        private readonly ContainerInitConfiguration _fixtureConfiguration;
        

        public ContainerInstance(DockerClient client, ContainerInitConfiguration fixtureConfiguration)
        {
            _client = client;
            _fixtureConfiguration = fixtureConfiguration;
        }

        public DockerFixtureSettings Settings { get; private set; }

        public async Task Initialize()
        {
            _containerName = $"{_fixtureConfiguration.ContainerNamePrefix}-{Guid.NewGuid()}";

            Settings = new DockerFixtureSettings
            {
                BoundPorts = _fixtureConfiguration.Ports?
                    .Select(p =>
                        new DockerFixtureSettings.BoundPort
                        {
                            Port = p,
                            HostPort = Random.Next(_fixtureConfiguration.HostPortMin,
                                _fixtureConfiguration.HostPortMax)
                        })
                    .ToList(),

            };


            var images = await _client.Images.ListImagesAsync(new ImagesListParameters
                {MatchName = _fixtureConfiguration.DockerImage});

            if (images.Count == 0)
            {
                // No image found. Pulling latest ..
                await _client.Images.CreateImageAsync(
                    new ImagesCreateParameters {FromImage = _fixtureConfiguration.DockerImage}, null,
                    IgnoreProgress.Forever);
            }

            IDictionary<string, IList<PortBinding>> portBindings = Settings.BoundPorts?.ToDictionary(
                p => p.Port, p =>
                    new List<PortBinding>()
                    {
                        new PortBinding
                        {
                            HostPort = $"{p.HostPort}",
                        }
                    } as IList<PortBinding>);

            await _client.Containers.CreateContainerAsync(
                new CreateContainerParameters
                {
                    Image = _fixtureConfiguration.DockerImage,
                    Name = _containerName,
                    Tty = true,
                    Env = _fixtureConfiguration.EnvironmentVariables,
                    HostConfig = new HostConfig
                    {
                        PortBindings = portBindings,
                    }
                });

            await _client.Containers.StartContainerAsync(_containerName, new ContainerStartParameters { });
            await Task.Delay(_fixtureConfiguration.PostBuildDelayMs);
        }

        private class IgnoreProgress : IProgress<JSONMessage>
        {
            public static readonly IProgress<JSONMessage> Forever = new IgnoreProgress();

            public void Report(JSONMessage value)
            {
            }
        }

        public async Task Dispose()
        {
            if (_client != null)
            {
                await _client.Containers.StopContainerAsync(_containerName, new ContainerStopParameters { });
                await _client.Containers.RemoveContainerAsync(_containerName, new ContainerRemoveParameters { Force = true });
            }
        }
    }
}
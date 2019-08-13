using System.Collections.Generic;

namespace Spike.Fixtures
{
    public class DockerFixtureSettings
    {
        public List<BoundPort> BoundPorts { get; set; }
        public class BoundPort
        {
            public string Port { get; set; }
            public int HostPort { get; set; }
        }
    }
}
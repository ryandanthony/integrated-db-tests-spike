using System.Collections.Generic;

namespace Spike.Fixtures
{
    public class ContainerInitConfiguration
    {
        public ContainerInitConfiguration()
        {
            HostPortMin = 30000;
            HostPortMax = 45000;
            PostBuildDelayMs = 2500;
        }
        public int HostPortMin { get; set; }
        public int HostPortMax { get; set; }
        public string DockerImage { get; set; }
        public List<string> EnvironmentVariables { get; set; }
        public List<string> Ports { get; set; }
        public int PostBuildDelayMs { get; set; }
        public string ContainerNamePrefix { get; set; }

        
    }
}
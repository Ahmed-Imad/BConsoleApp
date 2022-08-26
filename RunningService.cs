using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
// Dependency Injection, Serilog (logging), Settings Tamplete for Console App projects
namespace BConsoleApp
{
    public class RunningService : IRunningService
    {
        private readonly ILogger<RunningService> log;
        private readonly IConfiguration config;

        public RunningService(ILogger<RunningService> log, IConfiguration config)
        {
            this.log = log;
            this.config = config;
        }
        public void Run()
        {
            for (int i = 0; i < config.GetValue<int>("RunningTimes"); i++)
            {
                log.LogInformation("Running number {runNumber}", i);
            }
        }
    }
}

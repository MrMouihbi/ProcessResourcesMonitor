using System.Diagnostics;
using System.Threading.Tasks;
using ProcessResourcesMonitor;

namespace ProcessResourcesMonitor {
    public class MetricsCollector {
        private readonly int _processId;
        private Process _process;
        private TimeSpan _prevCpuTime;
        private DateTime _prevTime;

        public MetricsCollector(int processId) {
            _processId = processId;
            _process = Process.GetProcessById(processId);
            _prevCpuTime = _process.TotalProcessorTime;
            _prevTime = DateTime.Now;
        }

        public async Task CollectMetricsAsync(TimeSpan interval, Action<Metrics> onMetricsCollected) {
            while (!_process.HasExited) {
                await Task.Delay(interval);

                var timestamp = DateTime.Now;
                var cpuUsage = GetCpuUsage();
                var memoryUsage = _process.WorkingSet64 / 1024.0 / 1024.0; // Convert bytes to MB
                var threadCount = _process.Threads.Count;
                var privateBytes = _process.PrivateMemorySize64 / 1024.0 / 1024.0; // Convert bytes to MB
                var status = _process.Responding ? "Running" : "Not Responding";

                var metrics = new Metrics {
                    Timestamp = new DateTimeOffset(timestamp).ToUnixTimeMilliseconds(),
                    CpuUsage = cpuUsage,
                    MemoryUsage = memoryUsage,
                    ThreadCount = threadCount,
                    PrivateBytes = privateBytes,
                    Status = status
                };

                onMetricsCollected(metrics);
            }
        }

        private double GetCpuUsage() {
            var currentCpuTime = _process.TotalProcessorTime;
            var currentTime = DateTime.Now;

            var cpuUsedMs = (currentCpuTime - _prevCpuTime).TotalMilliseconds;
            var totalMsPassed = (currentTime - _prevTime).TotalMilliseconds;

            var cpuUsageTotal = cpuUsedMs / Environment.ProcessorCount;
            _prevCpuTime = currentCpuTime;
            _prevTime = currentTime;

            return cpuUsageTotal / totalMsPassed * 100;
        }
    }
}

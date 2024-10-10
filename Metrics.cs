namespace ProcessResourcesMonitor {
    public class Metrics {
        public long Timestamp { get; set; }
        public double CpuUsage { get; set; }
        public double MemoryUsage { get; set; }
        public int ThreadCount { get; set; }
        public double PrivateBytes { get; set; }
        public required string Status { get; set; }
    }
}
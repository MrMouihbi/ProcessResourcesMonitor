using ProcessResourcesMonitor;

namespace ProcessResourcesMonitor.Storage {
    public class MetricsCsvStorage : IMetricsStorage {
        private readonly string _filePath;

        public MetricsCsvStorage(string filePath) {
            _filePath = filePath;

            // Create the file and write the header line if not existed
            if (!File.Exists(_filePath)) {
                using var writer = new StreamWriter(_filePath);
                writer.WriteLine("Timestamp,CpuUsage,MemoryUsage(MB),ThreadCount,PrivateBytes,Status");
            }
        }


        public void store(Metrics metrics) {
            var csvLine = $"{metrics.Timestamp},{metrics.CpuUsage},{metrics.MemoryUsage},{metrics.ThreadCount},{metrics.PrivateBytes},{metrics.Status}";
            File.AppendAllText(_filePath, csvLine + Environment.NewLine);
        }
    }
}

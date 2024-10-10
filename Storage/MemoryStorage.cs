using ProcessResourcesMonitor;
using System.Text.Json;

namespace ProcessResourcesMonitor.Storage {
    public class MetricsMemoryStorage : IMetricsStorage {
        private readonly List<Metrics> _metricsList = [];

        public void store(Metrics metrics) {
            _metricsList.Add(metrics);
        }

        public List<Metrics> getMetrics() {
            return _metricsList;
        }


        public void saveAsJson(string fileName) {
            var json = JsonSerializer.Serialize(_metricsList, new JsonSerializerOptions {
                WriteIndented = true
            });

            File.WriteAllText(fileName, json);
        }
    }
}
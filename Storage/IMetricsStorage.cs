using ProcessResourcesMonitor;

public interface IMetricsStorage {
    public void store(Metrics metrics);
}
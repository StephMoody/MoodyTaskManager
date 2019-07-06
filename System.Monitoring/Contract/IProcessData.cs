namespace System.Monitoring.Contract
{
    public interface IProcessData
    {
        string Name { get; }

        double MemoryUsage { get; }

        double ID { get; }

        string Description { get; }

        void Kill();
    }
}

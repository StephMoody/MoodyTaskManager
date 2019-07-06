using System.Threading.Tasks;

namespace MoodyTaskManager.Contract
{
    public interface IFilteredTasksHost
    {
        Task FilterProcess(string name);

        Task UnfilterProcess(string name);

        bool IsProcessFiltered(string name);
    }
}

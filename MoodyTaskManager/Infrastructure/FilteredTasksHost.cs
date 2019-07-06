using MoodyTaskManager.Contract;
using System.Threading.Tasks;

namespace MoodyTaskManager.Infrastructure
{
    internal class FilteredTasksHost : IFilteredTasksHost
    {
        private readonly ITaskManagerRepository _taskManagerRepository;

        public FilteredTasksHost(ITaskManagerRepository taskManagerRepository)
        {
            _taskManagerRepository = taskManagerRepository;
        }

        public async Task FilterProcess(string name)
        {
            if (!_taskManagerRepository.FilteredTasks.Contains(name))
            {
                _taskManagerRepository.FilteredTasks.Add(name);
                await _taskManagerRepository.Save();
            }
        }

        public bool IsProcessFiltered(string name)
        {
            return _taskManagerRepository.FilteredTasks.Contains(name);
        }

        public async Task UnfilterProcess(string name)
        {
            if (_taskManagerRepository.FilteredTasks.Contains(name))
            {
                _taskManagerRepository.FilteredTasks.Remove(name);
                await _taskManagerRepository.Save();
            }
        }
    }
}

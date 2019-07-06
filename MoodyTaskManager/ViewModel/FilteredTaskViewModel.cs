using System.Monitoring.Contract;
using MoodyTaskManager.Contract;

namespace MoodyTaskManager.ViewModel
{
    public class FilteredTaskViewModel : ViewModelBase
    {
        private readonly IProcessData _processData;
        private readonly IFilteredTasksHost _filteredTasksHost;

        public FilteredTaskViewModel(IProcessData processData, IFilteredTasksHost filteredTasksHost)
        {
            _processData = processData;
            _filteredTasksHost = filteredTasksHost;
            RemoveFilter = new RelayCommand(() => _filteredTasksHost.UnfilterProcess(Name));
        }

        public string Name => _processData.Name;

        public RelayCommand RemoveFilter { get; }
    }
}
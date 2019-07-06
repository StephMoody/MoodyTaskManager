using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Monitoring.Contract;
using System.Threading.Tasks;
using MoodyTaskManager.Contract;
using MoodyTaskManager.Resources;

namespace MoodyTaskManager.ViewModel
{
    public class FilteredTasksViewModel : TaskViewModelBase, IUpdateItem
    {

        public FilteredTasksViewModel(IProcesDataProvider procesInfoProvider, IFilteredTasksHost filteredTasksHost)
        {
            ProcessInfoProvider = procesInfoProvider;
            FilteredTasksHost = filteredTasksHost;
        }

        public ObservableCollection<FilteredTaskViewModel> FilteredTaskViewModels { get; } =
            new ObservableCollection<FilteredTaskViewModel>();

        public override string Designation => StringResources.FilteredTasksDesignation;

        public async Task Update()
        {
            await UpdateProcessData();
            await UpdateFilteredProcessData();

            Refresh();
        }

        protected override void Refresh()
        {
            OnPropertyChanged(nameof(FilteredTaskViewModels));
            base.Refresh();
        }

        private async Task UpdateProcessData()
        {
            IEnumerable<IProcessData> resultProcessData = await ProcessInfoProvider.GetProcessInfo();
            IProcessData[] currentProcessData = resultProcessData.ToArray();

            List<IProcessData> toRemove = new List<IProcessData>();
            foreach (IProcessData vmProcess in ProcessData)
                if (!currentProcessData.Any(b => Math.Abs(b.ID - vmProcess.ID) < 0.01) ||
                    !FilteredTasksHost.IsProcessFiltered(vmProcess.Name))
                    toRemove.Add(vmProcess);

            foreach (IProcessData removeAbleItem in toRemove) ProcessData.Remove(removeAbleItem);

            foreach (IProcessData processData in currentProcessData)
                if (!ProcessData.Any(b => Math.Abs(b.ID - processData.ID) < 0.01) &&
                    FilteredTasksHost.IsProcessFiltered(processData.Name))
                    ProcessData.Add(processData);
        }

        private Task UpdateFilteredProcessData()
        {
            List<FilteredTaskViewModel> filteredTaskViewModels = FilteredTaskViewModels.ToList();
            foreach (FilteredTaskViewModel filteredTask in filteredTaskViewModels)
                if (ProcessData.All(b => b.Name != filteredTask.Name))
                    FilteredTaskViewModels.Remove(filteredTask);

            foreach (IProcessData vmProcess in ProcessData)
                if (FilteredTaskViewModels.All(b => b.Name != vmProcess.Name))
                    FilteredTaskViewModels.Add(new FilteredTaskViewModel(vmProcess, FilteredTasksHost));

            return Task.CompletedTask;
        }
    }
}
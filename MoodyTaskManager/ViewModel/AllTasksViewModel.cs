using System;
using MoodyTaskManager.Contract;
using MoodyTaskManager.Resources;
using System.Collections.Generic;
using System.Linq;
using System.Monitoring.Contract;
using System.Threading.Tasks;

namespace MoodyTaskManager.ViewModel
{
    public class AllTasksViewModel : TaskViewModelBase, IUpdateItem
    {
        public AllTasksViewModel(IProcesDataProvider processInfoProvider, IFilteredTasksHost filteredTasksHost)
        {
            ProcessInfoProvider = processInfoProvider;
            FilteredTasksHost = filteredTasksHost;
        }

        public virtual async Task Update()
        {
            IEnumerable<IProcessData> currentProcessData = await ProcessInfoProvider.GetProcessInfo();

            IProcessData[] processDatas = currentProcessData as IProcessData[] ?? currentProcessData.ToArray();
            foreach (IProcessData vmProcess in ProcessData)
            {
                if (!processDatas.Any(b => Math.Abs(b.ID - vmProcess.ID) < 0.01))
                    ProcessData.Remove(vmProcess);
            }

            foreach (IProcessData processData in processDatas)
            {
                if (!ProcessData.Any(b => Math.Abs(b.ID - processData.ID) < 0.01))
                    ProcessData.Add(processData);
            }

            Refresh();
        }

        public override string Designation => StringResources.AllTaskDesignation;
    }
}

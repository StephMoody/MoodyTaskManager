using System.Collections.Generic;
using System.Monitoring.Contract;
using System.Threading.Tasks;

namespace System.Monitoring.Process
{
    public class ProcessDataProvider : IProcesDataProvider
    {
        Task _delayTask;

        public async Task<IEnumerable<IProcessData>> GetProcessInfo()
        {
            if (_delayTask != null)
                await _delayTask;

            TaskCompletionSource<Task> taskCompletionSource
                    = new TaskCompletionSource<Task>();

            List<ProcessData> result = new List<ProcessData>();

            try
            {
                _delayTask = taskCompletionSource.Task;
                Diagnostics.Process[] allProcesses = Diagnostics.Process.GetProcesses();
                foreach (Diagnostics.Process process in allProcesses)
                {
                    result.Add(new ProcessData(process));
                }

                return result;
            }
            finally
            {
                taskCompletionSource.SetResult(Task.CompletedTask);
                _delayTask = null;
            }
        }
    }
}

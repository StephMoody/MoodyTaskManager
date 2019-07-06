
using MoodyTaskManager.Contract;
using System.Collections.Generic;

namespace MoodyTaskManager.Data
{
    internal class TaskManagerRepository : RepositoryBase<TaskManagerData>, ITaskManagerRepository
    {
        public TaskManagerRepository(TaskManagerData data) : base(data)
        {
        }

        public HashSet<string> FilteredTasks
        {
            get => Data.FilteredTasks;
            set => Data.FilteredTasks = value;
        }
    }
}

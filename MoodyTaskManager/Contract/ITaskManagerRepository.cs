using System.Collections.Generic;

namespace MoodyTaskManager.Contract
{
    internal interface ITaskManagerRepository : IRepositoryBase
    {
        HashSet<string> FilteredTasks { get; set; }
    }
}

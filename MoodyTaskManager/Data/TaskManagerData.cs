using System.Collections.Generic;

namespace MoodyTaskManager.Data
{
    internal class TaskManagerData : DataBase
    {
        public HashSet<string> FilteredTasks { get; set; } = new HashSet<string>();

        public override int ID => 1;
    }
}

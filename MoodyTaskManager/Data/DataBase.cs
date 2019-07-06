using MoodyTaskManager.Contract;

namespace MoodyTaskManager.Data
{
    internal abstract class DataBase : IDataBase
    {
        public abstract int ID { get; }
    }
}

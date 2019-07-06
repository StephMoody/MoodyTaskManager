using System.Threading.Tasks;

namespace MoodyTaskManager.Contract
{
    internal interface IRepositoryBase
    {
        Task Load();

        Task Save();

        Task Initialize();
    }
}

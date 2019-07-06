using System.Collections.Generic;
using System.Threading.Tasks;

namespace System.Monitoring.Contract
{
    public interface IProcesDataProvider
    {
        Task<IEnumerable<IProcessData>> GetProcessInfo();
    }
}

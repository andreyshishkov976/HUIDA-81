using System.Collections.Generic;
using System.Threading.Tasks;

using Huida81_WPF.Core.Models;

namespace Huida81_WPF.Core.Contracts.Services
{
    public interface ISampleDataService
    {
        Task<IEnumerable<SampleOrder>> GetGridDataAsync();

        Task<IEnumerable<SampleOrder>> GetListDetailsDataAsync();
    }
}

using System.Threading.Tasks;

namespace Huida81_WPF.Contracts.Services
{
    public interface IApplicationHostService
    {
        Task StartAsync();

        Task StopAsync();
    }
}

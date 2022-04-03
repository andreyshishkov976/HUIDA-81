using System.Threading.Tasks;

namespace Huida81_WPF.Contracts.Activation
{
    public interface IActivationHandler
    {
        bool CanHandle();

        Task HandleAsync();
    }
}

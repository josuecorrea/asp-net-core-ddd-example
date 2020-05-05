using System.Threading.Tasks;

namespace AspNetCore.Example.Infra.Services.Contracts
{
    public interface IMessageReceiverService
    {
        Task Receiver<T>() where T : class;
    }
}

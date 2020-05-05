using System.Threading.Tasks;

namespace AspNetCore.Example.Infra.Services.Contracts
{
    public interface IMessageService
    {
        Task SendQueueAsync<T>(T message, string queueName) where T : class;
        Task SendTopicAsync<T>(T message, string routeKey = "", bool isMandatory = false) where T : class;
    }
}

using AspNetCore.Example.Infra.Services.Contracts;
using EasyNetQ;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Polly;
using System;
using System.Threading.Tasks;

namespace AspNetCore.Example.Infra.Services.Implements
{
    public class MessageReceiverService : IMessageReceiverService
    {
        private string _queueName;

        private int _retryAttempts;


        private readonly IBus _bus;
        private readonly ILogger<MessageReceiverService> _logger;

        public MessageReceiverService(IBus bus, ILogger<MessageReceiverService> logger, IConfiguration configuration)
        {
            _bus = bus;
            _logger = logger;
            _retryAttempts = Convert.ToInt32(configuration.GetSection("RabbitMQConfigurations:RetryAttempts").Value);
            _queueName = configuration.GetSection("RabbitMQConfigurations:LimitsEventsReceiver:QueueName").Value;
        }

        public async Task Receiver<T>() where T : class
        {
            if (!_bus.IsConnected)
            {
                throw new Exception("");
            }

            var policy = Policy.Handle<Exception>().WaitAndRetry(_retryAttempts, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, _retryAttempts)), (ex, time) =>
            {
                throw new Exception("");
            });

            Func<object, Task> Func = async (message) =>
            {
                //
            };

            policy.Execute(() =>
            {
                _logger.LogInformation($"Receiver message for topic {_queueName}");
                _bus.Receive<T>(_queueName, message => Func(message));
            });
        }
    }
}

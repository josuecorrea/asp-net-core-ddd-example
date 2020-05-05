using AspNetCore.Example.Infra.Services.Contracts;
using EasyNetQ;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Polly;
using System;
using System.Threading.Tasks;

namespace AspNetCore.Example.Infra.Services.Implements
{
    public class MessageService : IMessageService
    {
        private string _queueName;
        private string _exchangeName;
        private int _retryAttempts;
        private readonly ILogger _logger;
        private readonly IBus _bus;
        private readonly IAdvancedBus _advancedBus;

        public MessageService(ILogger<IMessageService> logger, IBus bus, IConfiguration configuration)
        {
            _logger = logger;
            _bus = bus;
            _advancedBus = _bus.Advanced;

            _retryAttempts = Convert.ToInt32(configuration.GetSection("RabbitMQConfigurations:RetryAttempts").Value);
            _queueName = configuration.GetSection("RabbitMQConfigurations:LimitsEventSender:QueueName").Value;
            _exchangeName = configuration.GetSection("RabbitMQConfigurations:LimitsEventSender:ExchangeName").Value;
        }


        private async Task SendQueueAsync<T>(T message, string queueName) where T : class
        {
            if (!_bus.IsConnected)
            {
                string errorMessage = "";
                _logger.LogError(errorMessage);
                throw new Exception("");
            }

            var policy = Policy.Handle<Exception>().WaitAndRetryAsync(_retryAttempts, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, _retryAttempts)), (ex, time) =>
            {
                string errorMessage = "";
                _logger.LogError(errorMessage);
                throw new Exception("");
            });

            await policy.ExecuteAsync(async () =>
                            await _bus.SendAsync(queueName, message).ConfigureAwait(false)
            ).ConfigureAwait(false);
        }

        private async Task SendTopicAsync<T>(T message, string routeKey = "", bool isMandatory = false) where T : class
        {
            var exchange = new ExchangeConfig();


            if (!_bus.IsConnected)
            {
                string errorMessage = "";
                _logger.LogError(errorMessage);
                throw new Exception("");
            }

            _advancedBus.ExchangeDeclare(exchange.Name, exchange.Type);

            var policy = Policy.Handle<Exception>().WaitAndRetryAsync(_retryAttempts, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, _retryAttempts)), (ex, time) =>
            {
                string errorMessage = "";
                _logger.LogError(errorMessage);
                throw new Exception("");
            });

            await policy.ExecuteAsync(async () =>
                await _advancedBus.PublishAsync(exchange, routeKey, isMandatory, new Message<T>(message)).ConfigureAwait(false)
            ).ConfigureAwait(false);
        }
    }
}

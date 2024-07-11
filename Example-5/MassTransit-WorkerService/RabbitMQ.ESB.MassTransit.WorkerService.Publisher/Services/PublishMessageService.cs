using MassTransit;
using Shared.WorkerService.Messages;

namespace RabbitMQ.ESB.MassTransit.WorkerService.Publisher.Services
{
    public class PublishMessageService(IPublishEndpoint publishEndpoint) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            int i = 0;
            while (true)
            {
                ExampleMessage message = new()
                {
                    Text = $"{i++}. mesaj"
                };
                await publishEndpoint.Publish(message);
            }
        }
    }
}
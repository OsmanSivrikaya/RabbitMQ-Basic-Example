using MassTransit;
using RabbitMQ.ESB.MassTransit.WorkerService.Consumer;
using RabbitMQ.ESB.MassTransit.WorkerService.Consumer.Consumers;

var builder = Host.CreateApplicationBuilder(args);

// Worker servisini ekleyin
builder.Services.AddHostedService<Worker>();

// MassTransit ve RabbitMQ yapılandırmasını ekleyin
builder.Services.AddMassTransit(configurator =>
{
    // ExampleMessageConsumer'ı tüketici olarak ekleyin
    configurator.AddConsumer<ExampleMessageConsumer>();

    // RabbitMQ bağlantısını ve yapılandırmasını ayarlayın
    configurator.UsingRabbitMq((context, _configurator) =>
    {
        // RabbitMQ sunucusuna bağlanma bilgileri
        _configurator.Host("amqps://dkqmxsrn:9d2EFpRPz22ELDJmBgIIxmU393NVxkFK@moose.rmq.cloudamqp.com/dkqmxsrn");

        // "example-message-queue" isimli kuyruğu dinleyin ve ExampleMessageConsumer ile yapılandırın
        _configurator.ReceiveEndpoint("example-message-queue", e => e.ConfigureConsumer<ExampleMessageConsumer>(context));
    });
});

// Uygulamayı başlatın
var host = builder.Build();
await host.RunAsync();

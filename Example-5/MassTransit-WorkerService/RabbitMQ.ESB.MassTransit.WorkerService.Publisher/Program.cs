using RabbitMQ.ESB.MassTransit.WorkerService.Publisher; // Worker service sınıfını içe aktarma
using MassTransit; // MassTransit kütüphanesini içe aktarma
using RabbitMQ.ESB.MassTransit.WorkerService.Publisher.Services; // Ek hizmetleri içe aktarma

var builder = Host.CreateApplicationBuilder(args);

// Worker Service'i ekleyin
builder.Services.AddHostedService<Worker>();

// MassTransit'i ve RabbitMQ yapılandırmasını ekleyin
builder.Services.AddMassTransit(configurator =>
{
    configurator.UsingRabbitMq((context, _configurator) =>
    {
        // RabbitMQ sunucusuna bağlanma
        _configurator.Host("amqps://dkqmxsrn:9d2EFpRPz22ELDJmBgIIxmU393NVxkFK@moose.rmq.cloudamqp.com/dkqmxsrn");
    });
});

// Mesaj gönderme hizmetini ekleyin
builder.Services.AddHostedService<PublishMessageService>(provider =>
{
    // Hizmet kapsamı oluşturun
    using IServiceScope scope = provider.CreateScope();

    // IPublishEndpoint hizmetini alın
    var publishEndpoint = scope.ServiceProvider.GetService<IPublishEndpoint>();

    // PublishMessageService hizmetini oluşturun ve sağlayıcıya geri döndürün
    return new PublishMessageService(publishEndpoint);
});

// Host'u oluşturun ve çalıştırın
var host = builder.Build();
await host.RunAsync();

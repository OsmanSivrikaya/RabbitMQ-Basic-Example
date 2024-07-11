using MassTransit;
using RabbitMQ.ESB.MassTransit.Consumer.Consumers;

// RabbitMQ bağlantı URI'sini belirleme
string rabbitMQUri = "amqps://dkqmxsrn:9d2EFpRPz22ELDJmBgIIxmU393NVxkFK@moose.rmq.cloudamqp.com/dkqmxsrn";

// Mesajın alınacağı kuyruk adını belirleme
string queueName = "example-queue";

// RabbitMQ kullanarak MassTransit bus kontrolünü oluşturma
IBusControl bus = Bus.Factory.CreateUsingRabbitMq(factory =>
{
    // RabbitMQ sunucusuna bağlanmak için gerekli bilgileri sağlar
    factory.Host(rabbitMQUri);

    // Belirli bir kuyruk için Receive Endpoint oluşturma
    factory.ReceiveEndpoint(queueName, endpoint =>
    {
        // Belirtilen kuyruğa gelen mesajları işlemek için bir consumer tanımlar
        endpoint.Consumer<ExampleMessageConsumer>();
    });
});

// Bus kontrolünü başlatma
await bus.StartAsync();

// Konsolun kapanmasını engeller
Console.Read();

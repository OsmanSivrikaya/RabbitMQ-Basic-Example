using MassTransit; // MassTransit kütüphanesini içe aktarır
using RabbitMQ.ESB.MassTransit.RequestResponsePattern.Consumer.Consumers; // RequestMessageConsumer'ı içe aktarır

// RabbitMQ bağlantı URI ve kuyruk adı tanımları
var rabbitMQUri = "amqps://dkqmxsrn:9d2EFpRPz22ELDJmBgIIxmU393NVxkFK@moose.rmq.cloudamqp.com/dkqmxsrn";
var requestQueue = "request-queue";

// RabbitMQ iletişimini yapılandırır ve başlatır
IBusControl bus = Bus.Factory.CreateUsingRabbitMq(factory => {
    factory.Host(rabbitMQUri); // RabbitMQ sunucusuna bağlantı sağlar

    // Belirtilen kuyruğu dinleyen ve mesajları işleyen bir Consumer ekler
    factory.ReceiveEndpoint(requestQueue, endpoint => {
        endpoint.Consumer<RequestMessageConsumer>(); // RequestMessageConsumer'ı kuyruğa bağlar
    });
});

await bus.StartAsync(); // RabbitMQ bağlantısını başlatır

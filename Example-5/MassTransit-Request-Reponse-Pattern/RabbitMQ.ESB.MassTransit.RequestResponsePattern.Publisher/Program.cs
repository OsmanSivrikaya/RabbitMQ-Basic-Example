using MassTransit; // MassTransit kütüphanesini içe aktarır
using RabbitMQ.ESB.MassTransit.RequestResponsePattern.Shared.RequestResponseMessage; // Paylaşılan RequestMessage ve ResponseMessage sınıflarını içe aktarır

// RabbitMQ bağlantı URI ve kuyruk adı tanımları
var rabbitMQUri = "amqps://dkqmxsrn:9d2EFpRPz22ELDJmBgIIxmU393NVxkFK@moose.rmq.cloudamqp.com/dkqmxsrn";
var requestQueue = "request-queue";

// RabbitMQ iletişimini yapılandırır ve başlatır
IBusControl bus = Bus.Factory.CreateUsingRabbitMq(factory => {
    factory.Host(rabbitMQUri); // RabbitMQ sunucusuna bağlantı sağlar
});

await bus.StartAsync(); // RabbitMQ bağlantısını başlatır

// RequestClient oluşturarak istemci iletişimini yapılandırır
var request = bus.CreateRequestClient<RequestMessage>(new Uri($"{rabbitMQUri}/{requestQueue}"));

int i = 1;
while(true){
    await Task.Delay(200); // 200 milisaniye bekleme
    // Yeni bir request gönderir ve cevabı alır
    var response = await request.GetResponse<ResponseMessage>(new() { MessageNo = i, Text = $"{i}. request"});
    Console.WriteLine($"Response received: {response.Message.Text} "); // Alınan cevabı konsola yazdırır
    i++;
}

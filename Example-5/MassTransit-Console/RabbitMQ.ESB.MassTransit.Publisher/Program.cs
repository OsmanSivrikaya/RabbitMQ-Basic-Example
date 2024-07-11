using MassTransit;
using RabbitMQ.ESB.MassTransit.Shared.Messages;

// RabbitMQ bağlantı URI'sini belirleme
// Bu URI, RabbitMQ sunucusuna bağlanmak için gerekli bilgileri içerir.
string rabbitMQUri = "amqps://dkqmxsrn:9d2EFpRPz22ELDJmBgIIxmU393NVxkFK@moose.rmq.cloudamqp.com/dkqmxsrn";

// Mesajın gönderileceği kuyruk adını belirleme
// Bu, mesajların gönderileceği kuyruğun adıdır.
string queueName = "example-queue";

// RabbitMQ kullanarak MassTransit bus kontrolünü oluşturma
// Bus, MassTransit'in mesaj gönderme ve alma işlemlerini yöneten bir bileşenidir.
IBusControl bus = Bus.Factory.CreateUsingRabbitMq(factory => factory.Host(rabbitMQUri));

// Belirtilen kuyruğa mesaj göndermek için send endpoint'i alır
// Send endpoint, belirli bir kuyruğa mesaj göndermek için kullanılır.
ISendEndpoint sendEndpoint = await bus.GetSendEndpoint(new($"{rabbitMQUri}/{queueName}"));

// Konsoldan gönderilecek mesajı al
// Kullanıcıdan gönderilmek üzere bir mesaj alınır.
Console.Write("Gönderilicek mesaj: ");
var message = Console.ReadLine() ?? "";

// Alınan mesajı belirlenen kuyruğa gönder
// Kullanıcıdan alınan mesaj, belirlenen send endpoint aracılığıyla kuyruğa gönderilir.
await sendEndpoint.Send<IMessage>(new ExampleMessage{Text = message});

// Konsolun kapanmasını engeller
// Bu satır, programın hemen kapanmasını engeller ve kullanıcıdan bir giriş bekler.
Console.Read();

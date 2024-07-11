using MassTransit; // MassTransit kütüphanesini kullanarak mesajlaşma işlemleri gerçekleştirilir
using RabbitMQ.ESB.MassTransit.Shared.Messages; // Mesajların tanımlandığı ve paylaşıldığı namespace

namespace RabbitMQ.ESB.MassTransit.Consumer.Consumers // Tüketicilerin tanımlandığı namespace
{
    // IMessage türündeki mesajları tüketen bir consumer sınıfı tanımlanıyor
    public class ExampleMessageConsumer : IConsumer<IMessage>
    {
        // Mesaj alındığında çalışacak olan method
        public Task Consume(ConsumeContext<IMessage> context)
        {
            // Gelen mesajın içeriği konsola yazdırılıyor
            Console.WriteLine($"Gelen mesaj: {context.Message.Text}");
            
            // Görevin tamamlandığını belirten Task döndürülüyor
            return Task.CompletedTask;
        }
    }
}

# 🐰 RabbitMQ ve MassTransit Örnekleri

Bu repo, RabbitMQ ve MassTransit kullanarak farklı mesajlaşma senaryolarını gösteren örnekleri içermektedir.

## 📁 Example-1: Basic RabbitMQ Publisher ve Consumer Örneği

Bu örnekte, temel RabbitMQ yayıncı ve tüketici senaryosunu gösterir.

- **Publisher:** `BasicPublisher.cs`
  - Mesaj yayınlama işlemi.
  
- **Consumer:** `BasicConsumer.cs`
  - Yayımlanan mesajları dinleme ve işleme.

## 📁 Example-2: Fair Dispatch, Message Acknowledgement, Message Durability, Round-Robin Dispatching

Bu örnekte, RabbitMQ'de adil dağıtım, mesaj doğrulama, mesaj dayanıklılığı ve round-robin dağıtımı gösterir.

- **Fair Dispatch:** `FairDispatch.cs`
  - Mesajları birden fazla tüketicinin adil bir şekilde alması.
  
- **Message Acknowledgement:** `MessageAcknowledgement.cs`
  - Mesajların tüketici tarafından doğrulama ile işlenmesi.
  
- **Message Durability:** `MessageDurability.cs`
  - Mesajların RabbitMQ'de dayanıklı olarak saklanması.
  
- **Round-Robin Dispatching:** `RoundRobinDispatching.cs`
  - Mesajların tüketicilere round-robin algoritmasıyla dağıtılması.

## 📁 Example-3: Direct Exchange, Fanout Exchange, Header Exchange, Topic Exchange

Bu örnekte, RabbitMQ'de direct, fanout, header ve topic exchange türlerini gösterir.

- **Direct Exchange:** `DirectExchange.cs`
  - Mesajların doğrudan belirli bir kuyruğa yönlendirilmesi.
  
- **Fanout Exchange:** `FanoutExchange.cs`
  - Mesajların tüm bağlı kuyruklara yayınlanması.
  
- **Header Exchange:** `HeaderExchange.cs`
  - Mesajların header bilgilerine göre belirli kuyruklara yönlendirilmesi.
  
- **Topic Exchange:** `TopicExchange.cs`
  - Mesajların topic bazlı paternlere göre belirli kuyruklara yönlendirilmesi.

## 📁 Example-4: P2P, Publish-Subscribe, Request-Response, Work Queue Tasarım Örnekleri

Bu örnekte, RabbitMQ ve MassTransit ile point-to-point, publish-subscribe, request-response ve work queue tasarımlarını gösterir.

- **Point-to-Point (P2P):** `P2PExample.cs`
  - Tek bir yayıncının tek bir tüketiciye mesaj göndermesi.
  
- **Publish-Subscribe:** `PublishSubscribeExample.cs`
  - Tek bir yayıncının birden fazla tüketicinin aldığı mesajları yayınlaması.
  
- **Request-Response:** `RequestResponseExample.cs`
  - İstemci tarafından gönderilen isteğe sunucunun cevap vermesi.
  
- **Work Queue:** `WorkQueueExample.cs`
  - Birden fazla işçi (worker) tarafından alınan ve işlenen mesajlar.

## 📁 Example-5: MassTransit Örnekleri (Console, Worker Service, Request-Response Pattern)

Bu örnekte, MassTransit kullanarak farklı senaryoları gösterir.

- **Console Uygulaması:** `MassTransitConsoleExample.cs`
  - Basit bir konsol uygulaması örneği.
  
- **Worker Service:** `MassTransitWorkerService.cs`
  - Arka planda çalışan bir işçi hizmeti (Worker Service) örneği.
  
- **Request-Response Pattern:** `MassTransitRequestResponse.cs`
  - MassTransit ile request-response (istek-cevap) deseni örneği.

---

Not: İçerikler Gencay Yıldız RabbitMQ eğitiminden esinlenilmiştir.

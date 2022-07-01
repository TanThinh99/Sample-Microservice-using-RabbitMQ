using Inventory.Persistence.Dtos;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Inventory.RabbitMQ
{
    public class Consumer
    {
        private ManualResetEvent _resetEvent;
        private IConnection _connection;
        private IModel _channel;
        private HttpClient _httpClient;
        private ConfigurationString _config;

        public Consumer(ManualResetEvent resetEvent, HttpClient httpClient, ConfigurationString config)
        {
            _resetEvent = resetEvent;

            var factory = new ConnectionFactory
            {
                Uri = new Uri(config.RabbitMQString),
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _httpClient = httpClient;
            _config = config;
        }

        public void ConsumeQueue()
        {
            var queueName = "back-orders";
            bool durable = false;
            bool exclusive = false;
            bool autoDelete = true;

            _channel.QueueDeclare(queueName, durable, exclusive, autoDelete, null);

            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += (model, deliveryEventArgs) =>
            {
                var body = deliveryEventArgs.Body.ToArray();

                var message = Encoding.UTF8.GetString(body);

                Console.WriteLine($"Message received: {message}");

                _channel.BasicAck(deliveryEventArgs.DeliveryTag, false);

                var response = System.Text.Json.JsonSerializer.Deserialize<TransactionResponseDto>(message);

                _httpClient.PostAsJsonAsync(_config.BaseAddress+"/Transaction", response);
            };

            // start consuming
            _ = _channel.BasicConsume(consumer, queueName);

            // Wait for the reset event and clean up when it triggers
            _resetEvent.WaitOne();
            Console.WriteLine("CancelEvent recieved, shutting down Consumer");
            _channel?.Close();
            _channel = null;
            _connection?.Close();
            _connection = null;
        }
    }
}

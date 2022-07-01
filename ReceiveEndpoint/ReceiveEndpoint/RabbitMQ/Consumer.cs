using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using ReceiveEndpoint.Persistence.Dtos;
using System.Text;

namespace ReceiveEndpoint.RabbitMQ
{
    public class Consumer
    {
        private ManualResetEvent _resetEvent;
        private IConnection _connection;
        private IModel _channel;
        private HttpClient _httpClient;
        private ConfigurationString _config;
        private IConfiguration _configuration;

        public Consumer(ManualResetEvent resetEvent, HttpClient httpClient, ConfigurationString config, IConfiguration configuration)
        {
            _resetEvent = resetEvent;

            // create a connection and open a channel, dispose them when done
            var factory = new ConnectionFactory
            {
                Uri = new Uri(config.RabbitMQString),
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _httpClient = httpClient;
            _config = config;

            _configuration = configuration;
            string a = _configuration["ConfigurationString:BackOfficeAddress"];
        }

        public void ConsumeQueue()
        {
            // ensure that the queue exists before we access it
            var queueName = "orders";
            bool durable = false;
            bool exclusive = false;
            bool autoDelete = true;
            
            _channel.QueueDeclare(queueName, durable, exclusive, autoDelete, null);

            var consumer = new EventingBasicConsumer(_channel);

            // add the message receive event
            consumer.Received += (model, deliveryEventArgs) =>
            {
                var body = deliveryEventArgs.Body.ToArray();

                var message = Encoding.UTF8.GetString(body);

                Console.WriteLine($"Message received: {message}");

                _channel.BasicAck(deliveryEventArgs.DeliveryTag, false);

                var response = System.Text.Json.JsonSerializer.Deserialize<TransactionResponseDto>(message);

                var res = _httpClient.PostAsJsonAsync(_config.BackOfficeAddress+"/Office/check-product", response);
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

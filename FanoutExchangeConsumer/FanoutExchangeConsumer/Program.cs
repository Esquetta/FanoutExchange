

using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

ConnectionFactory connectionFactory = new ConnectionFactory();
connectionFactory.Uri = new Uri("amqps://nmlgtsid:Hl-ZajX0FHO3eG52BQnrPKc8aQpkYTsd@sparrow.rmq.cloudamqp.com/nmlgtsid");

using(IConnection connection=connectionFactory.CreateConnection())
using(IModel channel=connection.CreateModel())
{
    channel.ExchangeDeclare("iskuyrugu", ExchangeType.Fanout);

    string queueName = channel.QueueDeclare().QueueName;
    channel.QueueBind(queueName, "iskuyrugu", "");

    channel.BasicQos(0, 1, false);
    EventingBasicConsumer consumer = new EventingBasicConsumer(channel);
    channel.BasicConsume(queueName, false, consumer);

    consumer.Received += (sender, e) =>
    {
        Thread.Sleep(1000);
        channel.BasicAck(e.DeliveryTag, false);
        Console.WriteLine(Encoding.UTF8.GetString(e.Body.ToArray())+" alındı");
    };
    Console.Read();

}


using RabbitMQ.Client;
using System.Text;

ConnectionFactory connectionFactory = new ConnectionFactory();
connectionFactory.Uri = new Uri("amqps://nmlgtsid:Hl-ZajX0FHO3eG52BQnrPKc8aQpkYTsd@sparrow.rmq.cloudamqp.com/nmlgtsid");

using(IConnection connection=connectionFactory.CreateConnection())
using(IModel channel=connection.CreateModel())
{
    channel.ExchangeDeclare("iskuyrugu", ExchangeType.Fanout);
	for (int i = 0; i <= 100; i++)
	{
		byte[] byteMessage = Encoding.UTF8.GetBytes($"is {i}");

		IBasicProperties basicProperties = channel.CreateBasicProperties();
		basicProperties.Persistent = true;

		channel.BasicPublish("iskuyrugu", routingKey: "", basicProperties: basicProperties, body: byteMessage);
	}
}
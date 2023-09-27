using System;
namespace miry.async_service.client.Exeptions
{
	public class RabbitMQConnectionException: Exception
    {
		public RabbitMQConnectionException(string message):base(message)
		{
		}
	}
}


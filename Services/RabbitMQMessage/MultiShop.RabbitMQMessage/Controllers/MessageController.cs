using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using System.Text;

namespace MultiShop.RabbitMQMessage.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateMessage()
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost"
            };

            await using var connection = await factory.CreateConnectionAsync();
            await using var channel = await connection.CreateChannelAsync();

            await channel.QueueDeclareAsync(
                queue: "Kuyruk2",
                durable: false,
                exclusive: false,
                autoDelete: false);

            var message = "Merhaba bugün hava çok sıcak!";
            var body = Encoding.UTF8.GetBytes(message);

            await channel.BasicPublishAsync(
                exchange: "",
                routingKey: "Kuyruk2",
                body: body);

            return Ok("Mesajınız alınmıştır!");
        }

        [HttpGet]
        public async Task<IActionResult> ReadMessage()
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost"
            };

            await using var connection = await factory.CreateConnectionAsync();
            await using var channel = await connection.CreateChannelAsync();

            var result = await channel.BasicGetAsync("Kuyruk2", autoAck: true);

            if (result == null)
                return NoContent();

            var message = Encoding.UTF8.GetString(result.Body.ToArray());
            return Ok(message);
        }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipelines;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Sockets;
using PullReqDashboard.API.Models.Response;
using Newtonsoft.Json;
using Microsoft.AspNetCore.SignalR;

namespace PullReqDashboard.API.Utilities
{
    public class SignalRHub : Hub
    {
        private readonly ConnectionList _connectionList = new ConnectionList();

        public async Task SendToAllAsync(IEnumerable<GetPullRequest> data)
        {
            foreach (var connection in _connectionList)
            {
                var ms = new MemoryStream();
                await WriteAsync(data, ms);
                await connection.Transport.Output.WriteAsync(new Message(ms.ToArray(), MessageType.Binary, endOfMessage: true));
            }
        }

        public Task WriteAsync(IEnumerable<GetPullRequest> value, Stream stream)
        {
            var serializer = new JsonSerializer();
            var writer = new JsonTextWriter(new StreamWriter(stream));
            serializer.Serialize(writer, value);
            writer.Flush();
            return Task.FromResult(0);
        }

        public async Task ProcessRequests(Connection connection)
        {
            while (true)
            {
                Message message = await connection.Transport.Input.ReadAsync();
                if (message.Equals(null))
                {

                }
                //var stream = new MemoryStream();
                //await stream.WriteAsync(message.Payload, 0, message.Payload.Length);
                //WeatherReport weatherReport = await formatter.ReadAsync(stream);
                //await _lifetimeManager.SendToAllAsync(weatherReport);
            }
        }
    }
}
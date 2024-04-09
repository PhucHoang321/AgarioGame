using AgarioModels;
using Communications;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TowardTowardAgarioStepThree;

namespace TowardAgarioStepThree
{
    class Program
    {
        private readonly Networking server;
        private readonly List<Food> food = [];
        public Program()
        {
            server = new Networking(NullLogger.Instance, ConnectedToServer,
                                    DisconnectedFromServer, MessageFromServer);
        }

        public static void Main(string[] args)
        {
            Program program = new Program();
            _ = program.StartAsync();
            Console.ReadLine();
        }

        public async Task StartAsync()
        {
            await server.ConnectAsync("2620:9b::190a:2685", 11000);
        }

        private void DisconnectedFromServer(Networking channel)
        {
            Console.WriteLine("Disconnected from server.");
        }

        private void ConnectedToServer(Networking channel)
        {
            Console.WriteLine("Connected to server.");
            _ = server.HandleIncomingDataAsync(true);
        }

        private void MessageFromServer(Networking channel, string message)
        {
            if (message.StartsWith(Protocols.CMD_Food))
            {
                Food fud = JsonSerializer.Deserialize<Food>(message) ?? throw new Exception("bad json");
                Console.WriteLine($"Server Received: {fud.X}, {fud.Y}, {fud.ARGBcolor}, {fud.Mass}");
            }
            else
            {
                // Handle other types of messages if needed
                Console.WriteLine("Received unknown message: " + message);
            }
        }

    }
}
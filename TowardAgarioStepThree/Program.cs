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
using System.Threading.Channels;
using System.Threading.Tasks;
using TowardTowardAgarioStepThree;

namespace TowardAgarioStepThree
{
    class Program
    {
        private readonly Networking server;

        public Program()
        {
            server = new Networking(NullLogger.Instance, ConnectedToServer,
                                    DisconnectedFromServer, MessageFromServer) ;
        }

        public static void Main(string[] args)
        {
            Program program = new Program();
            _ = program.StartAsync();
            Console.ReadLine();
        }

        public async Task StartAsync()
        {
            await server.ConnectAsync("localhost", 11000);
            await server.HandleIncomingDataAsync(true);
       
        }

        private void DisconnectedFromServer(Networking channel)
        {
            Console.WriteLine("Disconnected from server.");
        }

        private void ConnectedToServer(Networking channel)
        {
            Console.WriteLine("Connected to server.");
        }

        private void MessageFromServer(Networking channel, string message)
        {
            if (message.StartsWith(Protocols.CMD_Food))
            {
                message = message[Protocols.CMD_Food.Length..]!;
                Food[]? fud = JsonSerializer.Deserialize<Food[]>(message);
                for(int i = 0; i < fud?.Length ; i++)
                {
                    Console.WriteLine($"Server Received: {fud?[i].X}, {fud?[i].Y}, {fud?[i].ARGBColor}, {fud?[i].Mass}");
                }             
            }
           
        }

    }
}
using HiveMQtt.Client.Options;
using HiveMQtt.Client;
using HiveMQtt.MQTT5.ReasonCodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HiveMQtt.MQTT5.Types;
using System.Text.Json;

namespace SOC.Ticketing
{
    public class TicketingService
    {
        private HiveMQClientOptions options;

        private HiveMQClient client;

        public TicketingService()
        {
            options = new HiveMQClientOptions
             {
                 Host = "",
                 Port = 8883,
                 UseTLS = true,
                 UserName = "admin",
                 Password = "password",
             };

        client = new HiveMQClient(options);
        }
        public async void DisplayConnectionStatus()
        {
            Console.WriteLine($"Connecting to {options.Host} on port {options.Port} ...");

            // Connect
            HiveMQtt.Client.Results.ConnectResult connectResult;
            try
            {
                connectResult = await client.ConnectAsync().ConfigureAwait(false);
                if (connectResult.ReasonCode == ConnAckReasonCode.Success)
                {
                    Console.WriteLine($"Connect successful: {connectResult}");
                }
                else
                {
                    // FIXME: Add ToString
                    Console.WriteLine($"Connect failed: {connectResult}");
                    Environment.Exit(-1);
                }
            }
            catch (System.Net.Sockets.SocketException e)
            {
                Console.WriteLine($"Error connecting to the MQTT Broker with the following socket error: {e.Message}");
                Environment.Exit(-1);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error connecting to the MQTT Broker with the following message: {e.Message}");
                Environment.Exit(-1);
            }
        }

        public async void ReceiveMessages()
        {
            // Message Handler
            client.OnMessageReceived += (sender, args) =>
            {
                string received_message = args.PublishMessage.PayloadAsString;
                Console.WriteLine(received_message);

            };
        }

        public async void Subscribe()
        {
            // Subscribe
            await client.SubscribeAsync("hivemqdemo/commands").ConfigureAwait(false);
        }

        public async void PublishMessage()
        {
            double temperature = 25.1;
            double humidity = 77.5;
            var rand = new Random();
            double currentTemperature = temperature + rand.NextDouble();
            double currentHumidity = humidity + rand.NextDouble();
            var msg = JsonSerializer.Serialize(
                new
                {
                    temperature = currentTemperature,
                    humidity = currentHumidity,
                });
            //Publish MQTT messages
            var result = await client.PublishAsync("hivemqdemo/telemetry", msg, QualityOfService.AtLeastOnceDelivery).ConfigureAwait(false);

        }


    }

}

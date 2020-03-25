using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.EventArgs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;


namespace DadBot
{
    public class Bot
    {
        public DiscordClient Client { get; private set; }
        public CommandsNextExtension Commands { get; private set; }

        public async Task RunAsync() 
        {
            var json = string.Empty;

            using (var fs = File.OpenRead("config.json"))
            using (var sr = new StreamReader(fs, new UTF8Encoding(false)))
                json = await sr.ReadToEndAsync().ConfigureAwait(false);

            var configJson = JsonConvert.DeserializeObject<ConfigJson>(json);

            DiscordConfiguration config = new DiscordConfiguration
            {
                Token = configJson.Token,
                TokenType = TokenType.Bot,
                AutoReconnect = true,
                LogLevel = LogLevel.Debug,
                UseInternalLogHandler = true
            };

            Client = new DiscordClient(config);

            Client.Ready += OnClientReady;

            CommandsNextConfiguration commandsConfig = new CommandsNextConfiguration
            {
                StringPrefixes = new string[] { configJson.Prefix },
                EnableDms = false,
                EnableMentionPrefix = true
            };

            Commands = Client.UseCommandsNext(commandsConfig);

            Client.MessageCreated += Client_MessageCreated;

            await Client.ConnectAsync();

            await Task.Delay(-1);
        }

        private async Task Client_MessageCreated(MessageCreateEventArgs e)
        {
            if (e.Message.Content.StartsWith("im", StringComparison.InvariantCultureIgnoreCase))
            {
                var message = e.Message.Content.Substring(2, e.Message.Content.Length - 2);
                await e.Channel.SendMessageAsync($"Hi,{message}. I'm Dad!");
            } else if (e.Message.Content.StartsWith("i'm", StringComparison.InvariantCultureIgnoreCase)) 
            {
                var message = e.Message.Content.Substring(3, e.Message.Content.Length - 3);
                await e.Channel.SendMessageAsync($"Hi,{message}. I'm Dad!");
            }
        }

        private Task OnClientReady(ReadyEventArgs e) 
        {
            return Task.CompletedTask;
        }
    }
}

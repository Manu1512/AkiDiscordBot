using System;
using System.Reflection;
using System.Threading.Tasks;
using AkiDiscordBot.Modules;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;

namespace AkiDiscordBot
{
    class Program : ModuleBase<SocketCommandContext>
    {
        static void Main(string[] args) => new Program().RunBotAsync().GetAwaiter().GetResult();

        public static ulong joinedServerId;
        public static string joinedServerName;

        public static DiscordSocketClient _client;
        private CommandService _commands;
        private IServiceProvider _services;

        public async Task RunBotAsync()
        {
            Console.WriteLine("DiscordBot Aki, version " + Config.bot.version + "\n");

            UserData.Data();

            _client = new DiscordSocketClient();
            _commands = new CommandService();

            _services = new ServiceCollection()
                .AddSingleton(_client)
                .AddSingleton(_commands)
                .BuildServiceProvider();

            _client.Log += _client_Log;

            await RegisterCommandsAsync();
            await _client.LoginAsync(TokenType.Bot, Config.bot.token);
            await _client.StartAsync();

            await _client.SetGameAsync(Config.bot.cmdPrefix + "help for commands");

            await Task.Delay(-1);
        }

        private Task _client_Log(LogMessage msg)
        {
            Console.WriteLine(msg);

            string message = Convert.ToString(msg);
            if (message.Contains("Joined"))
            {
                joinedServerId = Context.Guild.Id; // ID wird zu diesem Zeitpunkt noch nicht erkannt -> Muss später eingelesen werden
                //joinedServerName = Context.Guild.Name;

                Console.WriteLine(/*joinedServerName + ": " + */joinedServerId);

                UserData.Data();
            }

            return Task.CompletedTask;
        }

        public async Task RegisterCommandsAsync()
        {
            _client.MessageReceived += HandleCommandAsync;
            _client.MessageReceived += Moderation.WordsFilter;
            await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), _services);
        }

        private async Task HandleCommandAsync(SocketMessage msg)
        {
            var message = msg as SocketUserMessage;
            var context = new SocketCommandContext(_client, message);
            if (message.Author.IsBot) return;

            int argPos = 0;
            if (message.HasStringPrefix(Config.bot.cmdPrefix, ref argPos))
            {
                var result = await _commands.ExecuteAsync(context, argPos, _services);
                if (!result.IsSuccess) Console.WriteLine(result.ErrorReason);
            }
        }
    }
}

using System;
using System.IO;
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

            return Task.CompletedTask;
        }

        public async Task RegisterCommandsAsync()
        {
            _client.MessageReceived += HandleCommandAsync;
            _client.MessageReceived += Moderation.WordsFilter;
            _client.JoinedGuild += RegisterJoinAsync;
            _client.LeftGuild += RegisterLeftAsync;
            await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), _services);
        }

        private async Task HandleCommandAsync(SocketMessage msg)
        {
            var message = msg as SocketUserMessage;
            var context = new SocketCommandContext(_client, message);
            if (message.Author.IsBot) return;

            // Die Guild ID auslesen für Pfad
            var channel = message.Channel as SocketGuildChannel;
            var guild = channel.Guild.Id;

            // Die für den Server zuständige Datei öffnen
            string path = UserData.userDataFolder + UserData.userDataFolder02 + guild + "/" + UserData.prefixData;
            string cmdPrefix = File.ReadAllText(path);

            int argPos = 0;
            if (message.HasStringPrefix(cmdPrefix, ref argPos))
            {
                var result = await _commands.ExecuteAsync(context, argPos, _services);
                if (!result.IsSuccess) Console.WriteLine(result.ErrorReason);
            }
        }

        private async Task RegisterJoinAsync(SocketGuild guild)
        {
            joinedServerId = guild.Id;
            joinedServerName = guild.Name;

            Console.WriteLine("[JOIN] Aki has joined a new Server.");
            Console.WriteLine("[JOIN] Name: " + joinedServerId);
            Console.WriteLine("[JOIN] Id: " + joinedServerName);
            UserData.Data();
        }

        private async Task RegisterLeftAsync(SocketGuild guild)
        {
            joinedServerId = guild.Id;
            joinedServerName = guild.Name;

            Console.WriteLine("[LEFT] Aki has left a Server.");
            Console.WriteLine("[LEFT] Name: " + joinedServerId);
            Console.WriteLine("[LEFT] Id: " + joinedServerName);
        }
    }
}

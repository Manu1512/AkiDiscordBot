using Discord.WebSocket;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AkiDiscordBot.Modules
{
    public class Moderation
    {
        static string[] Filter_de = File.ReadAllLines(@"Resources/Filter/Filter-de.txt");
        static string[] Filter_en = File.ReadAllLines(@"Resources/Filter/Filter-en.txt");

        public static async Task WordsFilter(SocketMessage msg)
        {
            string user = msg.Author.Username;

            var channel = msg.Channel as SocketGuildChannel;

            if (Filter_de.Any(word => msg.Content.Contains(word, StringComparison.OrdinalIgnoreCase)) ||
                Filter_en.Any(word => msg.Content.Contains(word, StringComparison.OrdinalIgnoreCase)))
            { 
                await msg.DeleteAsync();

                Console.WriteLine($"[DELETED] {user}: {msg}");
            }
        }
    }
}

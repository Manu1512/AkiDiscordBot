using Discord.WebSocket;
using System;
using System.Threading.Tasks;

namespace AkiDiscordBot.Modules
{
    public class Welcome
    {
        public static async Task WelcomeMsg(SocketUser user)
        {
            Random rnd = new Random();

            ulong welcomeChannelId = 536949141159542804;
            ulong rulesChannelId = 539898667935727656;

            string u = $"<@{user.Id}>";
            string c = $"<#{rulesChannelId}>";

            string rules = $" Bitte lies dir mal die {c} durch.";

            string[] msg =
            {
                $"{u} ist auf dem Server gelandet!",
                $"A wild {u} appeared!",
                $"Bitte begrüßt unseren neuen Gast: {u}.",
                $"{u} ist jetzt neu hier. Seid alle lieb!",
                $"Ist das wirklich wahr? {u} ist da!"
            };

            int i = rnd.Next(0, msg.Length);

            await ((ISocketMessageChannel)Program._client.GetChannel(welcomeChannelId)).SendMessageAsync(msg[i] + rules);
        }
    }
}

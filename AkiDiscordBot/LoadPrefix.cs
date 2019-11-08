using System.IO;

namespace AkiDiscordBot
{
    class LoadPrefix
    {
        public string loadPrefix(ulong guildId)
        {
            string path = UserData.userDataFolder + UserData.userDataFolder02 + guildId + "/" + UserData.prefixData;
            string currentPrefix = File.ReadAllText(path);

            return currentPrefix;
        }
    }
}

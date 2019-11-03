using System.IO;
using Newtonsoft.Json;

namespace AkiDiscordBot
{
    class Config
    {
        private const string configFolder = "Resources";
        private const string configFile = "config.json";

        public const string configPath = configFolder + "/" + configFile;

        public static BotConfig bot;

        static Config()
        {
            if (!Directory.Exists(configFolder))
                Directory.CreateDirectory(configFolder);

            if(!File.Exists(configPath))
            {
                bot = new BotConfig();
                string json = JsonConvert.SerializeObject(bot, Formatting.Indented);
                File.WriteAllText(configPath, json);
            }
            else
            {
                string json = File.ReadAllText(configPath);
                bot = JsonConvert.DeserializeObject<BotConfig>(json);
            }
        }
    }

    public struct BotConfig
    {
        public string version;
        public string token;
        public string cmdPrefix;
    }
}

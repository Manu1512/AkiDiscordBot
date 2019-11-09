using System;
using System.IO;

namespace AkiDiscordBot
{
    public class UserData
    {
        public static ulong joinedServerId;
        public static string joinedServerName;

        public const string userDataFolder = "Resources/";
        public const string userDataFolder02 = "Configs/";
        private static string userDataFolder03 = Convert.ToString(Program.joinedServerId);

        public static string userDataFile = "userConfig.txt";
        public static string prefixData = "prefix.txt";

        public static string userDataPath = userDataFolder + userDataFolder02 + userDataFolder03;

        public string Sender { get; set; }
        public string Receiver { get; set; }
        public string Hugs { get; set; }
        public string Pats { get; set; }

        public static void Data()
        {
            // Create Folders
            if (!Directory.Exists(userDataFolder))
                Directory.CreateDirectory(userDataFolder);
            if (!Directory.Exists(userDataFolder02))
                Directory.CreateDirectory(userDataFolder + "/" + userDataFolder02);
            if (!Directory.Exists(userDataFolder03))
                Directory.CreateDirectory(userDataFolder + "/" + userDataFolder02 + "/" + userDataFolder03);

            // Create Files
            if (!File.Exists(userDataPath + "/" + userDataFile))
                File.Create(userDataPath + "/" + userDataFile);
            if (!File.Exists(userDataPath + "/" + prefixData))
                File.WriteAllText(userDataPath + "/" + prefixData, Config.bot.cmdPrefix);
        }
    }
}

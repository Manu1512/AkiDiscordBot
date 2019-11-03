using System;
using System.IO;

namespace AkiDiscordBot
{
    public class UserData
    {
        private const string userDataFolder = "Resources";
        private const string userDataFolder02 = "Configs";
        private static string userDataFile = Convert.ToString(Program.joinedServerId) + ".txt";

        public static string userDataPath = userDataFolder + "/" + userDataFolder02 + "/" + userDataFile;

        public string Sender { get; set; }
        public string Receiver { get; set; }
        public string Hugs { get; set; }
        public string Pats { get; set; }

        public static void Data()
        {
            //Console.WriteLine(Program.joinedServerName);

            if (!Directory.Exists(userDataFolder))
                Directory.CreateDirectory(userDataFolder);

            if (!Directory.Exists(userDataFolder02))
                Directory.CreateDirectory(userDataFolder + "/" + userDataFolder02);

            if (!File.Exists(userDataPath))
                File.Create(userDataPath);
        }
    }
}

using NLog;
using NLog.Conditions;
using NLog.Config;
using NLog.Layouts;
using NLog.Targets;
using System.Text;
using Faker;
using NLog.Fluent;
using System.IO;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Serialization;

namespace Module_14_NLog
{
    // https://github.com/nlog/nlog/wiki/Tutorial#Configure-NLog-Targets-for-output
    // https://github.com/nlog/nlog/wiki/Configure-from-code
    // https://yougame.biz/threads/200323/
    // Layouts https://nlog-project.org/config/?tab=layouts
    // Targets https://nlog-project.org/config/?tab=targets
    // Layout renderers https://nlog-project.org/config/?tab=layout-renderers

    internal class Program
    {
        public static Logger Logger = LogManager.GetCurrentClassLogger();

        static void Main(string[] args)
        {

            #region NLog Initializator

            var config = new NLog.Config.LoggingConfiguration();

            var Target = new ColoredConsoleTarget()
            {
                Layout = @"[${logger}/${uppercase: ${level}}] >> ${message}"

            };
            config.AddRule(LogLevel.Trace, LogLevel.Info, Target);

            NLog.LogManager.Configuration = config;


            #endregion NLog Initializator
            for (int i = 0; i < 10; i++)
            {
                var user = new FakeUser();
                user.Name = Faker.Name.First();
                user.Surname = Faker.Name.Last();
                user.PhoneNumber = Faker.Phone.Number();
                user.Email = Faker.Internet.Email();
                user.address = Faker.Address.StreetAddress();

                var currentDirectory = System.IO.Directory.GetCurrentDirectory();
                Console.WriteLine($"Fake User: {user}");
                WriteToFile(user);
            };
        }
        static void WriteToFile(FakeUser user)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(FakeUser));
            FileStream file = new FileStream("File.xml", FileMode.Append);
            serializer.Serialize(file, user);
            file.Close();

        }
    }

    public class FakeUser
        {
            public string Name { get; set; }
            public string Surname { get; set; }
            public string PhoneNumber { get; set; }
            public string Email { get; set; }
            public string address { get; set; }
        public override string ToString()
        {
            return "\nName: " + Name +
                "\nSurname: " + Surname +
                "\nPhone Number: " + PhoneNumber +
                "\nEmail: " + Email +
                "\nAddress" + address + "\n";
        }
    }

        
}
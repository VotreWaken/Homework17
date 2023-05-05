using NLog;
using NLog.Conditions;
using NLog.Config;
using NLog.Layouts;
using NLog.Targets;
using System.Text;

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

            try
            {
                string nameFile = "test.txt";
                StreamWriter file = new StreamWriter(nameFile, false);
                string str = "Hello World\n 2023 World Hello\n Module 14 Nlog";
                file.WriteLine(str);
                file.Close();



                int choice = 0;
                do
                {
                    Console.WriteLine("1. A String with One Lowercase English letter");
                    Console.WriteLine("2. String with one number");
                    Console.WriteLine("3. String with one capital English letter");
                    Console.WriteLine("0. Exit");
                    choice = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine();
                    switch (choice)
                    {
                        case 0:
                            return;
                        case 1:
                            {
                                ReadFile_txt(nameFile, 97, 122);
                                Logger.Info("Вывод всех предложений с маленькой англ буквой");
                                break;
                            }
                        case 2:
                            {
                                ReadFile_txt(nameFile, 48, 57);
                                Logger.Info("Вывод всех предложений с цифрой");
                                break;
                            }
                        case 3:
                            {
                                ReadFile_txt(nameFile, 65, 90);
                                Logger.Info("Вывод всех предложений с большой англ буквой");
                                break;
                            }
                        default:
                            {
                                Logger.Info("Была введена неверная операция");
                                break;
                            }

                    }
                    Console.WriteLine();
                    Console.WriteLine();


                } while (choice != 0);

            }
            catch (Exception ex)
            {
                Logger.Error("Error " + ex.Message);
                Logger.Fatal("Fatal Error! " + ex);
            }
        }

        static void ReadFile_txt(string NameFile, int start, int end)
        {

            string line = null;
            bool flag = false;
            StreamReader reader = new StreamReader(NameFile, Encoding.UTF8);
            while (reader.Peek() >= 0)
            {
                line = reader.ReadLine();
                char[] chars = line.ToCharArray();
                for (int i = 0; i < chars.Length; i++)
                {
                    if (chars[i] >= start && chars[i] <= end) flag = true;
                }
                if (flag)
                {
                    Console.WriteLine(line);
                    flag = false;
                }
            }
            reader.Close();
        }
    
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBusManagementConsole
{
    class ManagementConsole
    {
        // Enter a valid Service Bus connection string
        private static string ServiceBusConnectionString = "";



        static void Main(string[] args)
        {
            ManagementHelper helper = new ManagementHelper(ServiceBusConnectionString);



            bool done = false;
            do
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write(">");
                string commandLine = Console.ReadLine();
                Console.ForegroundColor = ConsoleColor.Magenta;
                string[] commands = commandLine.Split(' ');

                try
                {
                    if (commands.Length > 0)
                    {
                        switch (commands[0])
                        {
                            case "createqueue":
                            case "cq":
                                if (commands.Length > 1)
                                {
                                    helper.CreateQueueAsync(commands[1]).Wait();
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                    Console.WriteLine("Queue path not specified.");
                                }
                                break;
                            case "listqueues":
                            case "lq":
                                helper.ListQueuesAsync().Wait();
                                break;
                            case "getqueue":
                            case "gq":
                                if (commands.Length > 1)
                                {
                                    helper.GetQueueAsync(commands[1]).Wait();
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                    Console.WriteLine("Queue path not specified.");
                                }
                                break;
                            case "deletequeue":
                            case "dq":
                                if (commands.Length > 1)
                                {
                                    helper.DeleteQueueAsync(commands[1]).Wait();
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                    Console.WriteLine("Queue path not specified.");
                                }
                                break;
                            case "createtopic":
                            case "ct":
                                if (commands.Length > 1)
                                {
                                    helper.CreateTopicAsync(commands[1]).Wait();
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                    Console.WriteLine("Topic path not specified.");
                                }
                                break;
                            case "createsubscription":
                            case "cs":
                                if (commands.Length > 2)
                                {
                                    helper.CreateSubscriptionAsync(commands[1], commands[2]).Wait();
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                    Console.WriteLine("Topic path not specified.");
                                }
                                break;
                            case "listtopics":
                            case "lt":
                                helper.ListTopicsAndSubscriptionsAsync().Wait();
                                break;
                            case "exit":
                                done = true;
                                break;
                            default:
                                break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                }

            } while (!done);
        }
    }
}

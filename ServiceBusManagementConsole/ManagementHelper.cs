using Microsoft.Azure.ServiceBus.Management;

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;

namespace ServiceBusManagementConsole
{
    class ManagementHelper
    {

        
        private ManagementClient m_ManagementClient;

        public ManagementHelper(string connectionString)
        {
            // Using the specified credentials.
            m_ManagementClient = new ManagementClient(connectionString);

        }



        public async Task CreateQueueAsync(string queuePath)
        {
            Console.Write("Creating queue {0}...", queuePath);
            var description = GetQueueDescription(queuePath);
            var createdDescription = await m_ManagementClient.CreateQueueAsync(description);
            Console.WriteLine("Done!");
        }

        public async Task DeleteQueueAsync(string queuePath)
        {
            Console.Write("Deleting queue {0}...", queuePath);
            await m_ManagementClient.DeleteQueueAsync(queuePath);
            Console.WriteLine("Done!");
        }

        public async Task ListQueuesAsync()
        {
            IEnumerable<QueueDescription> queueDescriptions = await m_ManagementClient.GetQueuesAsync();
            Console.WriteLine("Listing queues...");
            foreach (QueueDescription queueDescription in queueDescriptions)
            {
                Console.WriteLine("\t{0}", queueDescription.Path);
            }
            Console.WriteLine("Done!");
        }

        public async Task GetQueueAsync(string queuePath)
        {
            QueueDescription queueDescription = await m_ManagementClient.GetQueueAsync(queuePath);
            Console.WriteLine($"Queue description for { queuePath }");
            Console.WriteLine($"    Path:                                   { queueDescription.Path }");
            Console.WriteLine($"    MaxSizeInMB:                            { queueDescription.MaxSizeInMB }");
            Console.WriteLine($"    RequiresSession:                        { queueDescription.RequiresSession }");
            Console.WriteLine($"    RequiresDuplicateDetection:             { queueDescription.RequiresDuplicateDetection }");
            Console.WriteLine($"    DuplicateDetectionHistoryTimeWindow:    { queueDescription.DuplicateDetectionHistoryTimeWindow }");
            Console.WriteLine($"    LockDuration:                           { queueDescription.LockDuration }");
            Console.WriteLine($"    DefaultMessageTimeToLive:               { queueDescription.DefaultMessageTimeToLive }");
            Console.WriteLine($"    EnableDeadLetteringOnMessageExpiration: { queueDescription.EnableDeadLetteringOnMessageExpiration }");
            Console.WriteLine($"    EnableBatchedOperations:                {  queueDescription.EnableBatchedOperations }");
            Console.WriteLine($"    MaxSizeInMegabytes:                     { queueDescription.MaxSizeInMB }");
            Console.WriteLine($"    MaxDeliveryCount:                       { queueDescription.MaxDeliveryCount }");
            Console.WriteLine($"    Status:                                 { queueDescription.Status }");
        }



        public async Task CreateTopicAsync(string topicPath)
        {
            Console.Write("Creating topic {0}...", topicPath);
            var description = await m_ManagementClient.CreateTopicAsync(topicPath);
            Console.WriteLine("Done!");
        }


        public async Task CreateSubscriptionAsync(string topicPath, string subscriptionName)
        {
            Console.Write("Creating subscription {0}/subscriptions/{1}...", topicPath, subscriptionName);
            var description = await m_ManagementClient.CreateSubscriptionAsync(topicPath, subscriptionName);
            Console.WriteLine("Done!");
        }


        public async Task ListTopicsAndSubscriptionsAsync()
        {
            IEnumerable<TopicDescription> topicDescriptions = await m_ManagementClient.GetTopicsAsync();
            Console.WriteLine("Listing topics and subscriptions...");
            foreach (TopicDescription topicDescription in topicDescriptions)
            {
                Console.WriteLine("\t{0}", topicDescription.Path);
                IEnumerable<SubscriptionDescription> subscriptionDescriptions = await m_ManagementClient.GetSubscriptionsAsync(topicDescription.Path);
                foreach (SubscriptionDescription subscriptionDescription in subscriptionDescriptions)
                {
                    Console.WriteLine("\t\t{0}", subscriptionDescription.SubscriptionName);
                }
            }
            Console.WriteLine("Done!");
        }




        public QueueDescription GetQueueDescription(string path)
        {
            return new QueueDescription(path)
            {
                //RequiresDuplicateDetection = true,
                //DuplicateDetectionHistoryTimeWindow = TimeSpan.FromMinutes(10),
                //RequiresSession = true,
                //MaxDeliveryCount = 20,
                //DefaultMessageTimeToLive = TimeSpan.FromHours(1),
                //EnableDeadLetteringOnMessageExpiration = true
            };
        }


    }
}

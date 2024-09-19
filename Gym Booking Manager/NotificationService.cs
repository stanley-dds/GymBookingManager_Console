using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym_Booking_Manager
{
    internal class NotificationService
    {
        private static string logFilePath = "notifications_log.txt";

        // method to log data
        public static void SendNotification(string message)
        {
            
            using (StreamWriter writer = new StreamWriter(logFilePath, true))
            {
                writer.WriteLine($"{DateTime.Now}: {message}");
            }

            Console.WriteLine($"Notification logged: {message}");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Gym_Booking_Manager
{
    internal class Reservation
    {
        [JsonConverter(typeof(UserConverter))]
        public User Client { get; set; }
        public string ReservationType { get; set; } 
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool IsActive { get; set; } = true; 


       
        public Reservation(User client, string reservationType, DateTime startTime, DateTime endTime)
        {
            Client = client;
            ReservationType = reservationType;
            StartTime = startTime;
            EndTime = endTime;
            IsActive = true;
        }


       
        public void Cancel()
        {
            IsActive = false;
            Console.WriteLine($"Reservation for {ReservationType} by {Client.Name} has been cancelled.");
           
        }

        
        public bool IsAvailable(DateTime requestedStartTime, DateTime requestedEndTime)
        {
           
            if (IsActive && (requestedStartTime < EndTime && requestedEndTime > StartTime))
            {
                return false;
            }
            return true;
        }
    }
}

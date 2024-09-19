using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace Gym_Booking_Manager
{
    internal class GroupSession
    {
        public string Name { get; set; }

        [JsonConverter(typeof(UserConverter))]
        public User Coach { get; set; }  
        public DateTime StartTime { get; set; }
        public int Capacity { get; set; } 
        private List<User> participants; 

        public GroupSession(string name, User coach, DateTime startTime, int capacity)
        {
            Name = name;
            Coach = coach;
            StartTime = startTime;
            Capacity = capacity;
            participants = new List<User>();
        }


        
        public bool RegisterClient(User client)
        {
            if (participants.Count < Capacity)
            {
                participants.Add(client);
                return true;
            }
            else
            {
                return false; 
            }
        }

        
        public void ShowParticipants()
        {
            Console.WriteLine($"Participants for '{Name}':");
            foreach (var participant in participants)
            {
                Console.WriteLine(participant.Name);
            }
        }
    }
}

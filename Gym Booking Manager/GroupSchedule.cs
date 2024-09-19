using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

#if DEBUG
[assembly: InternalsVisibleTo("GymBookingManager.Tests")]
#endif

namespace Gym_Booking_Manager
{
    internal class GroupSchedule
    {
        
        private List<GroupSession> groupSessions;

        public GroupSchedule()
        {
            groupSessions = new List<GroupSession>();
        }

        
        public void AddGroupSession(GroupSession session)
        {
            groupSessions.Add(session);
            Console.WriteLine($"Group session '{session.Name}' has been successfully added.");
        }

       
        public List<GroupSession> GetSessions()
        {
            return groupSessions;
        }

        
        public void SaveToFile(string filePath)
        {
            string jsonData = JsonConvert.SerializeObject(groupSessions, Formatting.Indented);
            File.WriteAllText(filePath, jsonData);
        }


        public void LoadFromFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                var settings = new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All
                };
                string jsonData = File.ReadAllText(filePath);
                var loadedSessions = JsonConvert.DeserializeObject<List<GroupSession>>(jsonData, settings);
                if (loadedSessions != null)
                {
                    groupSessions.Clear();
                    groupSessions.AddRange(loadedSessions);
                    Console.WriteLine($"Data loaded from {filePath}");
                }
            }
            else
            {
                Console.WriteLine($"File {filePath} not found. Starting with an empty group session list.");
            }
        }


        public void ShowGroupSessions()
        {
            Console.WriteLine("Current group sessions:");
            foreach (var session in groupSessions)
            {
                Console.WriteLine($"{session.Name} with {session.Coach.Name} on {session.StartTime}");
            }
        }

        
        public bool RegisterClient(User client, GroupSession session)
        {
            if (session.RegisterClient(client))
            {
                Console.WriteLine($"{client.Name} has been successfully registered for '{session.Name}'.");
                return true;
            }
            else
            {
                Console.WriteLine("Registration failed. The session may be full.");
                return false;
            }
        }
    }
}


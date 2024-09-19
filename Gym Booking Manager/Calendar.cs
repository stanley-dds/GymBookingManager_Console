// Placeholder name for file until we get a more complete grasp of classes in the system
// and the organisation thereof.


using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

#if DEBUG
[assembly: InternalsVisibleTo("GymBookingManager.Tests")]
#endif
namespace Gym_Booking_Manager
{
    internal class Calendar
    {
        private readonly List<Reservation> reservations;

        public Calendar()
        {
            this.reservations = new List<Reservation>();
        }

        // Leaving this method for now. Idea being it may be useful to get entries within a "start" and "end" time/date range.
        // Need parameters if so.
        // Or maybe we'll come up with a better solution elsewhere.
        public List<Reservation> GetSlice()
        {
            return this.reservations; // Promise to implement or delete this later, please just compile.
        }



        public bool AddReservation(Reservation reservation)
        {
            foreach (var existingReservation in reservations)
            {
                if (!existingReservation.IsAvailable(reservation.StartTime, reservation.EndTime))
                {
                    return false;
                }
            }
            reservations.Add(reservation);
            Logger.Log($"Reservation added: {reservation.Client.Name} - {reservation.ReservationType}");
            return true;
        }



        public bool CancelReservation(Reservation reservation)
        {
            if (reservations.Contains(reservation))
            {
                reservation.Cancel();
                Logger.Log($"Reservation cancelled: {reservation.Client.Name} - {reservation.ReservationType}");
                return true;
            }
            return false;
        }



        public List<Reservation> GetReservations()
        {
            return reservations;
        }




        public void SaveToFile(string filePath)
        {
            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            };
            string jsonData = JsonConvert.SerializeObject(reservations, Formatting.Indented, settings);
            File.WriteAllText(filePath, jsonData);
            Console.WriteLine($"Data saved to {filePath}");
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
                var loadedReservations = JsonConvert.DeserializeObject<List<Reservation>>(jsonData, settings);
                if (loadedReservations != null)
                {
                    reservations.Clear();
                    reservations.AddRange(loadedReservations);
                    Console.WriteLine($"Data loaded from {filePath}");
                }
            }
            else
            {
                Console.WriteLine($"File {filePath} not found. Starting with an empty reservation list.");
            }
        }






        public void ShowReservations()
        {
            Console.WriteLine("Current reservations:");
            foreach (var reservation in reservations)
            {
                Console.WriteLine($"{reservation.ReservationType} reserved by {reservation.Client.Name} from {reservation.StartTime} to {reservation.EndTime}. Status: {(reservation.IsActive ? "Active" : "Cancelled")}");
            }
        }
    }
}
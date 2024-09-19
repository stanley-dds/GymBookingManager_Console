using System.Runtime.CompilerServices;


#if DEBUG
[assembly: InternalsVisibleTo("GymBookingManager.Tests")]
#endif


namespace Gym_Booking_Manager
{
    internal class Program
    {

        static string reservationsFilePath = Path.Combine(Environment.CurrentDirectory, "reservations.json");
        static string groupSessionsFilePath = Path.Combine(Environment.CurrentDirectory, "group_sessions.json");


        static void Main(string[] args)
        {
            Calendar calendar = new Calendar();
            GroupSchedule groupSchedule = new GroupSchedule();

 
            calendar.LoadFromFile(reservationsFilePath);
            groupSchedule.LoadFromFile(groupSessionsFilePath);

            while (true)
            {
                Console.WriteLine("Welcome to Gym Booking Manager");
                Console.WriteLine("1. Add Reservation");
                Console.WriteLine("2. Cancel Reservation");
                Console.WriteLine("3. Show All Reservations");
                Console.WriteLine("4. Add Group Session");
                Console.WriteLine("5. Register for Group Session");
                Console.WriteLine("6. Show All Group Sessions");
                Console.WriteLine("7. Exit");
                Console.WriteLine("DONT FORGET TO PRESS 7 IN THE END TO SAVE CHANGES");
                Console.Write("Please select an option: ");
                string option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        AddReservation(calendar);
                        break;
                    case "2":
                        CancelReservation(calendar);
                        break;
                    case "3":
                        calendar.ShowReservations();
                        break;
                    case "4":
                        AddGroupSession(groupSchedule);
                        break;
                    case "5":
                        RegisterForGroupSession(groupSchedule);
                        break;
                    case "6":
                        groupSchedule.ShowGroupSessions();
                        break;
                    case "7":
                        calendar.SaveToFile(reservationsFilePath);
                        groupSchedule.SaveToFile(groupSessionsFilePath);
                        return;
                    default:
                        Console.WriteLine("Invalid option, please try again.");
                        break;
                }
            }
        }




        // Static methods for the program


        static void AddGroupSession(GroupSchedule groupSchedule)
        {
            Console.Write("Enter group session name: ");
            string sessionName = Console.ReadLine();

            Console.Write("Enter coach name: ");
            string coachName = Console.ReadLine();

            Console.Write("Enter start time (yyyy-mm-dd hh:mm): ");
            DateTime startTime = DateTime.Parse(Console.ReadLine());

            Console.Write("Enter maximum participants: ");
            int capacity = int.Parse(Console.ReadLine());

          
            Coach coach = new Coach(coachName, "coach@gym.com", "123456789"); 
            GroupSession session = new GroupSession(sessionName, coach, startTime, capacity);

            groupSchedule.AddGroupSession(session);
        }

        static void RegisterForGroupSession(GroupSchedule groupSchedule)
        {
            Console.Write("Enter client name: ");
            string clientName = Console.ReadLine();

            Console.Write("Enter session name: ");
            string sessionName = Console.ReadLine();

           
            Customer client = new Customer(clientName, "client@gym.com", "123456789", true); 
            foreach (var session in groupSchedule.GetSessions())
            {
                if (session.Name == sessionName)
                {
                    groupSchedule.RegisterClient(client, session);
                    return;
                }
            }

            Console.WriteLine("Session not found.");
        }   

        static void AddReservation(Calendar calendar)
        {
            Console.Write("Enter client name: ");
            string clientName = Console.ReadLine();

            Console.Write("Enter client email: ");
            string clientEmail = Console.ReadLine();

            Console.Write("Enter client phone number: ");
            string clientPhoneNumber = Console.ReadLine();

            Console.Write("Is the client a member? (yes/no): ");
            bool isMember = Console.ReadLine().ToLower() == "yes";

            
            Customer client = new Customer(clientName, clientEmail, clientPhoneNumber, isMember);

            Console.Write("Enter reservation type (Equipment, Trainer, GroupSession): ");
            string reservationType = Console.ReadLine();

            Console.Write("Enter start time (yyyy-mm-dd hh:mm): ");
            DateTime startTime = DateTime.Parse(Console.ReadLine());

            Console.Write("Enter end time (yyyy-mm-dd hh:mm): ");
            DateTime endTime = DateTime.Parse(Console.ReadLine());

            Reservation reservation = new Reservation(client, reservationType, startTime, endTime);

            if (calendar.AddReservation(reservation))
            {
                Console.WriteLine("Reservation added successfully.");
            }
            else
            {
                Console.WriteLine("Failed to add reservation.");
            }
        }


        static void CancelReservation(Calendar calendar)
        {
            Console.Write("Enter client name: ");
            string clientName = Console.ReadLine();

            Console.Write("Enter start time of reservation to cancel (yyyy-mm-dd hh:mm): ");
            DateTime startTime = DateTime.Parse(Console.ReadLine());


            foreach (var reservation in calendar.GetReservations())
            {
                if (reservation.Client.Name == clientName && reservation.StartTime == startTime)
                {
                    if (calendar.CancelReservation(reservation))
                    {
                        Console.WriteLine("Reservation cancelled successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Failed to cancel reservation.");
                    }
                    return;
                }
            }
            Console.WriteLine("Reservation not found.");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

#if DEBUG
[assembly: InternalsVisibleTo("GymBookingManager.Tests")]
#endif
namespace Gym_Booking_Manager
{
    internal abstract class User
    {
        public string Name { get; set; } 
        public string Email { get; set; }
        public string PhoneNumber { get; set; }


        public User(string name, string email, string phoneNumber)
        {
            Name = name;
            Email = email;
            PhoneNumber = phoneNumber;
        }
    }
}
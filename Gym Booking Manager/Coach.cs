using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym_Booking_Manager
{
    internal class Coach : User
    {

        public Coach(string name, string email, string phoneNumber)
        : base(name, email, phoneNumber)
        {
        }
    }
}

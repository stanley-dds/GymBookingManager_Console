using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Gym_Booking_Manager
{
    internal class Customer : User
    {
        public bool IsMember { get; set; }

        public Customer(string name, string email, string phoneNumber, bool isMember)
            : base(name, email, phoneNumber)
        {
            IsMember = isMember;
        }
    }
}

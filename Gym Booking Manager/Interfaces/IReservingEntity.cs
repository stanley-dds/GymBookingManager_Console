using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym_Booking_Manager
{
    internal interface IReservingEntity
    {
        string Name { get; set; } 
        bool IsAvailable(DateTime startTime, DateTime endTime); 
    }

}

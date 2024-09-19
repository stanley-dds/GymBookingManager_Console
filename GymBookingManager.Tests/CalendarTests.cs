using NUnit.Framework;
using System;
using Gym_Booking_Manager;



namespace GymBookingManager.Tests
{
    [TestFixture]
    public class CalendarTests
    {
        private Gym_Booking_Manager.Calendar calendar;

        [SetUp]
        public void Setup()
        {
            calendar = new Gym_Booking_Manager.Calendar();
        }

        [Test]
        public void CancelReservation_ShouldCancelReservation_WhenReservationExists()
        {
            var customer = new Customer("Test Client", "client@test.com", "123456789", true);
            var reservation = new Reservation(customer, "Equipment", DateTime.Now, DateTime.Now.AddHours(1));
            calendar.AddReservation(reservation);

            var result = calendar.CancelReservation(reservation);

            Assert.IsTrue(result);  
            Assert.IsFalse(reservation.IsActive);  
        }

        [Test]
        public void CancelReservation_ShouldReturnFalse_WhenReservationDoesNotExist()
        {
            var customer = new Customer("Test Client", "client@test.com", "123456789", true);
            var reservation = new Reservation(customer, "Equipment", DateTime.Now, DateTime.Now.AddHours(1));

            var result = calendar.CancelReservation(reservation);

            Assert.IsFalse(result);  
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SettlementService.API.Controllers;
using SettlementService.Interfaces;

namespace SettlementService.API.Test.Controllers
{
    [TestFixture]
    public class BookingControllerTests
    {
        private Mock<IBookingService> bookingService;

        [SetUp]
        public void Setup()
        {
            bookingService = new Mock<IBookingService>();
        }

        [Test]
        public void AddBooking_WhenCalled_ReturnsOkResult()
        {
            var controller = new BookingController(bookingService.Object);

            var result = controller.AddBooking();
            
            Assert.IsInstanceOf<OkResult>(result);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using OMSSample.Controllers;
using OMSSample.Models;
using QuickFix;
using Xunit;

namespace OMSSample.Tests.Unit_Tests
{
    public class OmsSampleTests
    {
        [Fact]
        public void OrderAmount_SetValidValue_Success()
        {
            var omsSample = new OmsSample
            {
                OrderAmount = 10
            };

            Assert.Equal(10u, omsSample.OrderAmount);
        }

        [Theory]
        [InlineData(0)]
        public void OrderAmount_SetInvalidValue_ThrowsArgumentException(uint value)
        {
            var omsSample = new OmsSample();

            Assert.Throws<ArgumentException>(() => omsSample.OrderAmount = value);
        }

        [Fact]
        public void OrderSymbol_SetValidValue_Success()
        {
            var omsSample = new OmsSample
            {
                OrderSymbol = "ABC"
            };

            Assert.Equal("ABC", omsSample.OrderSymbol);
        }

        [Fact]
        public void OrderSymbol_SetNullValue_ThrowsArgumentNullException()
        {
            var omsSample = new OmsSample();

            Assert.Throws<ArgumentNullException>(() => omsSample.OrderSymbol = null);
        }
        
        [Fact]
        public void SendNewOrder_ValidFields_ReturnsOkResult()
        {
            var controller = new OmsSampleController();
            var fields = new OmsSample
            {
                OrderSymbol = "AAPL",
                OrderAmount = 100,
                Price = 150.50m
            };
            
            var result = controller.SendNewOrder(fields);
            
            Assert.IsType<OkObjectResult>(result);
        }
        
    }
}
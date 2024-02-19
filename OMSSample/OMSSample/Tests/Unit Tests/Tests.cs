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
        public void AddToDb_ValidKeyAndList_Success()
        {
            var model = new Model();
            const string? key = "key";
            var list = new List<OmsSample>();

            model.AddToDb(key, list);

            Assert.True(model.ContainsKey(key));
        }

        [Fact]
        public void AddToDb_NullKey_ThrowsArgumentNullException()
        {
            var model = new Model();
            string? key = null;
            var list = new List<OmsSample>();

            Assert.Throws<ArgumentNullException>(() => model.AddToDb(key, list));
        }

        [Fact]
        public void AddToDb_DuplicateKey_ThrowsArgumentException()
        {
            var model = new Model();
            const string key = "a";
            var list1 = new List<OmsSample>();
            var list2 = new List<OmsSample>();

            model.AddToDb(key, list1);

            Assert.Throws<ArgumentException>(() => model.AddToDb(key, list2));
        }

        [Fact]
        public void ContainsKey_ExistingKey_ReturnsTrue()
        {
            var model = new Model();
            var key = "key";
            var list = new List<OmsSample>();
            model.AddToDb(key, list);

            var result = model.ContainsKey(key);

            Assert.True(result);
        }

        [Fact]
        public void ContainsKey_NonExistingKey_ReturnsFalse()
        {
            var model = new Model();
            const string key = "key";

            var result = model.ContainsKey(key);

            Assert.False(result);
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
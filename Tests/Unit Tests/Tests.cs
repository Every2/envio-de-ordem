using Microsoft.EntityFrameworkCore;
using OMSSample.Models;
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

        private class NewOrderSingleTest
        {
            public string? ClOrdId { get; set; }
            public string? Symbol { get; set; }
            public char Side { get; set; }
            public char OrdType { get; set; }
            public decimal Price { get; set; }
            public uint OrderQty { get; set; }
        }

        [Fact]
        public void NewOrderSingle_PropertiesSetCorrectly()
        {
            var order = new NewOrderSingleTest()
            {
                ClOrdId = "123456", Symbol = "AAPL", Side = '1', OrdType = '2',
                Price = 10.5m, OrderQty = 10u
            };


            Assert.Equal("123456", order.ClOrdId);
            Assert.Equal("AAPL", order.Symbol);
            Assert.Equal('1', order.Side);
            Assert.Equal('2', order.OrdType);
            Assert.Equal(10.5m, order.Price);
            Assert.Equal(10u, order.OrderQty);
        }

        [Fact]
        public void NewOrderSingle_InvalidValues()
        {
            var order = new NewOrderSingleTest
            {
                ClOrdId = "",
                Symbol = null,
                Side = 'X',
                OrdType = 'Z',
                Price = -10.5m,
                OrderQty = 0u
            };

            Assert.NotEqual("123456", order.ClOrdId);
            Assert.NotEqual("AAPL", order.Symbol);
            Assert.NotEqual('1', order.Side);
            Assert.NotEqual('2', order.OrdType);
            Assert.NotEqual(10.5m, order.Price);
            Assert.NotEqual(10u, order.OrderQty);
        }

        public class InMemoryContext : DbContext
        {
            public DbSet<OrderSingle>? OrderSingles { get; set; }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString());
            }
        }


        [Fact]
        public void Can_Add_OrderSingle_To_Database()
        {
            using (var context = new InMemoryContext())
            {
                var orderSingle = new OrderSingle
                {
                    ClOrdId = "1",
                    Symbol = "1",
                    OrdQty = 10u,
                    OrdType = '2',
                    Price = 10m,
                    Side = '1',
                };


                context.OrderSingles.Add(orderSingle);
                context.SaveChanges();


                Assert.NotEqual("", orderSingle.ClOrdId);
            }
        }
    }
}
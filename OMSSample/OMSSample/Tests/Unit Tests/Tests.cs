 using Microsoft.AspNetCore.Mvc;
 using OMSSample.Controllers;
 using OMSSample.Models;
 using Xunit;

 namespace OMSSample.Tests.Unit_Tests;

 public class Tests
 {
     [Fact]
     public void OrderAmount_SetValidValue_ShouldSetOrderAmout()
     {
         var sample = new OmsSample();
         sample.OrderAmount = 10;

         Assert.Equal(10u, sample.OrderAmount);
     }

     [Fact]
     public void OrderAmount_SetInvalidValue_ShouldThrowArgumentException()
     {
         var sample = new OmsSample();

         Assert.Throws<ArgumentException>(() => sample.OrderAmount = 0);
     }

     [Fact]
     public void Price_SetAndGet_ShouldWorkCorrectly()
     {
         var sample = new OmsSample();
         decimal expectedPrice = 10.5m;

         sample.Price = expectedPrice;

         Assert.Equal(expectedPrice, sample.Price);
     }

     [Fact]
     public void OrderSymbol_SetAndGet_ShouldWorkCorrectly()
     {
         var sample = new OmsSample();
         string? expectedSymbol = "Teste";

         sample.OrderSymbol = expectedSymbol;

         Assert.Equal(expectedSymbol, sample.OrderSymbol);
     }

     [Fact]
     public void AddToDb_Should_Add_Element()
     {
         var model = new Model();
         var key = "key";
         var sampleList = new List<OmsSample>
         {
             new OmsSample { OrderAmount = 1, OrderSymbol = "ABC", Price = 10.5m },
             new OmsSample { OrderAmount = 2, OrderSymbol = "DEF", Price = 20.7m }
         };


         model.AddToDb(key, sampleList);

         Assert.True(model.GetFromDb(key) != null);
         Assert.Equal(sampleList, model.GetFromDb(key));
     }

     [Fact]
     public void Delete_Should_Remove_Element()
     {
         var model = new Model();
         var key = "key";
         var sampleList = new List<OmsSample>
         {
             new OmsSample { OrderAmount = 1, OrderSymbol = "ABC", Price = 10.5m },
             new OmsSample { OrderAmount = 2, OrderSymbol = "DEF", Price = 20.7m }
         };
         var database = new Dictionary<string, List<OmsSample>> { { key, sampleList } };

         model.Delete(key);
         Assert.False(model.ContainsKey(key));
     }

     
 }
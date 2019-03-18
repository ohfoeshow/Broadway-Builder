﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataAccessLayer;
using ServiceLayer.Services;

namespace ServiceLayer.Test
{
    [TestClass]
    public class ProductionServiceTest
    {
        [TestMethod]
        public void ProductionService_CreateProduction_Pass()
        {
            var dbcontext = new BroadwayBuilderContext();
            var theaterService = new TheaterService(dbcontext);

            // Arrange
            var theater = new Theater("someTheater", "Regal", "theater st", "LA", "CA", "US", "323323");
            theaterService.CreateTheater(theater);
            dbcontext.SaveChanges();

            var productionName = "The Lion King";
            var directorFirstName = "Jane";
            var directorLastName = "Doe";
            var street = "Anahiem";
            var city = "Long Beach";
            var stateProvince = "California";
            var country = "United States";
            var zipcode = "919293";

            
            var production = new Production(theater.TheaterID, productionName, directorFirstName, directorLastName, street, city, stateProvince, country, zipcode);

            var expected = true;
            var actual = false;

            var productionService = new ProductionService(dbcontext);


            // Act
            productionService.CreateProduction(production);
            var affectedRows = dbcontext.SaveChanges();

            if (affectedRows > 0) 
                actual = true;

            // Assert
            productionService.DeleteProduction(production);
            dbcontext.SaveChanges();
            theaterService.DeleteTheater(theater);
            dbcontext.SaveChanges();
            Assert.AreEqual(expected, actual);


        }

        [TestMethod]
        public void ProductionService_UpdateProduction_Pass()
        {
            // Arrange
            var dbcontext = new BroadwayBuilderContext();
            var theaterService = new TheaterService(dbcontext);

            var theater = new Theater("The Magicians", "Regal", "theater st", "LA", "CA", "US", "323323");
            theaterService.CreateTheater(theater);
            dbcontext.SaveChanges();

            var productionService = new ProductionService(dbcontext);

            var productionName = "The Lion King";
            var directorFirstName = "Joan";
            var directorLastName = "Doe";
            var street = "123 Anahiem St";
            var city = "Long Beach";
            var stateProvince = "California";
            var country = "United States";
            var zipcode = "919293";

            var production = new Production(theater.TheaterID, productionName, directorFirstName, directorLastName, street, city, stateProvince, country, zipcode);

            productionService.CreateProduction(production);
            dbcontext.SaveChanges();


            production.ProductionName = "The Lion King 2";
            production.StateProvince = "Utah";
            production.DirectorLastName = "Mangos";

            var expected = production;
           
            // Act
            var actual = productionService.UpdateProduction(production);
            dbcontext.SaveChanges();

            //productionService.DeleteProduction(production);
            //dbcontext.SaveChanges();

        }


    }
}

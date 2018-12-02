﻿using System;
using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataAccessLayer.Enums;
using ServiceLayer.Models;
using DataAccessLayer;

namespace ServiceLayer.Test
{
    [TestClass]
    public class UserServiceTest
    {
        [TestMethod]
        public void UserService_CreateUser_Pass()
        {
            // Arrange
            var username = "abixcastro@gmail.com";
            var password = "abc123@@@!!!";
            var dob = new DateTime(1994, 1, 7);
            var city = "San Diego";
            var stateProvince = "California";
            var country = "United States";
            var role = RoleType.GENERAL;

            var user = new User(username, password, dob, city, stateProvince, country, role);

            var expected = true;
            var actual = false;

            var context = new BroadwayBuilderContext();
            var repository = new UserRepository(context);
            var service = new UserService(repository);

            // Act
            actual = service.CreateUser(user);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void UserService_CreateUserAgain_Pass() //CreateUser_duplicateUsername_ThrowException()
        {
            // Arrange
            var username = "abixcastro@gmail.com";
            var password = "abc123@@@!!!";
            var dob = new DateTime(1994, 1, 7);
            var city = "San Diego";
            var stateProvince = "California";
            var country = "United States";
            var role = RoleType.GENERAL;

            User user = new User(username, password, dob, city, stateProvince, country, role);

            // Expected should not pass because
            // you cannot create the same user again
            var expected = false;
            var actual = false;

            var context = new BroadwayBuilderContext();
            var repository = new UserRepository(context);
            var service = new UserService(repository);

            // Act
            // Create user the first time
            var createUserFirstTime = service.CreateUser(user);

            // Create user again
            actual = service.CreateUser(user);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void UpdateUser_Pass()
        {
            //Arrange
            var context = new BroadwayBuilderContext();
            var repository = new UserRepository(context);
            var userService = new UserService(repository);

            var username = "fakejakejohnson@gmail.com";
            var password = "h@rDt0GeU$$P@$$word!!!";
            var dob = new DateTime(1992, 12, 7);
            var city = "Irvine";
            var stateProvince = "California";
            var country = "United States";
            var role = RoleType.GENERAL;

            User user = new User(username, password, dob, city, stateProvince, country, role);

          

            var expected = user;
            var actual = 0;

        }

       

        // TODO: Create more test cases for UserManagement
        // TODO: Exception thrown from repository
        // TODO: UpdateUser() Test
        // 
    }
}
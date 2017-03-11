using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using People.Model.Service;
using Moq;
using Newtonsoft.Json.Linq;
using People.Model.Db;
using PeopleSearch.Web.Controllers;

namespace PeopleSearch.Web.Tests
{
    [TestClass]
    public class PeopleSearchControllerTest
    {
        [TestMethod]
        public void TestGetPersonList()
        {
            var personService = new Mock<IPersonService>();
            var interestService = new Mock<IInterestService>();
            var persons = new List<Person>
            {
                new Person
                {
                    FirstName = "Joe", LastName = "Green", Age = 33, PhotoUrl = "image1.jpg"},
                new Person {
                    FirstName = "Robert", LastName = "Harris", Age = 21, PhotoUrl = "image2.jpg" },
                new Person {
                    FirstName = "Mary", LastName = "Jones", Age = 25, PhotoUrl = "image3.jpg" }
            }.AsQueryable();

            personService.Setup(p => p.GetAll()).Returns(persons);

            var controller = new PeopleSearchController(personService.Object, interestService.Object);
            var result = controller.GetPersonList();
            
        }

        [TestMethod]
        public void TestGetAllInterest()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void TestAddPerson()
        {
            throw new NotImplementedException();
        }
    }
}

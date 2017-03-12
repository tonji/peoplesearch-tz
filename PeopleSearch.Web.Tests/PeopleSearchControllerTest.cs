using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Script.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using People.Model.Service;
using Moq;
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
                    Id = 1,
                    FirstName = "Joe",
                    LastName = "Green",
                    Age = 33,
                    PhotoUrl = "image1.jpg"
                },
                new Person
                {
                    Id = 2,
                    FirstName = "Robert",
                    LastName = "Harris",
                    Age = 21,
                    PhotoUrl = "image2.jpg"
                },
                new Person
                {
                    Id = 3,
                    FirstName = "Mary",
                    LastName = "Jones",
                    Age = 25,
                    PhotoUrl = "image3.jpg"
                }
            }.AsQueryable();

            personService.Setup(p => p.GetAll()).Returns(persons);

            var controller = new PeopleSearchController(personService.Object, interestService.Object);
            var result = controller.GetPersonList();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string actual = serializer.Serialize(result.Data);

            Assert.AreEqual(@"[{""Id"":1,""FirstName"":""Joe"",""LastName"":""Green"",""Age"":33,""PhotoUrl"":""image1.jpg"",""Interests"":[]},{""Id"":2,""FirstName"":""Robert"",""LastName"":""Harris"",""Age"":21,""PhotoUrl"":""image2.jpg"",""Interests"":[]},{""Id"":3,""FirstName"":""Mary"",""LastName"":""Jones"",""Age"":25,""PhotoUrl"":""image3.jpg"",""Interests"":[]}]".Trim(), actual.Trim());
        }

        [TestMethod]
        public void TestGetAllInterests()
        {
            var personService = new Mock<IPersonService>();
            var interestService = new Mock<IInterestService>();

            var interests = new List<Interest>
            {
                new Interest { Id = 1, Description = "Basketball"},
                new Interest { Id = 2, Description = "Skiing"},
                new Interest { Id = 3, Description = "Fitness"}
            }.AsQueryable();

            interestService.Setup(i => i.GetAll()).Returns(interests);

            var controller = new PeopleSearchController(personService.Object, interestService.Object);
            var result = controller.GetAllInterests();

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string actual = serializer.Serialize(result.Data);

            Assert.AreEqual(@"[{""Id"":1,""Description"":""Basketball""},{""Id"":2,""Description"":""Skiing""},{""Id"":3,""Description"":""Fitness""}]".Trim(), actual.Trim());
        }

        [TestMethod]
        public void TestAddPerson()
        {
            var personService = new Mock<IPersonService>();
            var interestService = new Mock<IInterestService>();
            var file = new Mock<HttpPostedFileBase>();

            var context = new Mock<HttpContextBase>();
            var server = new Mock<HttpServerUtilityBase>();

            var interest = new Interest { Id = 1, Description = "Cooking" };
            var person = new Person
            {
                FirstName = "Greg",
                LastName = "Richards",
                Age = 41, 
                PhotoUrl = "image.jpg",
                Interests = { interest }
            };
            interestService.Setup(i => i.Get(1)).Returns(interest);
            server.Setup(s => s.MapPath(It.IsAny<string>())).Returns("test-path");
            context.SetupGet(c => c.Server).Returns(server.Object);

            var controller = new PeopleSearchController(personService.Object, interestService.Object);
            controller.ControllerContext = new ControllerContext(context.Object,
                                     new RouteData(), controller);
            var status = controller.AddPerson(person, @"[{""Id"":1,""Description"":""Basketball""}]",  file.Object);

            Assert.IsTrue(status.StatusCode == HttpStatusCode.Created);
            personService.Verify(p => p.Add(It.IsAny<Person>()), Times.Once);
        }
    }
}

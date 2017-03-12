using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using People.Model.Config;
using People.Model.Db;
using People.Model.Service;

namespace PeopleTest.Service
{
    [TestClass]
    public class PersonServiceTest
    {
        [TestMethod]
        public void TestGetAll_Person()
        {
            var mockSet = new Mock<DbSet<Person>>();
            var mockContext = new Mock<PersonDbContext>();

            var data = new List<Person>
            {
                new Person
                {
                    FirstName = "Joe", LastName = "Green", Age = 33, PhotoUrl = "image1.jpg"},
                new Person {
                    FirstName = "Robert", LastName = "Harris", Age = 21, PhotoUrl = "image2.jpg" },
                new Person {
                    FirstName = "Mary", LastName = "Jones", Age = 25, PhotoUrl = "image3.jpg" }
            }.AsQueryable();
           
            mockContext.Setup(c => c.Set<Person>()).Returns(GetDbSetMock(data).Object);

            var service = new PersonService(mockContext.Object);
            var people = service.GetAll();

            Assert.AreEqual(3, people.Count());
        }

        private static Mock<DbSet<T>> GetDbSetMock<T>(IEnumerable<T> items = null) where T : class
        {
            if (items == null)
            {
                items = new T[0];
            }

            var dbSetMock = new Mock<DbSet<T>>();
            var q = dbSetMock.As<IQueryable<T>>();

            q.Setup(x => x.GetEnumerator()).Returns(items.GetEnumerator);

            return dbSetMock;

        }

    }

}


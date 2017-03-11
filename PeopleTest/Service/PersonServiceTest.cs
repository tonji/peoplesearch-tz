using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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

        //[TestMethod]
        //public void TestSavePerson()
        //{
        //    var mockSet = new Mock<DbSet<Person>>();
        //    var mockSet2 = new Mock<DbSet<Interest>>();
        //    var data = new List<Interest>
        //    {
        //        new Interest
        //        {Id = 1, Description = "Basketball"},
        //        new Interest
        //        {Id = 2, Description = "Skiing"},
        //        new Interest
        //        {Id = 3, Description = "Painting"},
        //        new Interest
        //        {Id = 4, Description = "Fitness"},
        //        new Interest
        //        {Id = 5, Description = "Cooking"}
        //    }.AsQueryable();

            //    mockSet2.As<IQueryable<Interest>>().Setup(m => m.Provider).Returns(data.Provider);
            //    mockSet2.As<IQueryable<Interest>>().Setup(m => m.Expression).Returns(data.Expression);
            //    mockSet2.As<IQueryable<Interest>>().Setup(m => m.ElementType).Returns(data.ElementType);
            //    mockSet2.As<IQueryable<Interest>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            //var mockContext = new Mock<PersonDbContext>();
            //    mockContext.Setup(c => c.Interests).Returns(mockSet2.Object);
            //    mockContext.Setup(m => m.Persons).Returns(mockSet.Object);

            //    var service = new PersonRepository(mockContext.Object);
            //    service.SavePerson("Robert","Harris",21, new HashSet<int>() {1, 2}, "image2.jpg");

            //    mockSet.Verify(m => m.Add(It.IsAny<Person>()), Times.Once());
            //    mockContext.Verify(m => m.SaveChanges(), Times.Once());
            //}

        }

}


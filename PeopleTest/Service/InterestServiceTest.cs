using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using People.Model.Config;
using People.Model.Db;
using People.Model.Service;

namespace PeopleTest.Service
{
    [TestClass]
    public class InterestServiceTest
    {
        [TestMethod]
        public void TestGetAll_Interest()
        {
            var mockSet = new Mock<DbSet<Interest>>();
            var mockContext = new Mock<PersonDbContext>();

            var data = new List<Interest>
            {
                new Interest { Description = "Basketball"},
                new Interest { Description = "Skiing"},
                new Interest {  Description = "Fitness"}
            }.AsQueryable();

            mockContext.Setup(c => c.Set<Interest>()).Returns(GetDbSetMock(data).Object);

            var service = new InterestService(mockContext.Object);
            var interests = service.GetAll();

            Assert.AreEqual(3, interests.Count());
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

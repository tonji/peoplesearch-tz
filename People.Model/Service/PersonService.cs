using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using People.Model.Config;
using People.Model.Db;

namespace People.Model.Service
{
    public class PersonService : Repository<Person>, IPersonService
    { 
        public PersonService(PersonDbContext context) : base(context){}
    

        //}
        
        //public IList<Person> FindPeople(string search)
        //{
        //    using (DbContext)
        //    {
        //       return DbContext.Persons.Where(p => p.FirstName.StartsWith(search) || p.LastName.StartsWith(search))
        //            .ToListAsync().Result;
        //    }
        //}
    }
}

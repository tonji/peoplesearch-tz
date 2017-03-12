using People.Model.Config;
using People.Model.Db;

namespace People.Model.Service
{
    public class PersonService : Repository<Person>, IPersonService
    { 
        public PersonService(PersonDbContext context) : base(context){}
   
    }
}

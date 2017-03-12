using People.Model.Config;
using People.Model.Db;

namespace People.Model.Service
{
    public class InterestService : Repository<Interest>, IInterestService
    {
        public InterestService(PersonDbContext context) : base(context) { }
    }
}

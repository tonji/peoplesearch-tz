using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using People.Model.Db;

namespace People.Model.Config
{
    public class PersonDbContext : DbContext
    {
        public PersonDbContext() : base("PersonDbContext")
        {
            Database.SetInitializer<PersonDbContext>(new DatabaseInitializer());
            if (Database.CreateIfNotExists())
            {
                Database.Initialize(true);
            }
        }

        public virtual DbSet<Person> Persons { get; set; }
        public virtual DbSet<Interest> Interests { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
        
    }

    public class DatabaseInitializer : DropCreateDatabaseAlways<PersonDbContext>
    {
        protected override void Seed(PersonDbContext context)
        {
            var interest1 = new Interest {Description = "Fitness"};
            var interest2 = new Interest { Description = "Cooking" };
            var interest3 = new Interest { Description = "Sports" };
            var interest4 = new Interest { Description = "Music" };
            var interest5 = new Interest {Description = "Technology"};
            var interest6 = new Interest {Description = "Fashion"};
            var interest7 = new Interest {Description = "Education"};

            context.Interests.Add(interest1);
            context.Interests.Add(interest2);
            context.Interests.Add(interest3);
            context.Interests.Add(interest4);
            context.Interests.Add(interest5);
            context.Interests.Add(interest6);
            context.Interests.Add(interest7);

            context.Persons.Add(new Person { Age = 30, FirstName = "Candance", LastName = "Winters", Interests = new List<Interest> { interest1, interest2, interest6 }, PhotoUrl = "profile1.jpeg" });
            context.Persons.Add(new Person { Age = 23, FirstName = "Devin", LastName = "Washington", Interests = new List<Interest> { interest4, interest1 }, PhotoUrl = "profile11.jpeg" });
            context.Persons.Add(new Person { Age = 21, FirstName = "Stephanie", LastName = "Patterson", Interests = new List<Interest> { interest3 }, PhotoUrl = "profile6.jpeg" });
            context.Persons.Add(new Person { Age = 18, FirstName = "Zoya", LastName = "Singh", Interests = new List<Interest> { interest4, interest2, interest7 }, PhotoUrl = "profile10.jpg" });
            context.Persons.Add(new Person { Age = 42, FirstName = "Robert", LastName = "Mitchell", Interests = new List<Interest> { interest1, interest5, interest4 }, PhotoUrl = "profile3.jpeg" });

            base.Seed(context);
        }
    }
}

using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using People.Model.Config;
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
            var interest1 = new Interest {Description = "Cars"};
            var interest2 = new Interest { Description = "Cooking" };
            var interest3 = new Interest { Description = "Sports" };
            var interest4 = new Interest { Description = "Music" };
            context.Interests.Add(interest1);
            context.Interests.Add(interest2);
            context.Interests.Add(interest3);
            context.Interests.Add(interest4);;

            context.Interests.Add(new Interest { Description = "Technology" });
            context.Interests.Add(new Interest { Description = "Fashion" });
            context.Interests.Add(new Interest { Description = "Education" });

            context.Persons.Add(new Person { Age = 23, FirstName = "Kevin", LastName = "Jones", Interests = new List<Interest> { interest4, interest2 }, PhotoUrl = "http://lorempixel.com/250/140/people" });
            context.Persons.Add(new Person { Age = 35, FirstName = "Stephanie", LastName = "Patterson", Interests = new List<Interest> { interest3 }, PhotoUrl = "http://lorempixel.com/250/140/people" });
            context.Persons.Add(new Person { Age = 18, FirstName = "Robert", LastName = "Mitchell", Interests = new List<Interest> { interest1, interest2, interest4 }, PhotoUrl = "http://lorempixel.com/250/140/people" });
            base.Seed(context);
        }
    }
}

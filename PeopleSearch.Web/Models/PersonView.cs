using System.Collections.Generic;

namespace PeopleSearch.Web.Models
{
    public class PersonView
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string PhotoUrl { get; set; }
        public ICollection<string> Interests { get; set; }

    }
}
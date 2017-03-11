using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace People.Model.Db
{
    public class Person
    {
        public Person()
        {
            this.Interests = new HashSet<Interest>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string PhotoUrl { get; set; }
        public virtual ICollection<Interest> Interests { get; set; }
    }
}

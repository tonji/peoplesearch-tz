using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace People.Model.Db
{
    public class Interest
    {
        public Interest()
        {
            this.Persons = new HashSet<Person>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Person> Persons { get; set; }
    }
}

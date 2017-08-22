using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Model
{
    [Table("EXHIBITION")]
    public class Exhibition : UserDefinable
    {
        [Required]
        public virtual string Name { get; set; }
        public virtual string Address { get; set; }
        public virtual string City { get; set; }
        public virtual string Zipcode { get; set; }
        public virtual string Gps { get; set; }
        [Required]
        public virtual DateTime Start { get; set; }
        [Required]
        public virtual DateTime End { get; set; }
        public virtual string Description { get; set; }

        public virtual ICollection<Painting> Paintings { get; set; }
    }
}

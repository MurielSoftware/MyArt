using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Model
{
    [Table("COLLECTION")]
    public class Collection : UserDefinable
    {
        [Required]
        public virtual string Name { get; set; }

        public virtual ICollection<Painting> Paintings { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}

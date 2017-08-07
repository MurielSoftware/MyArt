using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Model
{
    [Table("RESOURCE")]
    public class Resource : BaseEntity
    {
        [Required]
        public virtual string Name { get; set; }

        [Required]
        public virtual string Path { get; set; }

        public virtual Guid UserDefinableId { get; set; }

        [ForeignKey("UserDefinableId")]
        public virtual UserDefinable UserDefinable { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}

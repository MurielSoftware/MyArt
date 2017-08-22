using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Model
{
    [Table("USER_DEFINABLE")]
    public abstract class UserDefinable : BaseEntity
    {
        public virtual bool Public { get; set; }
        public virtual Guid? UserCreatorId { get; set; }

        public virtual ICollection<Gallery> Galleries { get; set; }
        public virtual ICollection<Resource> Resources { get; set; }
    }
}

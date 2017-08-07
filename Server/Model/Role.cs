using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Model
{
    [Table("ROLE")]
    public class Role : BaseEntity
    {
        [Required]
        public virtual string Name { get; set; }

        public virtual bool DojoCreation { get; set; }
        public virtual bool UserCreation { get; set; }
        public virtual bool RoleCreation { get; set; }
        public virtual bool MenuCreation { get; set; }
        public virtual bool CreateUpdateDeleteAll { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}

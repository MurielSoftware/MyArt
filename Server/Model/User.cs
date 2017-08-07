using Shared.Core.Attributes;
using Shared.Core.Context;
using Shared.Dtos.Users;
using Shared.I18n.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Model
{
    [Table("USER")]
    public class User : UserDefinable
    {
        [Required]
        public virtual string FirstName { get; set; }

        [Required]
        public virtual string Surname { get; set; }

        [Required]
        public virtual string Email { get; set; }

        [Required]
        public virtual string Password { get; set; }

        public virtual string Description { get; set; }

        public virtual Guid RoleId { get; set; }

        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }

        //public virtual ICollection<BaseEvent> Events { get; set; }
        //public virtual ICollection<Training> Trainings { get; set; }

        public override string ToString()
        {
            return string.Format(@"{0} {1}", FirstName, Surname);
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Model
{
    [Table("GALLERY")]
    public abstract class Gallery : UserDefinable
    {
        public virtual string Name { get; set; }
        public virtual Guid? CoverPhotoId { get; set; }
        public virtual Guid? OwnerId { get; set; }

        [ForeignKey("CoverPhotoId")]
        public virtual Resource CoverPhoto { get; set; }

        [ForeignKey("OwnerId")]
        public virtual UserDefinable Owner { get; set; }
    }
}

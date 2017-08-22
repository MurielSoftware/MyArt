using Shared.Dtos.Paintings;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Model
{
    [Table("PAINTING")]
    public class Painting : UserDefinable
    {
        [Required]
        public virtual string Title { get; set; }
        [Required]
        public virtual DateTime Date { get; set; }
        [Required]
        public virtual Surface Surface { get; set; }
        [Required]
        public virtual Technique Technique { get; set; }
        [Required]
        public virtual float Width { get; set; }
        [Required]
        public virtual float Height { get; set; }
        public virtual string Description { get; set; }
        public virtual string Info { get; set; }
        public virtual Guid? CollectionId { get; set; }
        public virtual string Owner { get; set; }

        [ForeignKey("CollectionId")]
        public virtual Collection Collection { get; set; }

        public virtual ICollection<Exhibition> Exhibitions { get; set; }
        public virtual ICollection<User> Users { get; set; }

        public override string ToString()
        {
            return Title;
        }
    }
}

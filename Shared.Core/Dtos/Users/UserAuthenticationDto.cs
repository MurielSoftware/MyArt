using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Core.Dtos.Users
{
    public class UserAuthenticationDto : BaseDto
    {
        public string FirstName { get; set; }
        public string Surname { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public override string ToString()
        {
            return FirstName + " " + Surname;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.CrossCuttingConcerns.Enums;

namespace Utils.Application.Dto.User
{
    public class InsertUserRequestDto
    {
        public string Name { get; set; }

        public Sex Sex { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }
    }
}

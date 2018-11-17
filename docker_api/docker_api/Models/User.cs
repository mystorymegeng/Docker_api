using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace docker_api.Models
{
    public class User
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }

    }
}

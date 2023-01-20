using System;
using System.Collections.Generic;
using System.Text;

namespace IntTrackerCrossPlatformMobile
{
    class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }

        public string DefaultProject { get; set; }

        public string Email { get; set; }

        public string Department { get; set; }

        public string Position { get; set; }
        public int Active { get; set; }
        public string DeletedUser { get; set; }
    }
}

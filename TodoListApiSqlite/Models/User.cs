using System;
using System.Collections.Generic;

namespace TodoListApiSqlite.Models
{
    public partial class User
    {
        public User()
        {
            Groups = new HashSet<GroupUser>();
        }

        public int Id { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Name { get; set; } = null!;

        public virtual ICollection<GroupUser> Groups { get; set; }
    }
}

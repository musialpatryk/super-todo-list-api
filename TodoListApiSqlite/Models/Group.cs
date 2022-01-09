﻿using System;
using System.Collections.Generic;

namespace TodoListApiSqlite.Models
{
    public partial class Group
    {
        public Group()
        {
            Notes = new HashSet<Note>();
            Users = new HashSet<GroupUser>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Note> Notes { get; set; }
        public virtual ICollection<GroupUser> Users { get; set; }
    }
}

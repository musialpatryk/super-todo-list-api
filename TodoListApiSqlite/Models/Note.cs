using System;
using System.Collections.Generic;

namespace TodoListApiSqlite.Models
{
    public partial class Note
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public int? Priority { get; set; }
        public int GroupId { get; set; }

        public virtual Group Group { get; set; } = null!;
    }
}

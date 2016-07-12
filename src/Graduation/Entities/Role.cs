using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Graduation.Entities
{
    public class Role : IEntityBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsDelete { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsSystem { get; set; }

        
        public Role()
        {
            IsDelete = false;
            DateCreated = System.DateTime.UtcNow;
            IsSystem = false;
        }
    }

}

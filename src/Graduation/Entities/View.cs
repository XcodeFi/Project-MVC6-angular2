using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Graduation.Entities
{
    public class View:IEntityBase
    {
        public int Id { get; set; }
        public int Key { get; set; }
        public DateTime TimeView { get; set; }
        public int TotalViews { get; set; }
        //nguoi da truy cap
        public int TotalViewings { get; set; }
        public int TotalViewIsMember { get; set; }

        public View () {}
    }
}

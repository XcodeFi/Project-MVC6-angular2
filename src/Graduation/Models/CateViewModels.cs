using Graduation.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Graduation.Models
{
    public class CateViewModel
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public byte Level { get; set; }
        public string Name { get; set; }
        public string UrlSlug { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public ICollection<CateViewModel> CateChilds { get; set; }
        public CateViewModel()
        {
            CateChilds = new HashSet<CateViewModel>();
        }
    }
}

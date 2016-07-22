using Graduation.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Graduation.Models
{
    public class CateViewModel
    {
        public int Id { get; set; }
        public string Icon { get; set; }
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
    public class CateCreateViewModel
    {
        [Required]
        [StringLength(450)]
        public string Name { get; set; }
        [StringLength(100)]
        public string Icon { get; set; }
        public int? ParentId { get; set; }
        [Required]
        public byte Level { get; set; }
        //[Required]
        //[StringLength(450)]
        //public string UrlSlug { get; set; }
        [Required]
        [StringLength(250)]
        public string ImageUrl { get; set; }
        [Required]
        [StringLength(300)]
        public string Description { get; set; }
        [Required]
        public bool IsPublished { get; set; }
        [Required]
        public bool IsMainMenu { get; set; }

    }

    public class CateChartVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public int TotalCards { get; set; }
    }

}

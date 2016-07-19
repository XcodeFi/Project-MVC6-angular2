using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Graduation.Areas.Admin.Models
{
    public class CateViewModels
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
}

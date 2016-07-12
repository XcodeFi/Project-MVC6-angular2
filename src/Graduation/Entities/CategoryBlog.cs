using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Graduation.Entities
{
    public class CategoryBlog : IEntityBase
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(250)]
        public string Title { get; set; }
        [Required]
        [StringLength(500)]
        public string Description { get; set; }
        [Required]
        public DateTime DateCreated { get; set;}
        [Required]
        public DateTime DateEdited { get; set; }
        [Required]
        public bool Status { get; set; }
        public virtual ICollection<Blog> Blogs { get; set; }
        public CategoryBlog()
        {
            DateEdited = System.DateTime.UtcNow;
            Status = false;
            Blogs = new HashSet<Blog>();
        }
    }
}

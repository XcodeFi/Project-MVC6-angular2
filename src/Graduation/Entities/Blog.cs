using Graduation.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Graduation.Entities
{
    public class Blog:IEntityBase
    {
        [Key]
        public int Id { get; set; }
        public int CateBlogId { get; set; }
        [Required]
        [StringLength(250)]
        public string Title { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        [StringLength(250)]
        public string UrlSlug { get; set; }
        [Required]
        [StringLength(300)]
        public string Summary { get; set; }
        [Required]
        public string Content { get; set; }
        [StringLength(300)]
        public string ImageUrl { get; set; }
        [Required]
        public int ViewNo { get; set; }
        [Required]
        public bool IsDeleted { get; set; }
        [Required]
        public bool IsLocked { get; set; }
        [Required]
        public bool Status { get; set; }
        [Required]
        public DateTime DatePosted { get; set; }
        [Required]
        public int LikeNo { get; set; }
        public DateTime DateEdited { get; set; }
        public string TextSearch { get; set; }
        [ForeignKey("CateBlogId")]
        public virtual CategoryBlog CategoryBlog { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
        public string AppicationUserId { get; set; }

        [ForeignKey("AppicationUserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }

        public Blog()
        {
            ViewNo = 0;
            IsDeleted = false;
            IsLocked = false;
            Status = false;
            LikeNo = 0;
            DateEdited = System.DateTime.UtcNow;

            Comments = new List<Comment>();
        }
    }
}

using Graduation.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Graduation.Entities
{
    public class Comment : IEntityBase
    {
        [Key]
        public int Id { get; set; }
        public int? BlogId { get; set; }

        public string UserId { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public byte IsDeleted { get; set; }
        [Required]
        public int? LikeNo { get; set; }
        [Required]
        public bool Status { get; set; }
        public int? CardId { get; set; }
        [Required]
        public DateTime DatePosted { get; set; }
        [ForeignKey("BlogId")]
        public virtual Blog Blog { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }
        public Comment()
        {
            IsDeleted = 0;
            Status = false;
            DatePosted = DateTime.UtcNow;
        }
    }
}

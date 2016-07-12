using Graduation.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Graduation.Entities
{
    /// <summary>
    /// Property require
    /// CateID,
    /// Content,
    /// ImageUrl,
    /// DateCreate,
    /// Ispublish,
    /// UserId
    /// </summary>
    public class Card : IEntityBase
    {
        [Key]
        public int Id { get; set; }//id
        [Required]
        public int CateId { get; set; }//re
        [Required]
        [ForeignKey("CateId")]
        public virtual Category Category { get; set; }
        public string Title { get; set; }//re
        [StringLength(450)]
        public string UrlSlug { get; set; }
        [Required]
        [StringLength(500)]
        public string Content { get; set; }//re
        public int ViewNo { get; set; }
        public int LikesNo { get; set; }
        [StringLength(50)]
        public string CardSize { get; set; }
        [StringLength(50)]
        public string CardType { get; set; }
        public bool IsDeleted { get; set; }
        [Required]
        public string ImageUrl { get; set; }//re
        public byte? RateNo { get; set; }
        [Required]
        public DateTime DateCreated { get; set; }//re
        public DateTime? DateEdited { get; set; }
        [Required]
        public bool IsPublished { get; set; }//re
        [StringLength(250)]
        public string TextSearch { get; set; }
        [Required]
        public string ApplycationUserId { get; set; }//re
        [ForeignKey("ApplycationUserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }
        public Card()
        {
            IsPublished = true;
            CardType = "Image";
            ViewNo = 0;
            LikesNo = 0;
            IsDeleted = false;
            RateNo = 3;
            DateCreated = System.DateTime.UtcNow;
            CardSize = "100*100";
        }
    }
}
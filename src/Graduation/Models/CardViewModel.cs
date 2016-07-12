using System;
using System.ComponentModel.DataAnnotations;
namespace Graduation.Models
{

    public class CardViewModel
    {
        public int Id { get; set; }
        public int CateId { get; set; }
        public string Title { get; set; }
        public string UrlSlug { get; set; }
        public string Content { get; set; }
        public int ViewNo { get; set; }
        public int LikesNo { get; set; }
        public string ImageUrl { get; set; }
        public byte? RateNo { get; set; }
        public DateTime DateCreated { get; set; }
        public string[] Tag { get; set; }//text search
        public string ApplycationUserId { get; set; }
    }

    public class CardCreateViewModel
    {
        [Required]
        public int CateId { get; set; }//re
        [Required]
        public string Title { get; set; }//re
        [Required]
        [StringLength(500)]
        public string Content { get; set; }//re
        [StringLength(50)]
        public string CardSize { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        public bool IsPublished { get; set; }
        [StringLength(250)]
        public string TextSearch { get; set; }
        [Required]
        public string ApplycationUserId { get; set; }//re
    }
}

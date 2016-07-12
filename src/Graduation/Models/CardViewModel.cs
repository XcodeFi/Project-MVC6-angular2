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
}

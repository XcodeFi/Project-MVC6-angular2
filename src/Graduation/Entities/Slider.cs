using System.ComponentModel.DataAnnotations;

namespace Graduation.Entities
{
    public class Slider:IEntityBase
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Display(Name ="Image")]
        public string ImageUrl { get; set; }
        [Display(Name = "Seo link")]
        [Required]
        public string UrlSlug { get; set; }

    }
}

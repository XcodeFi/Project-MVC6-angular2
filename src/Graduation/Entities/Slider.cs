using System.ComponentModel.DataAnnotations;

namespace Graduation.Entities
{
    public class Slider:IEntityBase
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string UrlSlug { get; set; }

    }
}

using System;
using System.ComponentModel.DataAnnotations;

namespace Graduation.Entities
{
    public class Contact : IEntityBase
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        [StringLength(100)]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}",
            ErrorMessage = "Email is not valid.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        [StringLength(20)]
        public string Phone { get; set; }
        [Required]
        [StringLength(200)]
        public string Address { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime SendedTime { get; set; }
        [Required]
        public bool IsDeleted { get; set; }
        [Required]
        public bool Status { get; set; }

        public Contact()
        {
            SendedTime = System.DateTime.UtcNow;
            IsDeleted = false;
            Status = false;
        }
    }

}

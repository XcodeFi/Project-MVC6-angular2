using System;
using System.ComponentModel.DataAnnotations;

namespace Graduation.Entities
{
    public class Error : IEntityBase
    {
        [Key]
        public int Id { get; set; }
        public string Message { get; set; }
        public DateTime DateCreated { get; set; }
        public string StackTrace { get; set; }
    }
}

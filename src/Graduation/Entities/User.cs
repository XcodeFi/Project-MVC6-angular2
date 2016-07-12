using System;
using System.Collections.Generic;

namespace Graduation.Entities
{
    public class User : IEntityBase
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public string Email { get; set; }
        public bool? IsOnline { get; set; }
        public bool IsLocked { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActived { get; set; }
        public string AvatarUrl { get; set; }
        public DateTime? LastLogout { get; set; }
        public DateTime? LastLogin { get; set; }
        public int? LoginFailureCount { get; set; }
        public DateTime? FirstLoginFalureDate { get; set; }
        public DateTime? Birthday { get; set; }
        public DateTime DateCreated { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }
        public User()
        {
            IsLocked = false;
            IsDeleted = false;
            IsActived = false;

            UserRoles = new List<UserRole>();
        }
    }
}

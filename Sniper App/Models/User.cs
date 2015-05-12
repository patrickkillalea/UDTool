using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sniper_App.Models
{
    public enum UserType
    {
        General,
        Administrator
    }

    public class User
    {
        [Key]
        public string LanID { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public int UserType { get; set; }
        [DefaultValue(true)]
        public bool Active { get; set; }
        public DateTime TimeStamp { get; set; }

        public ICollection<UserGroup> UserGroups { get; set; }
        public ICollection<Permission> Permissions { get; set; }
    }
}
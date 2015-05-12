using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sniper_App.Models
{
    public class Group
    {
        [Key]
        public int GroupID { get; set; }
        [Required]
        public string GroupName { get; set; }
        public bool Active { get; set; }
        public DateTime TimeStamp { get; set; }

        public ICollection<Permission> Permissions { get; set; }
        public ICollection<UserGroup> UserGroups { get; set; }
    }
}
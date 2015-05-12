using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sniper_App.Models
{
    public class Permission
    {
        [Key]
        public int PermissionID { get; set; }
        public string LanID { get; set; }
        public int? GroupID { get; set; }
        public int? AppEnvironmentID { get; set; }
        public int? FixID { get; set; }
        public bool Active { get; set; }
        public DateTime TimeStamp { get; set; }

        public virtual User User { get; set; }
        public virtual Group Group { get; set; }
        public virtual AppEnvironment AppEnvironment { get; set; }
        public virtual Fix Fix { get; set; }
    }
}
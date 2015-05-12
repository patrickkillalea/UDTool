using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sniper_App.Models
{
    public class Audit
    {
        [Key]
        public int AuditID { get; set; }
        [Required]
        public string LanID { get; set; }
        public int? FixID { get; set; }
        public int ApplicationID { get; set; }
        public int AppEnvironmentID { get; set; }
        public string Event { get; set; }
        public string KeyValue { get; set; }
        public string ValueStart { get; set; }
        public string ValueEnd { get; set; }
        public DateTime TimeStamp { get; set; }

        public virtual User User { get; set; }
        public virtual Fix Fix { get; set; }
        public virtual Application Application { get; set; }
        public virtual AppEnvironment AppEnvironment { get; set; }
    }
    public class AuditUsers
    {
        public string LanID { get; set; }
        public string count { get; set; } 
    }
    public class AuditEvents
    {
        public string eventName { get; set; }
        public string count { get; set; }

    }
    public class AuditDates
    {
        public string date { get; set; }
        public string count { get; set; }
    }

}
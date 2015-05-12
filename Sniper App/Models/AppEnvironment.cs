using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Sniper_App.Models
{
    public class AppEnvironment
    {
        public AppEnvironment()
        {
            this.AppEnvironmentDatabases = new HashSet<AppEnvironmentDatabase>();
        }

        [Key]
        public int AppEnvironmentID { get; set; }
        public int ApplicationID { get; set; }
        [Required]
        public string Name { get; set; }
        public bool Active { get; set; }
        public DateTime TimeStamp { get; set; }

        public virtual ICollection<AppEnvironmentDatabase> AppEnvironmentDatabases { get; set; }
    }
}
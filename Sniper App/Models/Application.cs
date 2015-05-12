using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Sniper_App.Models
{
    public class Application
    {
        public Application()
        {
            this.AppEnvironments = new HashSet<AppEnvironment>();
        }

        [Key]
        public int ApplicationID { get; set; }
        public int? RemoteStoredProcedureID { get; set; }
        [Required]
        public string Name { get; set; }
        public bool Active { get; set; }
        public DateTime TimeStamp { get; set; }

        public virtual RemoteStoredProcedure RemoteStoredProcedure { get; set; }
        public virtual ICollection<AppEnvironment> AppEnvironments { get; set; }
    }
}
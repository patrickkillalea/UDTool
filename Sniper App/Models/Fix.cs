using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Sniper_App.Models
{
    public class Fix
    {
        [Key]
        public int FixID { get; set; }
        [ForeignKey("RelatedFixes")]
        public int? ParentFixID { get; set; }
        public int AppEnvironmentID { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string HtmlCode { get; set; }
        [ForeignKey("SelectProcedure")]
        public int SelectProcedureID { get; set; }
        [ForeignKey("UpdateProcedure")]
        public int UpdateProcedureID { get; set; }
        public bool Active { get; set; }
        public DateTime TimeStamp { get; set; }

        public virtual ICollection<Fix> RelatedFixes { get; set; }
        public virtual AppEnvironment AppEnvironment { get; set; }
        public virtual RemoteStoredProcedure SelectProcedure { get; set; }
        public virtual RemoteStoredProcedure UpdateProcedure { get; set; }
    }
}
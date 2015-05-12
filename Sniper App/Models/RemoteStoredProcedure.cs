using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sniper_App.Models
{
    public class RemoteStoredProcedure
    {
        [Key]
        public int RemoteStoredProcedureID { get; set; }
        public int DatabaseID { get; set; }
        [Required]
        public string Name { get; set; }
        public bool Active { get; set; }
        public DateTime TimeStamp { get; set; }

        public virtual Database Database { get; set; }
        public virtual ICollection<Column> Columns { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Sniper_App.Models
{
    public class UserGroup
    {
        [Key]
        [Column(Order = 0)]
        public string LanID { get; set; }
        [Key]
        [Column(Order = 1)]
        public int GroupID { get; set; }

        public virtual User User { get; set; }
        public virtual Group Group { get; set; }
    }
}
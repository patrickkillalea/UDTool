using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Sniper_App.Models
{
    public class AppEnvironmentDatabase
    {
        [Key]
        [Column(Order = 0)]
        public int AppEnvironmentID { get; set; }
        [Key]
        [Column(Order = 1)]
        public int DatabaseID { get; set; }
    }
}
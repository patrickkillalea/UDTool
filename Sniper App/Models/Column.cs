using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Sniper_App.Models
{
    public enum DataType
    {
        Integer,
        NVarCharMax,
        DateTime,
        Bit
    }

    public class Column
    {
        [Key]
        public int ColumnID { get; set; }
        public int RemoteStoredProcedureID { get; set; }
        [Required]
        public string ColumnName { get; set; }
        [Required]
        public string ColumnDisplayName { get; set; }
        public int DataType { get; set; }
        public bool Active { get; set; }
        public DateTime TimeStamp { get; set; }

        public virtual RemoteStoredProcedure RemoteStoredProcedure { get; set; }
    }
}
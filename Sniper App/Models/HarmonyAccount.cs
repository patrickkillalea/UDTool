using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Sniper_App.Models
{
    public class HarmonyAccount
    {
        [Key]
        //   public int RowId { get; set; }
        public string AccountName { get; set; }
        public string HarmonyAccountNumber { get; set; }
        public string AccountId { get; set; }

    }


    public class Bcn
    {
        [Key]
        public string BcnNumber { get; set; }
        public string describition { get; set; }
        public string bcnId { get; set; }
        public string fNumber { get; set; }

    }

}
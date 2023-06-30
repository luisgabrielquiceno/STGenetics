using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace STGenetics.Models
{
    public class RespOrder
    {
        public int OrderPurchaseId { get; set; }
        public System.DateTime Date { get; set; }
        public int Discount { get; set; }
        public int Totalquantity { get; set; }
        public double Totalprice { get; set; }
        public double Freightcharge { get; set; }
    }
}
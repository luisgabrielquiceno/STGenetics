using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace STGenetics.Models
{
    public class ReqOrderDetail
    {
        public int OrderPurchaseId { get; set; }
        public int Animalid { get; set; }
        public int Quantity { get; set; }
    }
}
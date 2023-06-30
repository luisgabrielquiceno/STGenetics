using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace STGenetics.Models
{
    public class ReqAnimalFilter
    {
        public int AnimalId { get; set; }        
        public int StatusId { get; set; }
        public int SexId { get; set; }
        public string Name { get; set; }        
    }
}
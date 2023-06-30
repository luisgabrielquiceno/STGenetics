using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace STGenetics.Models
{
    public class RespAnimal
    {
        public int AnimalId { get; set; }
        public string Breed { get; set; }
        public string Status { get; set; }
        public string Sex { get; set; }
        public string Name { get; set; }
        public System.DateTime Birthdate { get; set; }
        public double Price { get; set; }
    }
}
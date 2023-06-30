using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace STGenetics.Models
{
    public class ReqAnimalToUpdate
    {
        public int AnimalId { get; set; }
        public int BreedId { get; set; }
        public int StatusId { get; set; }
        public int SexId { get; set; }
        public string Name { get; set; }
        public System.DateTime Birthdate { get; set; }
        public double Price { get; set; }
    }
}
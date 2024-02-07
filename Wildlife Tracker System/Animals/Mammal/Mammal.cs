using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wildlife_Tracker_System.Animals.Mammal
{
    public class Mammal:Animal
    {
        public string GestationPeriod { get; set; }
        public Mammal() : base() 
        {
            Species.Add("Cat");
            Species.Add("Dog");
        }
    }
}

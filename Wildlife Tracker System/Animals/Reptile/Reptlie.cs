using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wildlife_Tracker_System.Animals.Reptile
{
    public class Reptlie:Animal
    {
        public string Size { get; set; }
        public Reptlie() : base()
        {
            Species.Add("Snake");
            Species.Add("Crocodile");
        }
    }
}

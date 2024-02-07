using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wildlife_Tracker_System.Animals
{
    public class Animal
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Age { get; set; }
        public string Gender { get; set; }
        public Boolean Domesticated { get; set; }
        public string Avatar { get; set; }
        public List<string> Species { get; set; }
        public Animal() 
        {
            Species = new List<string>();
        }
    }
}

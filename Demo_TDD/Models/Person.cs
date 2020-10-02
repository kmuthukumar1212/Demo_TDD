using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo_TDD.Models
{
    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public HumanRaces Race { get; set; }
        public double Height { get; set; }
    }

    public enum HumanRaces
    {
        Erectus,
        Sapiens,
        Neanderthalensis,
        Denisova
    }
}

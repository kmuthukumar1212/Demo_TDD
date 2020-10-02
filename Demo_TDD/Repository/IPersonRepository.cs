using Demo_TDD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo_TDD.Repository
{
    public interface IPersonRepository
    {
        List<Person> GetPersons();
    }

    public class PersonRepository : IPersonRepository
    {
        public List<Person> GetPersons()
        {
            List<Person> lstPersons = new List<Person>();
            try
            {
                var rd = new Random();
                for (int i = 0; i < 10000; i++)
                {
                    int age = rd.Next(1, 99);
                    var race = (HumanRaces)rd.Next(0, 4);
                    lstPersons.Add(new Person
                    {
                        Name = $"Person # {i}",
                        Age = age,
                        Race = race,
                        Height = CalculateHeight(race, age)
                    });
                }
            }
            catch
            {
                throw;
            }
            return lstPersons;
        }

        double CalculateHeight(HumanRaces humanRace, int age)
        {
            double height = 0.0;

            if (humanRace == HumanRaces.Erectus || humanRace == HumanRaces.Sapiens)
            {
                height = (1.5 + (age * 0.45));
            }
            else if (humanRace == HumanRaces.Neanderthalensis)
            {
                height = ((age * 1.6) / 2);
            }
            else if (humanRace == HumanRaces.Denisova)
            {
                height = (1.7 + ((age + 2) * 0.23));
            }

            return height;
        }
    }

    

}



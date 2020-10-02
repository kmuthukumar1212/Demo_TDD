using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Demo_TDD.Models;
using Demo_TDD.Repository;
using System.Runtime.Caching;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using Newtonsoft.Json;

namespace Demo_TDD.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPersonRepository personRepository;
        private IMemoryCache _cache;
        private List<Person> lstPersons = null;
        public HomeController(IPersonRepository PersonRepository, IMemoryCache memoryCache)
        {
            this.personRepository = PersonRepository;
            this._cache = memoryCache;
            if (!_cache.TryGetValue(AppConstants.PersonList_Key, out lstPersons))
            {
                LoadListInCache();
            }
        }

        public IActionResult Index()
        {   
            _cache.TryGetValue(AppConstants.PersonList_Key, out lstPersons);
            return View();
        }

        [HttpPost("increaseage")]
        public IActionResult IncreaseAgeByOne()
        {
            if(this.lstPersons != null){
                this.lstPersons.ForEach(delegate (Person person) {
                    person.Age += 1;
                });

                _cache.Set(AppConstants.PersonList_Key, this.lstPersons);
                return Ok(true);
            }

            return BadRequest();
        }

        [HttpGet("races")]
        public IActionResult GetHumanRacess()
        {
            Dictionary<int, string> dictRaces = new Dictionary<int, string>();
            foreach (HumanRaces foo in Enum.GetValues(typeof(HumanRaces)))
            {
                dictRaces.Add((int)foo, foo.ToString());
            }

            return Ok(JsonConvert.SerializeObject(dictRaces));
        }

        [HttpGet("person/{race}")]
        public IActionResult GetPersons(int race)
        {
            _cache.TryGetValue(AppConstants.PersonList_Key, out lstPersons);
            var result = lstPersons;
            if(lstPersons != null && lstPersons.Count > 0)
            {
                if (race > -1)
                    result = result.Where(c => c.Race == (HumanRaces)race).ToList();
                result = result.Where(c => ((c.Age % 2) == 0)).OrderBy(c => c.Age).ToList();
                return Ok(JsonConvert.SerializeObject(result));
            }
            return BadRequest();
            
        }

        [HttpGet("report")]
        public IActionResult GetReport()
        {
            _cache.TryGetValue(AppConstants.PersonList_Key, out lstPersons);
            if(lstPersons != null)
            {
                int NoOfPeoples = lstPersons.Count;
                double AverageAge = lstPersons.Select(a => a.Age).Average();
                double Minage = lstPersons.Select(a => a.Age).Min();
                double Maxage = lstPersons.Select(a => a.Age).Max();

                List<KeyValuePair<string, int>> races = new List<KeyValuePair<string, int>>();

                foreach (HumanRaces foo in Enum.GetValues(typeof(HumanRaces)))
                {
                    races.Add(new KeyValuePair<string, int>(foo.ToString(), lstPersons.Where(c => c.Race == foo).ToList().Count));
                }
                var Age = new { Average = AverageAge, MinAge = Minage, MaxAge = Maxage };


                var report = new { NoOfPeople = NoOfPeoples, AgeDetail = Age, RaceDetails = races };

                return Ok(JsonConvert.SerializeObject(report));
            }
            return BadRequest();
            
        }

        void LoadListInCache()
        {
            this.lstPersons = this.personRepository.GetPersons();
            _cache.Set(AppConstants.PersonList_Key, this.lstPersons);
        }

    }
}

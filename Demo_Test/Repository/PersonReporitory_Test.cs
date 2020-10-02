using Demo_TDD.Models;
using Demo_TDD.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo_Test.Repository
{
    [TestClass]
    public class PersonReporitory_Test
    {
        [TestMethod]
        public void GetPersons_Test()
        {
            IPersonRepository personRepository = new PersonRepository();

            var persons = personRepository.GetPersons() as List<Person>;

            Assert.IsNotNull(persons);
        }
    }
}

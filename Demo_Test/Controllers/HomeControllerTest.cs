using Demo_TDD.Controllers;
using Demo_TDD.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Caching.Memory;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace Demo_Test.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        IMemoryCache GetMemoryCache()
        {   
            var services = new ServiceCollection();
            services.AddMemoryCache();
            var serviceProvider = services.BuildServiceProvider();
            var memoryCache = serviceProvider.GetService<IMemoryCache>();
            return memoryCache;
        }

        [TestMethod]
        public void Index_Test()
        {
            var mockRepo = new Mock<IPersonRepository>();
            HomeController controller = new HomeController(mockRepo.Object, this.GetMemoryCache());

            ViewResult result = controller.Index() as ViewResult;

            Assert.IsNotNull(result);

        }

        [TestMethod]
        public void IncreaseAgeByOne_Test()
        {
            var mockRepo = new Mock<IPersonRepository>();
            HomeController controller = new HomeController(mockRepo.Object, this.GetMemoryCache());

            IActionResult result = controller.IncreaseAgeByOne();

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetHumanRacess_Test()
        {
            var mockRepo = new Mock<IPersonRepository>();
            HomeController controller = new HomeController(mockRepo.Object, this.GetMemoryCache());

            IActionResult result = controller.GetHumanRacess();

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetPersons_WithoutParam_Test()
        {
            var mockRepo = new Mock<IPersonRepository>();
            HomeController controller = new HomeController(mockRepo.Object, this.GetMemoryCache());

            IActionResult result = controller.GetPersons(-1);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetPersons_WithParam_Test()
        {
            var mockRepo = new Mock<IPersonRepository>();
            HomeController controller = new HomeController(mockRepo.Object, this.GetMemoryCache());

            IActionResult result = controller.GetPersons(2);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetReport_Test()
        {
            var mockRepo = new Mock<IPersonRepository>();
            HomeController controller = new HomeController(mockRepo.Object, this.GetMemoryCache());

            IActionResult result = controller.GetReport();
            Assert.IsNotNull(result);
        }
    }
}

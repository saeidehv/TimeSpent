using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel.Composition;
using System.Collections.Generic;
using TimeSpent.Core.Extensions;
using TimeSpent.Core;
using TimeSpent.Business.Entities;
using TimeSpent.Business.Bootstrapper;
using TimeSpent.Data.Contracts.RepositoryInterfaces;
using Moq;
using TimeSpent.Core.Contracts;

namespace TimeSpent.DataLayer.Test
{
    [TestClass]
    public class DataLayerTest
    {
        [TestInitialize]
        public void Initilize()
        {
            ObjectBase.Container = MEFLoader.Init();
        }

        [TestMethod]
        public void test_repository_usage()
        {
            RepositoryTestClass test = new RepositoryTestClass();
            IEnumerable<TimeEntry> entries = test.GetEntries();
            Assert.IsTrue(entries != null);
        }

        [TestMethod]
        public void test_repository_factory_usage()
        {
            RepositoryFactoryTestClass factory = new RepositoryFactoryTestClass();
            IEnumerable<TimeEntry> entries = factory.GetEntries();
            Assert.IsTrue(entries != null);
        }

        [TestMethod]
        public void test_repositpry_mocking()
        {
            List<TimeEntry> entries = new List<TimeEntry>()
            {
                new TimeEntry() { EntityId=1 ,Duration=2 } ,
                new TimeEntry() { EntityId=2 , Duration=3}
            };

            Mock<ITimeEntryRepository> mockRepository = new Mock<ITimeEntryRepository>();
            mockRepository.Setup(obj => obj.Get()).Returns(entries);

            RepositoryTestClass test = new RepositoryTestClass(mockRepository.Object);
            IEnumerable<TimeEntry> ret  = test.GetEntries();
            Assert.IsTrue(ret == entries);

         }

        [TestMethod]
        public void test_repository_factory_mocking()
        {
            List<TimeEntry> entries = new List<TimeEntry>()
            {
                new TimeEntry() { EntityId =1 , Duration=2 },
                new TimeEntry() {EntityId=2 , Duration=4}
            };

            Mock<IDataRepositoryFactory> mockRepository = new Mock<IDataRepositoryFactory>();
            mockRepository.Setup(obj => obj.GetRepository<ITimeEntryRepository>().Get()).Returns(entries);

            RepositoryFactoryTestClass test = new RepositoryFactoryTestClass(mockRepository.Object);
            IEnumerable<TimeEntry> ret = test.GetEntries();

            Assert.IsTrue(ret == entries);


        }

        [TestMethod]
        public void test_repository_factory_mocking2()
        {
            List<TimeEntry> entries = new List<TimeEntry>()
            {
                new TimeEntry() { EntityId = 1, Duration=2 },
                new TimeEntry() {EntityId=2 , Duration=4}
            };

            Mock<ITimeEntryRepository> mockEntryRepository = new Mock<ITimeEntryRepository>();
            mockEntryRepository.Setup(obj => obj.Get()).Returns(entries);


            Mock<IDataRepositoryFactory> mockFactory = new Mock<IDataRepositoryFactory>();
            mockFactory.Setup(obj => obj.GetRepository<ITimeEntryRepository>()).Returns(mockEntryRepository.Object);

            RepositoryFactoryTestClass test = new RepositoryFactoryTestClass(mockFactory.Object);
            IEnumerable<TimeEntry> ret = test.GetEntries();
            Assert.IsTrue(ret == entries);
        }
    }

    public class RepositoryTestClass
    {
        public RepositoryTestClass()
        {
            ObjectBase.Container.SatisfyImportsOnce(this);
        }

        public RepositoryTestClass(ITimeEntryRepository rep)
        {
            _repository = rep;
        }

        [Import]
        ITimeEntryRepository _repository;


        public IEnumerable<TimeEntry> GetEntries()
        {
             return _repository.Get();
        }


    }

    public class RepositoryFactoryTestClass
    {
        public RepositoryFactoryTestClass()
        {
            ObjectBase.Container.SatisfyImportsOnce(this);
        }

        public RepositoryFactoryTestClass(IDataRepositoryFactory factory)
        {
            _factory = factory;
        }

        [Import]
        IDataRepositoryFactory _factory;

        public IEnumerable<TimeEntry> GetEntries()
        {
            return _factory.GetRepository<ITimeEntryRepository>().Get();
        }

    }
}

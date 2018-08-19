using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.AutoMock;
using myApiTreeView.API.Data;
using myApiTreeView.API.Dtos;
using myApiTreeView.Models;
using myApiTreeView.Services;
using myApiTreeView.Utilities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TreeViewTesting
{
    [TestClass]
    public class TestCaseServiceTest
    {
        private Mock<IDataRepo> _repoMock;
        private TestCaseService _service;
        public TestCaseServiceTest()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<AutoMapperProfiles>();
            });
        }

        [TestInitialize]
        public void Initialize()
        {
            var mocker = new AutoMocker();
            _repoMock = mocker.GetMock<IDataRepo>();
            _service = new TestCaseService(_repoMock.Object);
            
        }

        private static List<TestCase> getMockTestCaseList()
        {
            return new List<TestCase>() {
                        new TestCase(){
                            TestCaseId = 11,
                            Name = "TestCase11",
                            StepCount = 11,
                            FolderId = 1
                        },
                        new TestCase(){
                           TestCaseId = 12,
                            Name = "TestCase12",
                            StepCount = 12,
                            FolderId = 1
                        }
                };
        }

        [TestMethod]
        public async Task GetTestCases_Success()
        {
            //Arrange
            _repoMock.Setup(repo => repo.GetTestCases(It.IsAny<int>()))
                .ReturnsAsync(getMockTestCaseList());

            // Act
            var response = await _service.GetTestCases(1);
            var result = response as List<TestCaseDto>;
            //Assert
            Assert.IsNotNull(response);
            _repoMock.Verify(t => t.GetTestCases(1), Times.Once);
            Assert.IsTrue(response.Count == 2);
            Assert.IsTrue(response[0].TestCaseId == 11);
            Assert.IsTrue(response[0].Name == "TestCase11");
            Assert.IsTrue(response[1].TestCaseId == 12);
            Assert.IsTrue(response[1].Name == "TestCase12");
        }

    }
}

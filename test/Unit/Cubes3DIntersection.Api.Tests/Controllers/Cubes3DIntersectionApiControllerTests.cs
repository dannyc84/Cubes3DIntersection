using AutoMapper;
using Cubes3DIntersection.Api.Controllers;
using Cubes3DIntersection.Application.Factories;
using Cubes3DIntersection.Application.Interfaces;
using Cubes3DIntersection.Application.Models;
using Cubes3DIntersection.Core.Repository;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Cubes3DIntersection.Api.Tests.Controllers
{
    public class Cubes3DIntersectionApiControllerTests
    {
        private MockRepository _mockRepository;

        private Mock<ICube3DIntersectionService> _mockCube3DIntersectionService;
        private Mock<IMapper> _mockMapper;
        private Mock<IAppLogger<Cubes3DIntersectionApiController>> _mockAppLogger;

        public Cubes3DIntersectionApiControllerTests()
        {
            _mockRepository = new MockRepository(MockBehavior.Default);

            _mockCube3DIntersectionService = _mockRepository.Create<ICube3DIntersectionService>();
            _mockMapper = _mockRepository.Create<IMapper>();
            _mockAppLogger = _mockRepository.Create<IAppLogger<Cubes3DIntersectionApiController>>();
        }

        private Cubes3DIntersectionApiController CreateCubes3DIntersectionApiController()
        {
            return new Cubes3DIntersectionApiController(
                _mockCube3DIntersectionService.Object,
                _mockMapper.Object,
                _mockAppLogger.Object);
        }

        [Fact]
        public async Task PostCubes3DIntersection_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var cubes3DIntersectionApiController = CreateCubes3DIntersectionApiController();
            var cube3DIntersectionModel = new Cube3DIntersectionModel
            {
                FirstCube3D = new Cube3DModel
                {
                    PointCoordinates = PointModelFactory.Create(2, 2, 2)
                },
                SecondCube3D = new Cube3DModel
                {
                    PointCoordinates = PointModelFactory.Create(4, 2, 2)
                },
                EdgesLength = 2
            };

            // Act
            var response = await cubes3DIntersectionApiController.PostCubes3DIntersection(
                cube3DIntersectionModel);
            var okResult = response.Result as OkObjectResult;

            // Assert
            Assert.NotNull(okResult);
            Assert.True(okResult is OkObjectResult);
            Assert.True((int)HttpStatusCode.OK == okResult.StatusCode);
            _mockRepository.VerifyAll();
        }
    }
}

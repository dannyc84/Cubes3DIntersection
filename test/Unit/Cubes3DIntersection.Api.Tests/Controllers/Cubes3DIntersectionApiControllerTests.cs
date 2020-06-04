using Cubes3DIntersection.Api.Controllers;
using Cubes3DIntersection.Application.Factories;
using Cubes3DIntersection.Application.Interfaces;
using Cubes3DIntersection.Application.Models;
using Cubes3DIntersection.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Cubes3DIntersection.Api.Tests.Controllers
{
    public class Cubes3DIntersectionApiControllerTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly Mock<ICube3DIntersectionService> _mockCube3DIntersectionService;
        private readonly Mock<IAppLogger<Cubes3DIntersectionApiController>> _mockAppLogger;

        public Cubes3DIntersectionApiControllerTests(CustomWebApplicationFactory<Startup> factory)
        {
            _mockCube3DIntersectionService = new Mock<ICube3DIntersectionService>();
            _mockAppLogger = new Mock<IAppLogger<Cubes3DIntersectionApiController>>();
        }

        [Fact]
        public async Task PostCubes3DIntersectionShouldTouch()
        {
            // Arrange
            MakeCube3DIntersectionRequest(out var cubes3DIntersectionApiController, out var firstCube3D,
                out var secondCube3D, out var edgesLength, out var cube3DIntersectionRequest);

            var cube3DIntersectionExpectedResponse = new Cube3DIntersectionModel
            {
                FirstCube3D = firstCube3D,
                SecondCube3D = secondCube3D,
                EdgesLength = edgesLength,
                Collision = true,
                IntersectionVolume = 0
            };

            _mockCube3DIntersectionService
                .Setup(ci => ci.Create(cube3DIntersectionRequest))
                .ReturnsAsync(cube3DIntersectionExpectedResponse);

            // Act
            var createdResponse = await cubes3DIntersectionApiController.PostCubes3DIntersection(
                cube3DIntersectionRequest);

            Assert.NotNull(createdResponse);

            var result = createdResponse.Result as ObjectResult;
            Assert.Equal(result?.StatusCode, (int)HttpStatusCode.Created);

            var actualResult = result?.Value as Cube3DIntersectionModel;
            Assert.IsType<Cube3DIntersectionModel>(actualResult);

            Assert.True(actualResult.Collision == cube3DIntersectionExpectedResponse.Collision);
            Assert.True(actualResult.IntersectionVolume.Equals(cube3DIntersectionExpectedResponse.IntersectionVolume));

            _mockCube3DIntersectionService.VerifyAll();
        }

        private void MakeCube3DIntersectionRequest(
            out Cubes3DIntersectionApiController cubes3DIntersectionApiController,
            out Cube3DModel firstCube3D,
            out Cube3DModel secondCube3D,
            out int edgesLength,
            out Cube3DIntersectionModel cube3DIntersectionRequest)
        {
            cubes3DIntersectionApiController = CreateCubes3DIntersectionApiController();
            firstCube3D = new Cube3DModel
            {
                PointCoordinates = PointModelFactory.Create(2, 2, 2)
            };
            secondCube3D = new Cube3DModel
            {
                PointCoordinates = PointModelFactory.Create(4, 2, 2)
            };
            edgesLength = 2;
            cube3DIntersectionRequest = new Cube3DIntersectionModel
            {
                FirstCube3D = firstCube3D,
                SecondCube3D = secondCube3D,
                EdgesLength = edgesLength
            };
        }

        [Fact]
        public async Task PostCubes3DIntersectionShouldOverlap()
        {
            // Arrange
            var cubes3DIntersectionApiController = CreateCubes3DIntersectionApiController();

            var firstCube3D = new Cube3DModel
            {
                PointCoordinates = PointModelFactory.Create(2, 2, 2)
            };

            var secondCube3D = new Cube3DModel
            {
                PointCoordinates = PointModelFactory.Create(3, 2, 2)
            };

            var edgesLength = 2;

            var cube3DIntersectionRequest = new Cube3DIntersectionModel
            {
                FirstCube3D = firstCube3D,
                SecondCube3D = secondCube3D,
                EdgesLength = edgesLength
            };

            var cube3DIntersectionExpectedResponse = new Cube3DIntersectionModel
            {
                FirstCube3D = firstCube3D,
                SecondCube3D = secondCube3D,
                EdgesLength = edgesLength,
                Collision = true,
                IntersectionVolume = 4
            };

            _mockCube3DIntersectionService
                .Setup(ci => ci.Create(cube3DIntersectionRequest))
                .ReturnsAsync(cube3DIntersectionExpectedResponse);

            // Act
            var createdResponse = await cubes3DIntersectionApiController.PostCubes3DIntersection(
                cube3DIntersectionRequest);

            Assert.NotNull(createdResponse);

            var result = createdResponse.Result as ObjectResult;
            Assert.Equal(result?.StatusCode, (int)HttpStatusCode.Created);

            var actualResult = result?.Value as Cube3DIntersectionModel;
            Assert.IsType<Cube3DIntersectionModel>(actualResult);

            Assert.True(actualResult.Collision == cube3DIntersectionExpectedResponse.Collision);
            Assert.True(actualResult.IntersectionVolume.Equals(cube3DIntersectionExpectedResponse.IntersectionVolume));

            _mockCube3DIntersectionService.VerifyAll();
        }

        [Fact]
        public async Task PostCubes3DIntersectionShouldNotTouch()
        {
            // Arrange
            var cubes3DIntersectionApiController = CreateCubes3DIntersectionApiController();

            var firstCube3D = new Cube3DModel
            {
                PointCoordinates = PointModelFactory.Create(2, 2, 2)
            };

            var secondCube3D = new Cube3DModel
            {
                PointCoordinates = PointModelFactory.Create(10, 10, 10)
            };

            var edgesLength = 2;

            var cube3DIntersectionRequest = new Cube3DIntersectionModel
            {
                FirstCube3D = firstCube3D,
                SecondCube3D = secondCube3D,
                EdgesLength = edgesLength
            };

            var cube3DIntersectionExpectedResponse = new Cube3DIntersectionModel
            {
                FirstCube3D = firstCube3D,
                SecondCube3D = secondCube3D,
                EdgesLength = edgesLength,
                Collision = false,
                IntersectionVolume = 0
            };

            _mockCube3DIntersectionService
                .Setup(ci => ci.Create(cube3DIntersectionRequest))
                .ReturnsAsync(cube3DIntersectionExpectedResponse);

            // Act
            var createdResponse = await cubes3DIntersectionApiController.PostCubes3DIntersection(
                cube3DIntersectionRequest);

            Assert.NotNull(createdResponse);

            var result = createdResponse.Result as ObjectResult;
            Assert.Equal(result?.StatusCode, (int)HttpStatusCode.Created);

            var actualResult = result?.Value as Cube3DIntersectionModel;
            Assert.IsType<Cube3DIntersectionModel>(actualResult);

            Assert.True(actualResult.Collision == cube3DIntersectionExpectedResponse.Collision);
            Assert.True(actualResult.IntersectionVolume.Equals(cube3DIntersectionExpectedResponse.IntersectionVolume));

            _mockCube3DIntersectionService.VerifyAll();
        }

        private Cubes3DIntersectionApiController CreateCubes3DIntersectionApiController()
        {
            return new Cubes3DIntersectionApiController(
                _mockCube3DIntersectionService.Object,
                _mockAppLogger.Object);
        }
    }
}
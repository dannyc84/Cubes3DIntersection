using Cubes3DIntersection.Application.Extensions;
using Cubes3DIntersection.Application.Factories;
using Cubes3DIntersection.Application.Interfaces;
using Cubes3DIntersection.Application.Mappers;
using Cubes3DIntersection.Application.Models;
using Cubes3DIntersection.Core.Entities;
using Cubes3DIntersection.Core.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cubes3DIntersection.Core.Interfaces;

namespace Cubes3DIntersection.Application.Services
{
    public class Cube3DIntersectionService : ICube3DIntersectionService
    {
        private readonly IGenericRepository<Cube3DIntersection> _cube3DIntersectionRepository;
        private readonly ICube3DService _cube3DService;
        private readonly IPoint3DService _point3DService;
        private readonly IEdgeService _edgeService;
        private readonly IAppLogger<Cube3DIntersectionService> _logger;

        private const string serviceName = nameof(Cube3DIntersectionService);
        private const string withIdEqualTo = "with Id =";

        public Cube3DIntersectionService(IGenericRepository<Cube3DIntersection> cube3DIntersectRepository,
            ICube3DService cube3DService,
            IPoint3DService point3DService,
            IEdgeService edgeService,
            IAppLogger<Cube3DIntersectionService> logger)
        {
            _cube3DIntersectionRepository = cube3DIntersectRepository ?? throw new ArgumentNullException(nameof(cube3DIntersectRepository));
            _cube3DService = cube3DService ?? throw new ArgumentNullException(nameof(cube3DIntersectRepository));
            _point3DService = point3DService ?? throw new ArgumentNullException(nameof(point3DService));
            _edgeService = edgeService ?? throw new ArgumentNullException(nameof(edgeService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IEnumerable<Cube3DIntersectionModel>> GetCubes3DList()
        {
            var cubes3DList = await _cube3DIntersectionRepository.GetAll();
            var mapped = ObjectMapper.Mapper.Map<IEnumerable<Cube3DIntersectionModel>>(cubes3DList);
            return mapped;
        }

        public async Task<Cube3DIntersectionModel> GetCube3DById(int cube3DId)
        {
            var cube3D = await _cube3DIntersectionRepository.GetById(cube3DId);
            var mapped = ObjectMapper.Mapper.Map<Cube3DIntersectionModel>(cube3D);
            return mapped;
        }

        public async Task<Cube3DIntersectionModel> Create(Cube3DIntersectionModel cube3DIntersectionModel)
        {
            await ValidateIfExist(cube3DIntersectionModel);

            return await CreateCubesIntersectionDto(cube3DIntersectionModel);
        }

        private async Task<Cube3DIntersectionModel> CreateCubesIntersectionDto(Cube3DIntersectionModel cube3DIntersectionModel)
        {
            var cubesIntersectionMappedEntity = ObjectMapper.Mapper.Map<Cube3DIntersection>(cube3DIntersectionModel);
            if (cubesIntersectionMappedEntity == null)
                throw new ApplicationException($"Entity could not be mapped.");

            var newCubeIntersectionMappedEntity = await _cube3DIntersectionRepository.Create(cubesIntersectionMappedEntity);
            _logger.LogInformation($"Entity successfully added - { serviceName } ");

            var newCubeIntersectionMappedDto = ObjectMapper.Mapper.Map<Cube3DIntersectionModel>(newCubeIntersectionMappedEntity);

            var cubesIntersectionDto = new Cube3DIntersectionModel
            {
                FirstCube3D = await UpdateCubeDto(newCubeIntersectionMappedDto.FirstCube3D, newCubeIntersectionMappedDto.EdgesLength),
                SecondCube3D = await UpdateCubeDto(newCubeIntersectionMappedDto.SecondCube3D, newCubeIntersectionMappedDto.EdgesLength),
                EdgesLength = newCubeIntersectionMappedDto.EdgesLength
            };
            cubesIntersectionDto.Id = cubesIntersectionDto.FirstCube3D.Id;
            cubesIntersectionDto.SecondCube3DId = cubesIntersectionDto.SecondCube3D.Id;
            cubesIntersectionDto.Collision = CubeModelExtensions.Collision(cubesIntersectionDto.FirstCube3D, cubesIntersectionDto.SecondCube3D);
            cubesIntersectionDto.IntersectionVolume = CubeModelExtensions.IntersectionVolume(cubesIntersectionDto.FirstCube3D, cubesIntersectionDto.SecondCube3D);

            await UpdateCubesIntersectionDto(cubesIntersectionDto);
            return cubesIntersectionDto;
        }

        private async Task<Cube3DIntersectionModel> UpdateCubesIntersectionDto(Cube3DIntersectionModel cube3DIntersectionModel)
        {
            var mappedEntity = ObjectMapper.Mapper.Map<Cube3DIntersection>(cube3DIntersectionModel);
            if (mappedEntity == null)
                throw new ApplicationException($"Entity could not be mapped.");

            await _cube3DIntersectionRepository.Update(mappedEntity);
            _logger.LogInformation($"Entity successfully added - AspnetRunAppService");

            var newMappedEntity = ObjectMapper.Mapper.Map<Cube3DIntersectionModel>(mappedEntity);
            return newMappedEntity;
        }

        private async Task<Cube3DModel> UpdateCubeDto(Cube3DModel cube3DModel, double edgesLength)
        {
            var CubePointDto = cube3DModel?.PointCoordinates;
            CubePointDto.Cube3DId = cube3DModel.Id;
            await _point3DService.Update(CubePointDto);

            var edgeWithDto = await CreateEdgeDto(CubePointDto.X0, edgesLength, CubePointDto.Cube3DId);

            var edgeHeightDto = await CreateEdgeDto(CubePointDto.Y0, edgesLength, CubePointDto.Cube3DId);

            var edgeDepthDto = await CreateEdgeDto(CubePointDto.Z0, edgesLength, CubePointDto.Cube3DId);

            cube3DModel.Edges = new List<EdgeModel>() {
                edgeWithDto,
                edgeHeightDto,
                edgeDepthDto
            };

            await _cube3DService.Update(cube3DModel);
            return cube3DModel;
        }

        private async Task<EdgeModel> CreateEdgeDto(double pointCenter, double edgeLength, int cubeId)
        {
            var edgeDto = await _edgeService.Create(EdgeModelFactory.Create(pointCenter, edgeLength, cubeId));
            _logger.LogInformation($"Entity { nameof(edgeDto) } successfully created - { serviceName }");
            return edgeDto;
        }

        private async Task ValidateIfExist(Cube3DIntersectionModel Cube3DIntersectionModel)
        {
            var firstCube3D = await _cube3DIntersectionRepository.GetById(Cube3DIntersectionModel.Id);
            var secondCube3D = await _cube3DIntersectionRepository.GetById(Cube3DIntersectionModel.SecondCube3DId);
            if (firstCube3D != null && secondCube3D != null)
                throw new ApplicationException($"The pair of cubes with Ids = { firstCube3D.Id } { secondCube3D.Id } already exists");
        }
    }
}
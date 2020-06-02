using Cubes3DIntersection.Application.Interfaces;
using Cubes3DIntersection.Application.Mappers;
using Cubes3DIntersection.Application.Models;
using Cubes3DIntersection.Application.Services;
using Cubes3DIntersection.Core.Entities;
using Cubes3DIntersection.Core.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cubes3DIntersection.Application
{
    public class Cube3DService : ICube3DService
    {
        private readonly IGenericRepository<Cube3D> _cube3DRepository;
        private readonly IPoint3DService _point3DService;
        private readonly IEdgeService _edgeService;
        private readonly IAppLogger<Cube3DService> _logger;

        private const string serviceName = nameof(Cube3DService);
        private const string withIdEqualTo = "with Id =";

        public Cube3DService(IGenericRepository<Cube3D> cube3DRepository,
            IPoint3DService point3DService,
            IEdgeService edgeService,
            IAppLogger<Cube3DService> logger)
        {
            _cube3DRepository = cube3DRepository ?? throw new ArgumentNullException(nameof(cube3DRepository));
            _point3DService = point3DService ?? throw new ArgumentNullException(nameof(point3DService));
            _edgeService = edgeService ?? throw new ArgumentNullException(nameof(edgeService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IEnumerable<Cube3DModel>> GetCubes3DList()
        {
            var cubes3DList = await _cube3DRepository.GetAll();
            var mapped = ObjectMapper.Mapper.Map<IEnumerable<Cube3DModel>>(cubes3DList);
            return mapped;
        }

        public async Task<Cube3DModel> GetCube3DById(int cube3DId)
        {
            var cube3D = await _cube3DRepository.GetById(cube3DId);
            var mapped = ObjectMapper.Mapper.Map<Cube3DModel>(cube3D);
            return mapped;
        }

        public async Task<Cube3DModel> Create(Cube3DModel Cube3DModel)
        {
            await ValidateIfExist(Cube3DModel);

            var entity = ObjectMapper.Mapper.Map<Cube3D>(Cube3DModel)
                ?? throw new Exception($"Entity {nameof(Cube3DModel)} could not be mapped.");

            var newEntity = await _cube3DRepository.Create(entity);
            _logger.LogInformation($"Entity successfully added - { serviceName }");

            var newMappedEntity = ObjectMapper.Mapper.Map<Cube3DModel>(newEntity);
            return newMappedEntity;
        }

        private async Task ValidateIfExist(Cube3DModel Cube3DModel)
        {
            var cube3D = await _cube3DRepository.GetById(Cube3DModel.Id);
            if (cube3D != null) throw new ApplicationException($"The cube 3D with Id = { Cube3DModel.Id } already exists");
        }

        public async Task Update(Cube3DModel cube3D)
        {
            var mappedEntity = ObjectMapper.Mapper.Map<Cube3D>(cube3D) ?? throw new ApplicationException($"Entity could not be mapped.");

            await _cube3DRepository.Update(mappedEntity);
            _logger.LogInformation($"Entity successfully updated - {serviceName}");
        }
    }
}